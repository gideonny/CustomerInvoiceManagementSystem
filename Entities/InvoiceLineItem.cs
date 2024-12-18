namespace Invoicing.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        // FK:
        public int? InvoiceId { get; set; }
        //navigation property: one InvoiceLineItem belongs to one Invoice
        public Invoice? Invoice { get; set; }
    }
}
