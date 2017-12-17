namespace SqlParser
{
    public class Token
    {
        public readonly TokenType _type;
        public readonly string _lexeme;
        public readonly object _literal;

        public Token(TokenType type, string lexeme, object literal)
        {
            _type = type;
            _lexeme = lexeme;
            _literal = literal;
        }

        public bool IsOperator()
        {
            return Scanner.Operations.Contains(this._type);
        }

        public override string ToString()
        {
            return _type + " " + _lexeme + " " + _literal;
        }
    }
}