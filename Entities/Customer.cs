using System.ComponentModel.DataAnnotations;

namespace Invoicing.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }
        [Required]
        public string? City { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}$", ErrorMessage = "Province/State must be a 2 letter province/state code")]
        public string? ProvinceOrState { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$|^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$",
        ErrorMessage = "Zip/postal Code must be in a valid US or Canadian format.")]
        public string? ZipOrPostalCode { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$",
        ErrorMessage = "Phone number must be a valid US or Canadian format.")]
        public string? Phone { get; set; }

        public string? ContactLastName { get; set; }

        public string? ContactFirstName { get; set; }
        
        public string? ContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        //navigation property: one customer can have many invoices

    public ICollection<Invoice>? Invoices { get; set; }
    }
}
