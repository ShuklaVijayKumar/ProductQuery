using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductQuery.Models
{
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QueryID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(8, MinimumLength = 3)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(14, MinimumLength = 10)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}