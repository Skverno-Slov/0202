using NLog;
using System.Data;

namespace LabWork6
{
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    #region Task1
                    Console.WriteLine("Введите делимое: ");
                    string? dividend = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(dividend))
                        throw new ArgumentNullException("Делимое");

                    if (dividend.Trim().ToLower() == "exit")
                        break;

                    if (!double.TryParse(dividend, out double doubleDividend))
                        throw new FormatException("Некорректный ввод");

                    Console.WriteLine("Введите делитель: ");

                    string? divider = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(divider))
                        throw new ArgumentNullException("Делитель");

                    if (divider.Trim().ToLower() == "exit")
                        break;

                    if (!double.TryParse(divider, out double doubleDivider))
                        throw new FormatException("Некорректный ввод");

                    if (doubleDivider == 0)
                        throw new DivideByZeroException("Делитель не может быть 0");

                    double divideResult = doubleDividend / doubleDivider;
                    Console.WriteLine($"Частное: {divideResult}");
                    #endregion
                    #region Task2
                    Console.WriteLine("Введите возраст: ");

                    string age = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(age))
                        throw new ArgumentNullException("Делитель");

                    if (divider.Trim().ToLower() == "exit")
                        break;

                    if (!int.TryParse(age, out int intAge))
                        throw new FormatException("Некорректный ввод");

                    if (intAge < 0)
                        throw new NegativeNamberException("Возраст не может быть меньше нуля");
                    Console.WriteLine("Возраст введён корректно");
                    #endregion
                    #region Task3

                    string path = $"{Directory.GetCurrentDirectory()}\\TestData.txt";
                    try
                    {
                        var data = ReadFileData(path);
                        using var testStream = new StreamReader(path);
                        Console.WriteLine("Файл закрыт");

                        List<char> digits = data
                            .Where(char.IsDigit)
                            .ToList();

                        List<int> numbers = digits.Select(c => (int)char.GetNumericValue(c)).ToList();
                        List<int> numbersResult = new List<int>();
                        foreach (var number in numbers)
                        {
                            if (number % 2 == 0)
                                numbersResult.Add(number);
                        }

                        if (numbersResult.Count == 0)
                        {
                            Console.WriteLine("Нет чётных чисел");
                            continue;
                        }
                        Console.WriteLine("Найденные чётные числа:");
                        Console.WriteLine(string.Join(", ", numbersResult));
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (ArgumentNullException)
                    {
                        try
                        {
                            Console.WriteLine("Файл пуст");
                            using var testStream = new StreamReader($"{Directory.GetCurrentDirectory()}\\TestData.txt");
                            Console.WriteLine("Файл закрыт");
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("Файл открыт");
                        }
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Файл открыт");
                    }
                    #endregion
                    #region Task4
                    AppDomain.CurrentDomain.UnhandledException += HandleExeption;
                    CallExeption();
                    #endregion
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.Error(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.Error(ex.Message);
                }
                catch (NegativeNamberException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.Error(ex.Message);
                }
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Непредвиденная ошибка.");
                //    Logger.Error(ex.StackTrace);
                //}
            }
        }

        private static string ReadFileData(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл {path} не найден.");
            using var stream = new StreamReader(path);
            var data = stream.ReadToEnd();

            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException("Содержимое файла");

            return data;
        }

        private static void HandleExeption(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Произошла ошибка. Подробности в логах");
            var ex = e.ExceptionObject as Exception;
            Logger.Fatal(ex.StackTrace);
        }

        private static void CallExeption()
        {
            throw new Exception();
        }
    }
}
