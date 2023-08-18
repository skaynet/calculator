using System.Globalization;

internal class ValueBuilder : INodeVisitor
{
    public object VisitBinOp(Token op, INode left, INode right)
    {
        float leftOperand = (float)left.Accept(this);
        float rightOperand = (float)right.Accept(this);

        switch (op.Type)
        {
            case TokenType.Plus:
                return leftOperand + rightOperand;
            case TokenType.Minus:
                return leftOperand - rightOperand;
            case TokenType.Multiply:
                return leftOperand * rightOperand;
            case TokenType.Divide:
                return rightOperand == 0 ? throw new DivideByZeroException("Divide by zero.") : leftOperand / rightOperand;
            default:
                throw new NotSupportedException($"Unknown Operator. Не поддерживаемый оператор {op.Type}");
        }
    }

    public object VisitNum(Token num)
    {
        if (!float.TryParse(num.Value, CultureInfo.InvariantCulture, out float value))
        {
            throw new FormatException("Wrong expression. Ошибка в выражении.");
        }
        return value;
    }

    public object VisitUnaryOp(Token op, INode node)
    {
        switch (op.Type)
        {
            case TokenType.Plus:
                return (float)node.Accept(this);
            case TokenType.Minus:
                return -(float)node.Accept(this);
            default:
                throw new FormatException($"Wrong expression. Ошибка в выражении: {op.Type.ToString()}");
        }
    }
}