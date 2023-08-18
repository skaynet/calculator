internal class Token
{
    public TokenType Type { get; private set; }
    public string Value { get; private set; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    internal static Token None()
    {
        return new Token(TokenType.None, "");
    }

    public override string ToString()
    {
        return Value;
    }
}