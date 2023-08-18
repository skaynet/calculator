internal interface INodeVisitor
{
    object VisitNum(Token num);
    object VisitUnaryOp(Token op, INode node);
    object VisitBinOp(Token op, INode left, INode right);
}