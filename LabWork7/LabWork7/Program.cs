string path = $"{Directory.GetCurrentDirectory()}\\logs\\log.txt";

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

        Console.WriteLine("Введите второе слагаемое: ");

        string? y = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(y))
            throw new ArgumentNullException("Y");

        if (y.Trim().ToLower() == "exit")
            break;

        if (!int.TryParse(y, out int intY))
            throw new FormatException("Некорректный ввод Y");

        Console.WriteLine("Введите действие [+, -, *, /] ");
        var action = Console.ReadKey();

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
                Console.WriteLine("\nНекорректное действие");
                break;
        }

        Console.WriteLine($"\nРезультат: {result}");
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine("\nОшибка ввода");
        Console.WriteLine(ex.Message);
        WriteLog(path, ex.ToString(), ex.GetType().ToString());
    }
    catch (FormatException ex)
    {
        Console.WriteLine("\nОшибка ввода");
        Console.WriteLine(ex.Message);
        WriteLog(path, ex.ToString(), ex.GetType().ToString());
    }
    catch (OverflowException ex)
    {
        Console.WriteLine("\nПереполнение типа");
        Console.WriteLine(ex.Message);
        WriteLog(path, ex.ToString(), ex.GetType().ToString());
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nНепредвиденная ошибка");
        Console.WriteLine(ex.Message);
        WriteLog(path, ex.ToString(), ex.GetType().ToString());
    }
}

static void WriteLog(string path, string data, string type)
{
    if (!File.Exists(path))
        File.Create(path).Close();

    string message = $"[{DateTime.Now}] {type}: {data}\n";
    File.AppendAllText(path, message);
}