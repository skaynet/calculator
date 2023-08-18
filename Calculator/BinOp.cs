internal class BinOp : Expression
{
    internal Token Op { get; private set; }
    internal Expression Left { get; private set; }
    internal Expression Right { get; private set; }

    public BinOp(Token op, Expression left, Expression right)
    {
        Op = op;
        Left = left;
        Right = right;
    }

    override public object Accept(INodeVisitor visitor)
    {
        return visitor.VisitBinOp(Op, Left, Right);
    }
}