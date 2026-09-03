using System.Diagnostics;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Введите первое слагаемое: ");
                string? x;
                Debug.WriteLine("Запрошен ввод X (D)");
                Trace.WriteLine("Запрошен ввод X (T)");
                x = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(x))
                    throw new ArgumentNullException("Некорректный ввод X");

                if (x.Trim().ToLower() == "exit")
                    break;

                if (!int.TryParse(x, out int intX))
                    throw new ArgumentException("Некорректный ввод X");

                Console.WriteLine("Введите второе слагаемое: ");

                string? y;
                Debug.WriteLine("Запрошен ввод Y (D)");
                Trace.WriteLine("Запрошен ввод Y (T)");
                y = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(y))
                    throw new ArgumentNullException("Некорректный ввод y");

                if (y.Trim().ToLower() == "exit")
                    break;

                if (!int.TryParse(y, out int intY))
                    throw new ArgumentException("Некорректный ввод Y");

                Debug.WriteLine("Сложение выполняется (D)");
                Trace.WriteLine("Сложение выполняется (T)");
                int result = intX + intY;
                Debug.WriteLine("Вывод результата (D)");
                Trace.WriteLine("Вывод результата (T)");
                Console.WriteLine($"Сумма: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //var price = 1000.0;
        //var corDisc = 0.3;
        //var unCorDisc = 2.3;
        //Console.WriteLine($"Корректные (p = {price}, d = {corDisc}){CalculateDiscount(price, corDisc)}"); 
        //Console.WriteLine($"Некорректные (p = {price}, d = {unCorDisc}){CalculateDiscount(price, unCorDisc)}"); 
        //Console.WriteLine($"Некорректные (p = -1, d = {corDisc}){CalculateDiscount(-1, unCorDisc)}"); 

        MethodA();
    }

    private static double CalculateDiscount(double price, double discountRate)
    {
        Debug.Assert(price > 0);
        Debug.Assert(discountRate > 0 && discountRate <= 1);
        Debug.Assert(discountRate > 0 && discountRate <= 1);

        double result = price * discountRate;

        Debug.Assert(result <= price);
        return result;
    }

    private static void MethodA()
    {
        Console.WriteLine("Метод А работает...");
        MethodB();
    }

    private static void MethodB()
    {
        Console.WriteLine("Метод B работает...");
        MethodC();
    }
    private static void MethodC()
    {
        try
        {
            Console.WriteLine("Метод C работает...");
            throw new DivideByZeroException();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\StackTrace.txt", ex.StackTrace);
        }
    }
}