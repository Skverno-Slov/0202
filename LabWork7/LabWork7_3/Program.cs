using System.Diagnostics;

var id = 0;

var ts = new TraceSource("Calculator")
{
    Switch = new SourceSwitch("CalculatorSwitch") { Level = SourceLevels.Information }
};

ts.Listeners.Clear();
ts.Listeners.Add(new TextWriterTraceListener("trace.log", "fileListener"));
ts.Listeners.Add(new ConsoleTraceListener());
while (true)
{
    try
    {
        Console.WriteLine("Введите первое слагаемое: ");
        string? x = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(x))
            throw new ArgumentNullException("X");

        if (x.Trim().ToLower() == "exit")
            break;

        if (!int.TryParse(x, out int intX))
            throw new FormatException("Некорректный ввод X");

        ts.TraceEvent(TraceEventType.Verbose, id++, $"Выполнен ввод X ({intX})");

        Console.WriteLine("Введите второе слагаемое: ");

        string? y = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(y))
            throw new ArgumentNullException("Y");

        if (y.Trim().ToLower() == "exit")
            break;

        if (!int.TryParse(y, out int intY))
            throw new FormatException("Некорректный ввод Y");

        ts.TraceEvent(TraceEventType.Verbose, id++, $"Выполнен ввод X ({intY})");

        Console.WriteLine("Введите действие [+, -, *, /] ");
        var action = Console.ReadKey();

        Console.WriteLine("\n");

        int result = 0;

        switch (action.KeyChar)
        {
            case '+':
                result = checked(intX + intY);
                break;
            case '-':
                result = checked(intX - intY);
                break;
            case '*':
                result = checked(intX * intY);
                break;
            case '/':
                result = checked(intX / intY);
                break;
            default:
                Console.WriteLine("Некорректное действие");
                break;
        }

        ts.TraceInformation($"Выполнена операция {action.KeyChar}");

        Console.WriteLine($"Результат: {result}");
        ts.TraceInformation($"Результат выполнения {result}");
    }
    catch (ArgumentNullException ex)
    {
        ts.TraceEvent(TraceEventType.Error, id++, $"Ввод не выполнен ({ex})");
    }
    catch (FormatException ex)
    {
        ts.TraceEvent(TraceEventType.Error, id++, $"Неверный формат ({ex})");
    }
    catch (OverflowException ex)
    {
        ts.TraceEvent(TraceEventType.Error, id++, $"Переполнение ({ex})");
    }
    catch (Exception ex)
    {
        ts.TraceEvent(TraceEventType.Error, id++, $"Необработанное исключение ({ex})");
    }
    finally
    {
        ts.Flush(); 
        ts.Close();
    }
}
