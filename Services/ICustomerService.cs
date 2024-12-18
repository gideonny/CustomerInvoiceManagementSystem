using Invoicing.Entities;
using CustomerInvoiceManagementSystem.Entities;
namespace CustomerInvoiceManagementSystem.Services
{
    public interface ICustomerService
    {
        IEnumerable<IGrouping<char, Customer>> GroupCustomersAlphabetically();
        void AddCustomer(Customer customer);
        void EditCustomer(int customerId, Customer updatedCustomer);
        void SoftDeleteCustomer(int customerId);
        void UndoDeleteCustomer(int customerId);

        void AddInvoice(Invoice invoice);
        void AddInvoiceLineItem(int invoiceId, InvoiceLineItem invoiceLineItem);
    }
}
