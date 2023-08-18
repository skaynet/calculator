using System.Text.RegularExpressions;

internal class ExpressionParser
{
    public readonly string CheckExpressionPattern;
    public readonly string ExpressionPattern;

    public int CurPos { get; private set; }

    private int _tokensArrayCount;
    private string _curTokenStr;
    private string[] _tokensArray;

    public ExpressionParser()
    {
        CheckExpressionPattern = @"[^\.0-9-+*\/()]|\.{2,}";
        ExpressionPattern = @"(\b[-+*\/]|\d+\.\d+|\d+|[-+*\/()])";

        _curTokenStr = String.Empty;
        CurPos = -1;
        _tokensArray = new string[1];
    }

    public void CheckExpression(string expression)
    {
        Regex regex = new Regex(CheckExpressionPattern);
        if (String.IsNullOrEmpty(expression) || regex.IsMatch(expression))
        {
            throw new ArgumentException("Wrong input.");
        }
        CheckBrackets(expression);
    }

    public void StartParsing(string expression)
    {
        CheckExpression(expression);
        Regex regex = new Regex(ExpressionPattern);
        _tokensArray = regex.Split(expression).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray<string>();

        _tokensArrayCount = _tokensArray.Length;
        _curTokenStr = String.Empty;
        CurPos = -1;
        Advance();
    }

    public Token NextToken()
    {
        if (_curTokenStr == string.Empty)
        {
            return Token.None();
        }

        Token curToken;

        switch (_curTokenStr)
        {
            case "+":
                curToken = new Token(TokenType.Plus, _curTokenStr.ToString());
                break;
            case "-":
                curToken = new Token(TokenType.Minus, _curTokenStr.ToString());
                break;
            case "*":
                curToken = new Token(TokenType.Multiply, _curTokenStr.ToString());
                break;
            case "/":
                curToken = new Token(TokenType.Divide, _curTokenStr.ToString());
                break;
            case "(":
                curToken = new Token(TokenType.LeftParenthesis, _curTokenStr.ToString());
                break;
            case ")":
                curToken = new Token(TokenType.RightParenthesis, _curTokenStr.ToString());
                break;
            default: 
                curToken = new Token(TokenType.Number, _curTokenStr); 
                break;
        }

        Advance();
        return curToken;
    }

    private void Advance()
    {
        CurPos += 1;

        if (CurPos < _tokensArrayCount)
        {
            _curTokenStr = _tokensArray[CurPos];
        }
        else
        {
            _curTokenStr = string.Empty;
        }
    }

    private void CheckBrackets(string expression)
    {
        int brackets = 0;
        foreach (char c in expression)
        {
            switch (c)
            {
                case '(': brackets++; break;
                case ')': brackets--; break;
            }
            if (brackets < 0)
                throw new FormatException("Wrong expression. Не хватает '('");
        }
        if (brackets > 0)
            throw new FormatException("Wrong expression. Не хватает ')'");
    }
}