// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

namespace LabWork8.Models
{
    // Класс Order (заказ)
    public class Order
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
