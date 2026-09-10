using System.Text.RegularExpressions;

double PowerNumber(double number, int power)
{
    if (number == 0 && power < 0)
        throw new ArgumentException("Деление на ноль");

    return Math.Round(Math.Pow(number, power), 3);
}

bool TestPassword(string password)
{
    string regex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,30}$";
    return Regex.Match(password, regex).Success;
}

Console.WriteLine(PowerNumber(2, 0));
Console.WriteLine(PowerNumber(2, 3));
Console.WriteLine(PowerNumber(4, -2));
Console.WriteLine(PowerNumber(0, 3));
try
{
    Console.WriteLine(PowerNumber(0, -1));
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine(PowerNumber(-2.5, 3));
Console.WriteLine(PowerNumber(-2.5, 2));
Console.WriteLine(PowerNumber(-2.5, -3));
Console.WriteLine(PowerNumber(-2.5, -2));
Console.WriteLine(PowerNumber(-2.5, 0));
Console.WriteLine(PowerNumber(0, 0));

Console.WriteLine(TestPassword("Pass123!"));
Console.WriteLine(TestPassword("123456789012345678901234567Aa!"));
Console.WriteLine(TestPassword("123456789012345678901234567Aa!1"));
Console.WriteLine(TestPassword("123!Aa1"));
Console.WriteLine(TestPassword("passW123123"));
Console.WriteLine(TestPassword("passW!!!!!!!!"));
Console.WriteLine(TestPassword("pass123!!!!!!!!"));
Console.WriteLine(TestPassword("PASS123!!!!!!!!"));
Console.WriteLine(TestPassword("Пароль!123123123123"));