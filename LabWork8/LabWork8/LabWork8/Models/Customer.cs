// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

namespace LabWork8.Models
{
    // Класс Customer (клиент)
    public class Customer
    {
        private string _name;
        public int Id { get; set; }
        public string Name { 
            get => _name;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }
        public string Email;
        public List<Order> Orders { get; set; }

        public void PrintEmail() 
            => Console.WriteLine($"Эл. почта: {Email}");
    }
}
