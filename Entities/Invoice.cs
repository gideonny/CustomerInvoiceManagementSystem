namespace Invoicing.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }

        // FK:
        public int PaymentTermsId { get; set; }
        
        //Navigation property: one invoice has one payment terms
        public PaymentTerms? PaymentTerms { get; set; }

        // FK:
        public int CustomerId { get; set; }

        //Navigation property: one invoice belongs to one customer
        public Customer? Customer { get; set; }

        //navigation property: one invoice has multiple InvoiceLineItems
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }

    
}
