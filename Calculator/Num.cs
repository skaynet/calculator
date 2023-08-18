internal class Num : Expression
{
    internal Token Token { get; private set; }

    public Num(Token token)
    {
        Token = token;
    }

    override public object Accept(INodeVisitor visitor)
    {
        return visitor.VisitNum(Token);
    }
}