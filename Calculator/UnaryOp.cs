internal class UnaryOp : Expression
{
    internal Token Op { get; private set; }
    internal Expression Node { get; private set; }

    public UnaryOp(Token op, Expression node)
    {
        Op = op;
        Node = node;
    }

    override public object Accept(INodeVisitor visitor)
    {
        return visitor.VisitUnaryOp(Op, Node);
    }
}