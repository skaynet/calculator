internal interface INode
{
    object Accept(INodeVisitor visitor);
}