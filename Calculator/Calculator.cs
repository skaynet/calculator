using System.Globalization;

public class Calculator
{
    public readonly IFormatProvider FormatterDecimalSeparator;

    private Token _curToken;
    private ExpressionParser _expParser;

    public Calculator()
    {
        FormatterDecimalSeparator = new NumberFormatInfo { NumberDecimalSeparator = "." };

        _curToken = Token.None();
        _expParser = new ExpressionParser();
    }

    public void CheckExpression(string expressionStr)
    {
        _expParser.CheckExpression(expressionStr);
    }

    public float CalculateExpression(string expressionStr)
    {
        _expParser.StartParsing(expressionStr);
        _curToken = _expParser.NextToken();

        Expression node = GrabExpr();
        ExpectToken(TokenType.None);
        float result = (float)node.Accept(new ValueBuilder());

        return MathF.Round(result, 2);
    }

    private Token ExpectToken(TokenType tokenType)
    {
        if (_curToken.Type == tokenType)
        {
            return _curToken;
        }
        else
        {
            throw new FormatException(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.", _expParser.CurPos, tokenType, _curToken.Type.ToString()));
        }
    }

    private Expression GrabExpr()
    {
        Expression left = GrabTerm();

        while (_curToken.Type == TokenType.Plus
            || _curToken.Type == TokenType.Minus)
        {
            Token op = _curToken;
            _curToken = _expParser.NextToken();
            Expression right = GrabTerm();
            left = new BinOp(op, left, right);
        }

        return left;
    }

    private Expression GrabTerm()
    {
        Expression left = GrabFactor();

        while (_curToken.Type == TokenType.Multiply
            || _curToken.Type == TokenType.Divide)
        {
            Token op = _curToken;
            _curToken = _expParser.NextToken();
            Expression right = GrabFactor();
            left = new BinOp(op, left, right);
        }

        return left;
    }

    private Expression GrabFactor()
    {
        if (_curToken.Type == TokenType.Plus
         || _curToken.Type == TokenType.Minus)
        {
            Expression node = GrabUnaryExpr();
            return node;
        }
        else if (_curToken.Type == TokenType.LeftParenthesis)
        {
            Expression node = GrabBracketExpr();
            return node;
        }
        else
        {
            Token token = ExpectToken(TokenType.Number);
            _curToken = _expParser.NextToken();
            return new Num(token);
        }
    }

    private Expression GrabUnaryExpr()
    {
        Token op;

        if (_curToken.Type == TokenType.Plus)
        {
            op = ExpectToken(TokenType.Plus);
        }
        else
        {
            op = ExpectToken(TokenType.Minus);
        }

        _curToken = _expParser.NextToken();

        if (_curToken.Type == TokenType.Plus
         || _curToken.Type == TokenType.Minus)
        {
            Expression expr = GrabUnaryExpr();
            return new UnaryOp(op, expr);
        }
        else
        {
            Expression expr = GrabFactor();
            return new UnaryOp(op, expr);
        }
    }

    private Expression GrabBracketExpr()
    {
        ExpectToken(TokenType.LeftParenthesis);
        _curToken = _expParser.NextToken();
        Expression node = GrabExpr();
        ExpectToken(TokenType.RightParenthesis);
        _curToken = _expParser.NextToken();
        return node;
    }
}