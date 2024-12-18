using Invoicing.Entities;
using Microsoft.EntityFrameworkCore;
using CustomerInvoiceManagementSystem.Entities;
using CustomerInvoiceManagementSystem.Models;

namespace CustomerInvoiceManagementSystem.Services
{
    public class InvoiceService
    {
        private readonly CustomerInvoiceManagementDbContext _customerInvoiceManagementDbContext;

        public InvoiceService(CustomerInvoiceManagementDbContext customerInvoiceManagementDbContext)
        {
            _customerInvoiceManagementDbContext = customerInvoiceManagementDbContext;
        }

        public InvoiceViewModel GetInvoicesByCustomerId(int customerId, int? selectedInvoiceId, string group)
        {
            if (string.IsNullOrEmpty(group))
            {
                group = "A-E"; // default to group A-E
            }
            var customer = _customerInvoiceManagementDbContext.Customers
                .Include(c => c.Invoices)
                .ThenInclude(i => i.PaymentTerms)
                .Include(c => c.Invoices)
                .ThenInclude(i => i.InvoiceLineItems)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null) return null;

            var currentInvoice = customer.Invoices.FirstOrDefault(i => i.InvoiceId == selectedInvoiceId)
                     ?? customer.Invoices.FirstOrDefault();
            //fetch payment terms
            var paymentTerms = _customerInvoiceManagementDbContext.Invoices
                .Include(i => i.PaymentTerms)
                .FirstOrDefault(i => i.InvoiceId == currentInvoice.InvoiceId)
                ?.PaymentTerms;
            
            //fetch payment terms description
            var paymentTermsDescription = paymentTerms?.Description ??
                _customerInvoiceManagementDbContext.PaymentTerms
                .FirstOrDefault(pt => pt.PaymentTermsId == currentInvoice.PaymentTermsId)
                ?.Description
                ?? string.Empty;

            //fetch payment terms dueDays
            var dueDays = paymentTerms?.DueDays ??
                _customerInvoiceManagementDbContext.PaymentTerms
                .FirstOrDefault(pt => pt.PaymentTermsId == currentInvoice.PaymentTermsId)
                ?.DueDays
                ?? 0;


            return new InvoiceViewModel
            {
                CustomerId = customerId,
                CustomerName = customer.Name,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                City = customer.City,
                ProvinceOrState = customer.ProvinceOrState,
                ZipOrPostalCode = customer.ZipOrPostalCode,
                ContactFirstName = customer.ContactFirstName,
                ContactLastName = customer.ContactLastName,
                PaymentTermsDescription = paymentTermsDescription,
                DueDays = dueDays,
                SelectedGroup = group,
                Group = group,
                PaymentTermsList = _customerInvoiceManagementDbContext.PaymentTerms.ToList(),
                Invoices = customer.Invoices?.Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceDate = i.InvoiceDate,
                    InvoiceDueDate = i.InvoiceDueDate,
                    PaymentTotal = i.PaymentTotal,
                    PaymentDate = i.PaymentDate,
                    InvoiceLineItems = i.InvoiceLineItems?.Select(ili => new InvoiceLineItemDto
                    {
                        InvoiceLineItemId = ili.InvoiceLineItemId,
                        Amount = ili.Amount,
                        Description = ili.Description
                    }).ToList()
                }).ToList(),
                CurrentInvoice = currentInvoice != null ? new InvoiceDto
                {
                    InvoiceId = currentInvoice.InvoiceId,
                    InvoiceDate = currentInvoice.InvoiceDate,
                    InvoiceDueDate = currentInvoice.InvoiceDueDate,
                    PaymentTotal = currentInvoice.PaymentTotal,
                    PaymentDate = currentInvoice.PaymentDate,
                    InvoiceLineItems = currentInvoice.InvoiceLineItems?.Select(ili => new InvoiceLineItemDto
                    {
                        InvoiceLineItemId = ili.InvoiceLineItemId,
                        Amount = ili.Amount,
                        Description = ili.Description
                    }).ToList(),
                } : null,
                SelectedInvoiceId = currentInvoice?.InvoiceId,
                SelectedInvoiceLineItems = currentInvoice?.InvoiceLineItems?.Select(ili => new InvoiceLineItemDto
                {
                    InvoiceLineItemId = ili.InvoiceLineItemId,
                    Amount = ili.Amount,
                    Description = ili.Description
                }).ToList() ?? new List<InvoiceLineItemDto>(),
                SelectedInvoiceTotal = currentInvoice?.InvoiceLineItems?.Sum(ili => ili.Amount ?? 0) ?? 0
            };
        }

        public void AddInvoiceLineItem(int customerId, int invoiceId, string description, decimal amount)
        {
            var customer = _customerInvoiceManagementDbContext.Customers
                .Include(c => c.Invoices)
                .ThenInclude(c => c.InvoiceLineItems)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null) throw new KeyNotFoundException("Customer not found.");

            var invoice = customer.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            if (invoice == null) throw new KeyNotFoundException("Invoice not found.");

            var newLineItem = new InvoiceLineItem
            {
                Description = description,
                Amount = (double)amount
            };

            invoice.InvoiceLineItems.Add(newLineItem);
            invoice.PaymentTotal += (double)amount;

            _customerInvoiceManagementDbContext.SaveChanges();
        }

        public Invoice AddInvoice(InvoiceViewModel model)
        {
            if (model.InvoiceDate == null)
                throw new ArgumentException("Invoice date is required.");

            var newInvoice = new Invoice
            {
                CustomerId = model.CustomerId,
                InvoiceDate = model.InvoiceDate,
                PaymentTermsId = model.SelectedPaymentTermId ?? 0,
                PaymentTotal = 0,
                PaymentDate = null,
            };

            _customerInvoiceManagementDbContext.Add(newInvoice);
            _customerInvoiceManagementDbContext.SaveChanges();

            return newInvoice;
        }
    }
}
