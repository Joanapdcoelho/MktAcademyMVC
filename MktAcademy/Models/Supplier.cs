using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required (ErrorMessage = "You must enter a  {0} for the supplier!")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Display(Name ="Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Contact First Name")]
        [Required(ErrorMessage = "You must enter a  {0} for the supplier contact!")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        public string ContactFirstName {  get; set; }

        [Display(Name = "Contact Last Name")]
        [Required(ErrorMessage = "You must enter a  {0} for the supplier contact!")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        public string ContactLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must enter a  {0}!")]
        [Display(Name ="Phone Number")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 9)]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "You must enter an  {0}!")]
        [Display(Name ="Address")]
        [StringLength(100, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<SupplierCourse> SupplierCourses { get; set; }
    }
}