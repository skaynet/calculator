using System.Text;

internal class FileReaderWriter
{
    public readonly string NameInputFileCalculate;
    public readonly string NameOutputFileCalculate;

    public StringBuilder ResultToWriteFile { get; private set; }

    private Calculator _calculator;

    public FileReaderWriter(Calculator calculator)
    {
        NameInputFileCalculate = "InputFileCalculate.txt";
        NameOutputFileCalculate = "OutputFileCalculate.txt";

        _calculator = calculator;
        ResultToWriteFile = new StringBuilder();
    }

    public void ReadFileAndCalculate()
    {
        string fullPathFile = GetFullPathFile(NameInputFileCalculate);

        if (!File.Exists(fullPathFile))
        {
            throw new FileNotFoundException(fullPathFile);
        }

        using (StreamReader reader = new StreamReader(fullPathFile))
        {
            string? line = reader.ReadLine();

            while (line != null)
            {
                ResultToWriteFile.Append($"{line} = ");
                try
                {
                    ResultToWriteFile.AppendLine(_calculator.CalculateExpression(line).ToString(_calculator.FormatterDecimalSeparator));
                }
                catch (Exception ex)
                {
                    ResultToWriteFile.AppendLine($"Exeption. {ex.Message}");
                }
                line = reader.ReadLine();
            }
        }
    }

    public void WriteFileResultCalculate()
    {
        string fullPathFile = GetFullPathFile(NameOutputFileCalculate);

        using (StreamWriter writer = new StreamWriter(fullPathFile, false))
        {
            writer.Write(ResultToWriteFile.ToString());
        }
    }

    private string GetFullPathFile(string? fileNameOrFullPath)
    {
        if (String.IsNullOrEmpty(fileNameOrFullPath))
        {
            return "";
        }

        if (fileNameOrFullPath.IndexOf(':') > -1)
        {
            return fileNameOrFullPath;
        }
        else
        {
            return Path.GetFullPath(fileNameOrFullPath);
        }
    }
}