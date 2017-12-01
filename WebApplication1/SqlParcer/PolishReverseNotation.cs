using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlParcer
{
    public class ReversePolishNotation
    {
        private static readonly TokenType[][] _tokenTypes = new[]
        {
            new[] {TokenType.LeftParen, TokenType.RightParen},
            new[] {TokenType.And, TokenType.Or},
            new[]
            {
                TokenType.Equal, TokenType.BangEqual, TokenType.Greater, TokenType.GreaterEqual, TokenType.Less,
                TokenType.LessEqual, TokenType.Like
            },
            new[] {TokenType.Minus, TokenType.Plus},
            new[] {TokenType.Star, TokenType.Slash},
            new[] {TokenType.As}
        };

        public static int GetPriority(TokenType token)
        {
            for (var i = 0; i < _tokenTypes.Length; i++)
            {
                if (_tokenTypes[i].Contains(token))
                {
                    return i;
                }
            }

            return -1;
        }

        public static List<Token> GetTokens(string expression)
        {
            var tokens = new Scanner(expression).ScanTokens();
            var resultTokens = new List<Token>();

            var leftTokens = new List<TokenType>
            {
                TokenType.And,
                TokenType.Or,
                TokenType.LeftParen,
                TokenType.BangEqual,
                TokenType.Equal,
                TokenType.Greater,
                TokenType.GreaterEqual,
                TokenType.LessEqual,
                TokenType.Less,
            };

            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i]._type == TokenType.Minus)
                {
                    if (i == 0 || leftTokens.Contains(tokens[i - 1]._type))
                    {
                        resultTokens.Add(new Token(TokenType.ShortInt, "0", (short)0));
                    }
                }
                resultTokens.Add(tokens[i]);
            }

            return resultTokens;
        }

        public static List<Token> ConvertToPrefixVersion(List<Token> tokens)
        {
            var stack = new Stack<Token>();
            var result = new List<Token>();

            foreach (var node in tokens)
            {
                if (node.IsOperator())
                {
                    while (stack.Count != 0 && GetPriority(node._type) <= GetPriority(stack.Peek()._type))
                        result.Add(stack.Pop());
                    stack.Push(node);
                }
                else if (node._type == TokenType.RightParen)
                {
                    while (stack.Count != 0 && stack.Peek()._type != TokenType.LeftParen)
                        result.Add(stack.Pop());
                    if (stack.Count != 0)
                        stack.Pop();
                    else
                        throw new Exception("there was not )");
                }
                else if (node._type == TokenType.LeftParen)
                {
                    stack.Push(node);
                }
                else
                {
                    result.Add(node);
                }
            }

            while (stack.Count != 0)
                result.Add(stack.Pop());
            return result;
        }

        public static object GetValue(object first, object second, Token oper)
        {
          switch (oper._type)
            {
                case TokenType.Plus:
                    return Operations.Add(first, second);
                case TokenType.As:
                    return Operations.As(first, second);
                case TokenType.Minus:
                    return Operations.Substract(first, second);
                case TokenType.Star:
                    return Operations.Multiply(first, second);
                case TokenType.Slash:
                    return Operations.Divide(first, second);
                case TokenType.Greater:
                    return Operations.More(first, second);
                case TokenType.Less:
                    return Operations.Less(first, second);
                case TokenType.GreaterEqual:
                    return Operations.Equal(first, second) || Operations.More(first, second);
                case TokenType.LessEqual:
                    return Operations.Equal(first, second) || Operations.Less(first, second);
                case TokenType.Equal:
                    return Operations.Equal(first, second);
                case TokenType.BangEqual:
                    return !Operations.Equal(first, second);
                case TokenType.And:
                    return Operations.And(first, second);
                case TokenType.Or:
                    return Operations.Or(first, second);
            }
            return null;
        }

        public static bool Evaluate(List<Token> tokens, Dictionary<string, object> dict = null)
        {
            dict = dict ?? new Dictionary<string, object>();
            var nodes = ConvertToPrefixVersion(tokens);
            var stack = new Stack<object>();

            foreach (var node in nodes)
                if (node.IsOperator())
                {

                    var second = stack.Pop();
                    var first = stack.Pop();

                    var result = GetValue(first, second, node);

                    stack.Push(result);
                }
                else if (node._type == TokenType.Identifier && dict.ContainsKey(node._lexeme))
                {
                    var identifierValue = dict[node._lexeme];
                    stack.Push(identifierValue);
                }
                else
                {
                    stack.Push(node._literal);
                }


            var res = stack.Pop();
            if (res.GetType() != typeof(bool))
                throw new Exception("expression should return boolean");

            return (bool)res;
        }
    }
}