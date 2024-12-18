using Microsoft.AspNetCore.Mvc;
using CustomerInvoiceManagementSystem.Models;
using CustomerInvoiceManagementSystem.Services;

namespace CustomerInvoiceManagementSystem.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult GetAllInvoicesRequestByCustomerId(int customerId, int? selectedInvoiceId, string group)
        {
            var model = _invoiceService.GetInvoicesByCustomerId(customerId, selectedInvoiceId, group);
            if (model == null) return NotFound();

            return View("Invoices", model);
        }

        [HttpPost]
        public IActionResult AddInvoiceLineItem(int customerId, int invoiceId, string description, decimal amount, string group)
        {
            try
            {
                
                _invoiceService.AddInvoiceLineItem(customerId, invoiceId, description, amount);
                return RedirectToAction("GetAllInvoicesRequestByCustomerId", new { customerId, selectedInvoiceId = invoiceId, group });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddInvoice(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors) Console.WriteLine(error);

                return View("Invoices", model);
            }

            var newInvoice = _invoiceService.AddInvoice(model);
            TempData["Group"] = model.Group;

            return RedirectToAction("GetAllInvoicesRequestByCustomerId", new { customerId = model.CustomerId, selectedInvoiceId = newInvoice.InvoiceId, group = model.Group });
        }
    }
}
