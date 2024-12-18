using Invoicing.Entities;
using CustomerInvoiceManagementSystem.Models;

namespace CustomerInvoiceManagementSystem.Services
{
    public interface IInvoiceService
    {
        void AddInvoice(Invoice invoice);
        void AddInvoiceLineItem(int customerId, int invoiceId, InvoiceLineItem invoiceLineItem);
        InvoiceViewModel GetAllInvoicesRequestByCustomerId(int customerId, int? selectedInvoiceId, string group);
    }
}
