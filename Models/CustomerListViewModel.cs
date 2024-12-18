using Invoicing.Entities;
using CustomerInvoiceManagementSystem.Entities;
namespace CustomerInvoiceManagementSystem.Models
{
    public class CustomerListViewModel
    {
        public string SelectedGroup { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
