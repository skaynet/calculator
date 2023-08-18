using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        try
        {
            Calculator calc = new Calculator();
            CalculatorConsoleView calcView = new CalculatorConsoleView(calc);
            calcView.PrintWelcome();
            calcView.WorkModeSelection();
            calcView.RunCalculator();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникло исключение: {ex.Message}");
        }
    }
}