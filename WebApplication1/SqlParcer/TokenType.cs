namespace SqlParcer
{
    public enum TokenType
    {
        // Single-character tokens.
        LeftParen, RightParen, Minus, Plus, Slash, Star,

        // One or two character tokens.
        Bang, BangEqual, Equal,
        Greater, GreaterEqual,
        Less, LessEqual,

        // Literals.
        Identifier, String, ShortInt, Int, BigInt, Double,

        // Keywords.
        And, False, Nil, Or,
        True, As, Like
    }
}