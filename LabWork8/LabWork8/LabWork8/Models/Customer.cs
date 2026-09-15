// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

namespace LabWork8.Models
{
    // Класс Customer (клиент)
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email;
        public List<Order> Orders { get; set; }

        public void PrintEmail() 
            => Console.WriteLine($"Элю почта: {Email}");
    }
}
