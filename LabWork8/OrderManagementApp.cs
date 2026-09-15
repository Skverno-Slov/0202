// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementApp
{
    // Контекст базы данных
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=OrderManagement.db");
        }
    }

    // Класс Customer (клиент)
    public class Customer
    {
        private string _name;

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        };
        public string Email;
        public List<Order> Orders { get; set; }
    }

    // Класс Order (заказ)
    public class Order
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public void PrintCustomerInfo(int customerId)
        {
            var customer = _dbContext.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == customerId);
            Console.WriteLine("Customer: " + customer.Name);
            Console.WriteLine("Email: " + customer.Email);
        }
    }

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
    }

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
            Console.WriteLine("Order Id: " + order.Id);
            Console.WriteLine("Total: " + order.Total);
            Console.WriteLine("Express Shipping: " + (order.IsExpress ? "Yes" : "No"));
            Console.WriteLine("Customer Email: " + order.Customer.Email);
        }

        public double CalculateFinalPrice(Order order)
        {
			double vat = 0.2; // НДС
			// скидка 10% при заказе от 10000
            double discount = 0;
            double minDiscountPrice = 10000;
            double discountPercent = 0.1;

			discount = order.Total > minDiscountPrice 
                ? order.Total * discountPercent : 0;

			// итоговая цена
			return order.Total - discount + (order.Total * vat);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new AppDbContext();
            dbContext.Database.EnsureCreated();

            var customerService = new CustomerService(dbContext);
            var orderService = new OrderService(dbContext);

            var customer = new Customer { Name = "Alice", Email = "alice@example.com" };
            customerService.AddCustomer(customer);

            var order = new Order { Total = 1200, IsExpress = true, Customer = customer };
            orderService.AddOrder(order);

            customerService.PrintCustomerInfo(customer.Id);
            orderService.PrintOrderDetails(order.Id);

            Console.WriteLine("Final Price: " + orderService.CalculateFinalPrice(order));
        }
    }
}
