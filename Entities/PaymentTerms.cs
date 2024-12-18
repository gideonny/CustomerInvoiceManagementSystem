namespace Invoicing.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }

        public string Description { get; set; } = null!;

        public int DueDays { get; set; }

        //Navigation property: one paymentTerms applies to multiple Invocies
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
