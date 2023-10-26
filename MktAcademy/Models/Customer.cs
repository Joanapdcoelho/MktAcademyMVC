using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must insert a {0}")]
        [Display(Name = "First Name")]
        public string CustomerFirstName { get; set; }

        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must insert a {0}")]
        [Display(Name = "Last Name")]
        public string CustomerLastName { get; set; }

        [Display(Name ="Name")]
        [NotMapped]
        public string Name { get { return $"{CustomerFirstName} {CustomerLastName}"; } }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must enter a  {0}!")]
        [Display(Name = "Phone Number")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} digits", MinimumLength = 9)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter an  {0}!")]
        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter an  {0}!")]
        [Display(Name = "Document number")]
        [StringLength(20, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 5)]
        public string Document {  get; set; }

        [Required(ErrorMessage ="You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage="You must enter a {0}")]
        [Display(Name ="Document Type")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }//liga com as orders o cliente faz orders

    }
}