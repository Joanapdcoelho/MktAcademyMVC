using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Employee
    {
        [Key]
        
        public int EmployeeID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }//guarda o Cartão de cidadão

        public virtual DocumentType DocumentType { get; set; }  //efetua a ligação por isso é propriedade virtual

    }
}