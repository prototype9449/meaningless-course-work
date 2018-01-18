using System;
using System.Collections.Generic;

namespace SqlParser
{
    public class Scanner
    {
        public static readonly IDictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>()
        {
            {"and",    TokenType.And},
            {"false",  TokenType.False},
            {"nil",    TokenType.Nil},
            {"or",     TokenType.Or},
            {"true",   TokenType.True},
            {"as", TokenType.As},
            {"like", TokenType.Like }
        };

        public static readonly List<TokenType> Operations = new List<TokenType>()
        {
            TokenType.Minus, TokenType.Plus, TokenType.Star, TokenType.Slash,
            TokenType.Less, TokenType.LessEqual, TokenType.BangEqual,  TokenType.Equal, TokenType.Greater, TokenType.GreaterEqual,
            TokenType.As, TokenType.And, TokenType.Or, TokenType.Like
        };

        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private int _start = 0;
        private int _current = 0;

        public Scanner(string source)
        {
            _source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                //  We are at the beginning of the next lexeme.
                _start = _current;
                ScanToken();
            }

            //_tokens.Add(new Token(TokenType.Eof, "", null));
            return _tokens;
        }

        private void ScanToken()
        {
            var c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LeftParen);
                    break;
                case ')':
                    AddToken(TokenType.RightParen);
                    break;
                case '-':
                    AddToken(TokenType.Minus);
                    break;
                case '+':
                    AddToken(TokenType.Plus);
                    break;
                case '*':
                    AddToken(TokenType.Star);
                    break;
                case '!':
                    AddToken(Match('=') ? TokenType.BangEqual : TokenType.Bang);
                    break;
                case '=':
                    AddToken(TokenType.Equal);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater);
                    break;
                case '/':
                    if (Match('/'))
                    {
                        while (Peek() != '\n' && !IsAtEnd())
                        {
                            Advance();
                        }
                    }
                    else
                    {
                        AddToken(TokenType.Slash);
                    }

                    break;
                case ' ':
                case '\r':
                case '\t':
                    //  Ignore whitespace.
                    break;
                case '\n':
                    return;
                case '"':
                    ParseString();
                    break;
                default:
                    if (IsDigit(c))
                    {
                        Number();
                    }
                    else if (IsAlpha(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        Console.WriteLine("Unexpected character.");
                    }

                    break;
            }
        }

        private void Identifier()
        {
            while (IsAlphaNumeric(Peek())) Advance();

            var text = Substring(_source, _start, _current);

            TokenType type;

            type = Keywords.ContainsKey(text) ? Keywords[text] : TokenType.Identifier;

            AddToken(type, text);
        }
        private void Number()
        {
            while (IsDigit(Peek())) Advance();

            // Look for a fractional part.
            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                // Consume the "."
                Advance();

                while (IsDigit(Peek())) Advance();

                AddToken(TokenType.Double,
                    double.Parse(Substring(_source, _start, _current).Replace('.', ',')));
                return;
            }

            short shortNum;
            int intNum;
            long longNum;

            var str = Substring(_source, _start, _current);
            if (short.TryParse(str, out shortNum))
            {
                AddToken(TokenType.ShortInt, shortNum);
                return;
            }
            if (int.TryParse(str, out intNum))
            {
                AddToken(TokenType.Int, intNum);
                return;
            }
            if (long.TryParse(str, out longNum))
            {
                AddToken(TokenType.BigInt, longNum);
            }

            AddToken(TokenType.String, str);
        }

        private string Substring(string line, int start, int end)
        {
            return line.Substring(start, end - start);
        }

        private void ParseString()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n') return;
                Advance();
            }

            // Unterminated string.
            if (IsAtEnd())
            {
                Console.WriteLine("Unterminated string.");
                return;
            }

            // The closing ".
            Advance();

            // Trim the surrounding quotes.
            var value = Substring(_source, _start + 1, _current - 1);
            AddToken(TokenType.String, value);
        }

        private bool Match(char expected)
        {
            if (IsAtEnd())
            {
                return false;
            }

            if (_source[_current] != expected)
            {
                return false;
            }

            _current++;
            return true;
        }

        private char Peek()
        {
            if (IsAtEnd())
            {
                return '\\';
            }

            return _source[_current];
        }

        private char PeekNext()
        {
            if (_current + 1 >= _source.Length)
            {
                return '\\';
            }

            return _source[_current + 1];
        }

        private bool IsAlpha(char c)
        {
            return c >= 'a'
                   && c <= 'z' || c >= 'A'
                   && c <= 'Z' || c == '.';
        }
        private bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAtEnd()
        {
            return _current >= _source.Length;
        }

        private char Advance()
        {
            _current++;
            return _source[_current - 1];
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, object literal)
        {
            var text = Substring(_source, _start, _current);
            _tokens.Add(new Token(type, text, literal));
        }
    }
}