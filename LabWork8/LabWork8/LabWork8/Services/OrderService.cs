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
            Order? order = GetOrderById(orderId);
            PrintOrderData(order.Id);
            PrintOrderData(order.Total);
            PrintOrderData(order.IsExpress);
            order.Customer.PrintEmail();
        }

        private void PrintOrderData(int id)
            => Console.WriteLine("Id заказа: " + id);

        private void PrintOrderData(double total)
            => Console.WriteLine("Итоговая цена: " + total);

        private void PrintOrderData(bool isExpress) 
            => Console.WriteLine("Экспрес доставка: " + (isExpress ? "Да" : "Нет"));

        private Order? GetOrderById(int orderId)
            => _dbContext.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);

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
