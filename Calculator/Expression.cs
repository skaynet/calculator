internal abstract class Expression : INode
{
    abstract public object Accept(INodeVisitor visitor);
}