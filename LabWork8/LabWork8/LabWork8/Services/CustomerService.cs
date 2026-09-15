// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

using LabWork8.Contexts;
using LabWork8.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork8.Services
{
    // Сервис для работы с клиентами
    public class CustomerService
    {
        private readonly AppDbContext _dbContext;

        public CustomerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public void PrintCustomerInfo(int customerId)
        {
            var customer = _dbContext.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == customerId);
            Console.WriteLine("Customer: " + customer.Name);
            Console.WriteLine("Email: " + customer.Email);
        }
    }
}
