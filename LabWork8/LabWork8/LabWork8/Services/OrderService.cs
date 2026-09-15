// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

using LabWork8.Contexts;
using LabWork8.halpers;
using LabWork8.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork8.Services
{
    // Сервис для работы с заказами
    public class OrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public void PrintOrderDetails(int orderId)
        {
            var order = _dbContext.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
            Console.WriteLine("Id заказа: " + order.Id);
            Console.WriteLine("Итоговая цена: " + order.Total);
            Console.WriteLine("Экспрес доставка: " + (order.IsExpress ? "Да" : "Нет"));
            order.Customer.PrintEmail();
        }

        public double CalculateFinalPrice(Order order)
        {
            double vat = 0.2;
            double discount = 0;
            double minDiscountPrice = 10000;
            double discountPercent = 0.1;
            double total = order.Total;

            var calculator = new PriceCalculator(vat, discount, minDiscountPrice, discountPercent, total);

            discount = calculator.CalculateDiscount();
            return calculator.CalculatePrice();
        }

        
    }
}
