using Invoicing.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerInvoiceManagementSystem.Entities;
using CustomerInvoiceManagementSystem.Models;
using CustomerInvoiceManagementSystem.Services;

namespace CustomerInvoiceManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("customers")]
        public IActionResult GetAllCustomers(string group)
        {
            var customers = _customerService.GetCustomersByGroup(group);

            var viewModel = new CustomerListViewModel
            {
                Customers = customers,
                SelectedGroup = group,
                Groups = new[] { "A-E", "F-K", "L-R", "S-Z" }
            };

            return View("Customers", viewModel);
        }
        [HttpGet("customer/add-request")]
        public IActionResult GetAddNewCustomerRequest(string group)
        {
            ViewData["CurrentGroup"] = group;
            return View("AddCustomer", new Customer());
        }

        [HttpGet("/customer/{id}edit-request")]
        public IActionResult GetEditCustomerRequestById(int id, string group)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewData["CurrentGroup"] = group;
            return View("EditCustomer", customer);
        }

        [HttpPost("customers/save")]
        public IActionResult SaveCustomer(Customer customer, string group, int id)
        {
            if (ModelState.IsValid)
            {
                _customerService.SaveCustomer(customer);
                TempData["LastActionMessage"] = "Customer has been saved succesfully";

                return RedirectToAction("GetAllCustomers", "Customer", new { group = group });
            }

            ViewData["CurrentGroup"] = group;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors) Console.WriteLine(error);
            }
            return View("EditCustomer", customer);
        }

        [HttpPost("customers/add")]
        public IActionResult AddCustomer(Customer customer, string group, int id)
        {
            if (ModelState.IsValid)
            {
                _customerService.SaveCustomer(customer);
                TempData["LastActionMessage"] = "Customer has been saved succesfully";

                return RedirectToAction("GetAllCustomers", "Customer", new { group = group });
            }

            ViewData["CurrentGroup"] = group;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors) Console.WriteLine(error);
            }
            return View("AddCustomer", customer);
        }

        public IActionResult SoftDeleteCustomer(int id, string group)
        {
            var customer = _customerService.SoftDeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            //Store the deleted customer's information
            TempData["UndoDelete"] = customer.CustomerId;
            TempData["NameOfDeletedCustomer"] = customer.Name;

            return RedirectToAction("GetAllCustomers", "Customer", new {group = group});
        }

        public IActionResult UndoDelete(int id, string group)
        {
            _customerService.UndoDelete(id);
            return RedirectToAction("GetAllCustomers", "Customer", new { group = group, });
        }
    }
}
