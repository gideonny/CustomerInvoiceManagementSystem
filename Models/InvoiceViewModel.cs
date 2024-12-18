using Invoicing.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerInvoiceManagementSystem.Models
{
    
    public class InvoiceViewModel
    {
        // Customer Details
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? ProvinceOrState { get; set; }
        public string? ZipOrPostalCode { get; set; }
        public string? ContactName => $"{ContactFirstName} {ContactLastName}".Trim();
        public string? ContactFirstName { get; set; }
        public string? ContactLastName { get; set; }
        public string Group { get; set; }
        public List<string> Groups { get; set; } = new();
        public string SelectedGroup { get; set; }
        // Payment Terms
        public string PaymentTermsDescription { get; set; } = string.Empty;  // Retain this for displaying in the form, but consider it as optional
        public int? SelectedPaymentTermId { get; set; }  // Holds the selected PaymentTermId for the dropdown
        public List<PaymentTerms> PaymentTermsList { get; set; } = new(); // Dropdown options for payment terms
        public DateTime? InvoiceDate { get; set; }
        public int DueDays { get; set; }

        // Invoices
        public List<InvoiceDto> Invoices { get; set; } = new();

        // Currently Selected Invoice
        public InvoiceDto? CurrentInvoice { get; set; }

        // Selected Invoice Id for view state
        public int? SelectedInvoiceId { get; set; } // Add this property

        // Selected Invoice Line Items
        public List<InvoiceLineItemDto> SelectedInvoiceLineItems { get; set; } = new(); // Add this property

        // Total for the Selected Invoice
        public double SelectedInvoiceTotal { get; set; }  // Add this property
    }

    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public double? PaymentTotal { get; set; } = 0.0;
        public double? AmountPaid { get; set; } = 0.0;
        public DateTime? PaymentDate { get; set; }
        public string PaymentTerms { get; set; }
        

        // Line Items for the Current Invoice
        public List<InvoiceLineItemDto> InvoiceLineItems { get; set; } = new();

        // Calculated Total Amount for the Invoice
        public double TotalAmount => InvoiceLineItems?.Sum(li => li.Amount ?? 0) ?? 0;
    }

    public class InvoiceLineItemDto
    {
        public int InvoiceLineItemId { get; set; }
        public double? Amount { get; set; }
        public string? Description { get; set; }

    }
}

