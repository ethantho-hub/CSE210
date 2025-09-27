using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSystem
{
    // -------------------- Product --------------------
    public class Product
    {
        public string Name { get; }
        public string ProductId { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public Product(string name, string productId, decimal price, int quantity)
        {
            Name = name;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public decimal TotalCost()
        {
            return Price * Quantity;
        }
    }

    // -------------------- Address --------------------
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string StateOrProvince { get; }
        public string Country { get; }

        public Address(string street, string city, string stateOrProvince, string country)
        {
            Street = street;
            City = city;
            StateOrProvince = stateOrProvince;
            Country = country;
        }

        public bool IsInUSA()
        {
            return Country.Trim().Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public string FullAddress()
        {
            return $"{Street}\n{City}, {StateOrProvince}\n{Country}";
        }
    }

    // -------------------- Customer --------------------
    public class Customer
    {
        public string Name { get; }
        public Address Address { get; }

        public Customer(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public bool LivesInUSA()
        {
            return Address.IsInUSA();
        }
    }

    // -------------------- Order --------------------
    public class Order
    {
        private readonly List<Product> _products = new List<Product>();
        public Customer Customer { get; }

        public Order(Customer customer)
        {
            Customer = customer;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public decimal TotalCost()
        {
            decimal productTotal = _products.Sum(p => p.TotalCost());
            decimal shipping = Customer.LivesInUSA() ? 5m : 35m;
            return productTotal + shipping;
        }

        public string PackingLabel()
        {
            var lines = new List<string> { "Packing Label:" };
            foreach (var p in _products)
            {
                lines.Add($"{p.Name} (ID: {p.ProductId}) x {p.Quantity}");
            }
            return string.Join(Environment.NewLine, lines);
        }

        public string ShippingLabel()
        {
            return $"Shipping Label:\n{Customer.Name}\n{Customer.Address.FullAddress()}";
        }
    }

    // -------------------- Program Entry --------------------
    class Program
    {
        static void Main()
        {
            // Create products
            var p1 = new Product("Widget", "W123", 3.00m, 5);
            var p2 = new Product("Gadget", "G456", 10.00m, 2);

            // Create address and customer
            var address = new Address("123 Main St", "Springfield", "IL", "USA");
            var customer = new Customer("Alice Smith", address);

            // Create order and add products
            var order = new Order(customer);
            order.AddProduct(p1);
            order.AddProduct(p2);

            // Output results
            Console.WriteLine(order.PackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.ShippingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Cost: ${order.TotalCost():0.00}");
        }
    }
}
