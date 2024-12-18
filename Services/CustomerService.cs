using Invoicing.Entities;
using Microsoft.EntityFrameworkCore;
using CustomerInvoiceManagementSystem.Entities;
using CustomerInvoiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerInvoiceManagementSystem.Services
{
    public class CustomerService
    {
        private readonly CustomerInvoiceManagementDbContext _customerInvoiceManagementDbContext;

        public CustomerService(CustomerInvoiceManagementDbContext customerInvoiceManagementDbContext)
        {
            _customerInvoiceManagementDbContext = customerInvoiceManagementDbContext;
        }

        // Get customers filtered by group
        public List<Customer> GetCustomersByGroup(string group)
        {
            var groups = new[] { "A-E", "F-K", "L-R", "S-Z" };
            if (string.IsNullOrEmpty(group))
            {
                group = groups[0]; // Default to group A-E
            }

            var customers = _customerInvoiceManagementDbContext.Customers
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToList();

            // Filter customers based on group
            switch (group)
            {
                case "A-E":
                    customers = customers.Where(c => char.ToUpper(c.Name[0]) >= 'A' && char.ToUpper(c.Name[0]) <= 'E').ToList();
                    break;
                case "F-K":
                    customers = customers.Where(c => char.ToUpper(c.Name[0]) >= 'F' && char.ToUpper(c.Name[0]) <= 'K').ToList();
                    break;
                case "L-R":
                    customers = customers.Where(c => char.ToUpper(c.Name[0]) >= 'L' && char.ToUpper(c.Name[0]) <= 'R').ToList();
                    break;
                case "S-Z":
                    customers = customers.Where(c => char.ToUpper(c.Name[0]) >= 'S' && char.ToUpper(c.Name[0]) <= 'Z').ToList();
                    break;
            }

            return customers;
        }

        // Get a customer by ID
        public Customer GetCustomerById(int id)
        {
            return _customerInvoiceManagementDbContext.Customers.Find(id);
        }

        // Add or update a customer
        public void SaveCustomer(Customer customer)
        {
            if (customer.CustomerId == 0)
            {
                _customerInvoiceManagementDbContext.Customers.Add(customer);
            }
            else
            {
                _customerInvoiceManagementDbContext.Customers.Update(customer);
            }

            _customerInvoiceManagementDbContext.SaveChanges();
        }

        // Soft delete a customer
        public Customer SoftDeleteCustomer(int id)
        {
            var customer = _customerInvoiceManagementDbContext.Customers.Find(id);
            if (customer == null)
            {
                return null;
            }

            customer.IsDeleted = true;
            _customerInvoiceManagementDbContext.Customers.Update(customer);
            _customerInvoiceManagementDbContext.SaveChanges();

            return customer;
        }

        // Undo the soft delete for a customer
        public void UndoDelete(int id)
        {
            var customer = _customerInvoiceManagementDbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                customer.IsDeleted = false;
                _customerInvoiceManagementDbContext.Customers.Update(customer);
                _customerInvoiceManagementDbContext.SaveChanges();
            }
        }
    }
}