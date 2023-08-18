public class CalculatorConsoleView
{
    private Calculator _calculator;
    private bool _isFileWorkMode;

    public CalculatorConsoleView(Calculator calculator)
    {
        _calculator = calculator;
    }

    public void PrintWelcome()
    {
        Console.WriteLine("""
                          +---------------------------+
                          | ///////////////////////// |
                          +---------------------------+
                          | [              1,264.45 ] |
                          +---------------------------+
                          |                           |
                          |                           |
                          | [sto] [rcl] [<--] [AC/ON] |
                          |                           |
                          | [ ( ] [ ) ] [sqr] [  /  ] |
                          |                           |
                          | [ 7 ] [ 8 ] [ 9 ] [  *  ] |
                          |                           |
                          | [ 4 ] [ 5 ] [ 6 ] [  -  ] |
                          |                           |
                          | [ 1 ] [ 2 ] [ 3 ] [  +  ] |
                          |                           |
                          | [ 0 ] [ . ] [+/-] [  =  ] |
                          |                           |
                          +---------------------------+
                          """);
        Console.WriteLine("");
    }

    public void WorkModeSelection()
    {
        Console.WriteLine("Выберите режим работы калькулятора: 1-Файловый, 2-Ввод с консоли");

        string? choice;
        bool askAgain = true;

        while (askAgain)
        {
            Console.Write("Выш выбор: ");
            choice = Console.ReadLine();

            if (String.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Вы не чего не ввели, попробуйте еще раз!");
                continue;
            }

            switch (choice)
            {
                case "1":
                    _isFileWorkMode = true;
                    askAgain = false;
                    break;
                case "2":
                    _isFileWorkMode = false;
                    askAgain = false;
                    break;
                default: Console.WriteLine("Неверный ввод, попробуйте еще раз!");
                    break;
            }
        }
        Console.WriteLine("");
    }

    public void RunCalculator()
    {
        if (_isFileWorkMode)
        {
            Console.WriteLine("Выбран режим работы с файлом!");
            Console.WriteLine("Читаем файл...");

            FileReaderWriter fileReaderWriter = new FileReaderWriter(_calculator);

            fileReaderWriter.ReadFileAndCalculate();
            Console.WriteLine("Вычисляем...");
            Console.WriteLine("Пишем результаты в файл...");

            fileReaderWriter.WriteFileResultCalculate();
            Console.WriteLine("Готово!");
            Console.WriteLine("Нажмите любую клавишу, чтобы закрыть это окно…");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Выбран режим работы с консолью!");
            Console.WriteLine("Вводите выражение и нажимайте ввод. Для выхода наберите \"Exit\"");
            bool isContinue = true;
            string expression;
            
            while (isContinue)
            {
                expression = Console.ReadLine() ?? "";
                if (expression.ToLower().Contains("exit"))
                {
                    isContinue = false;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    Console.Write($"{expression} -> ");
                    try
                    {
                        Console.WriteLine(_calculator.CalculateExpression(expression).ToString(_calculator.FormatterDecimalSeparator));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Exception. {ex.Message}");
                    }
                }
            }
        }
    }
}