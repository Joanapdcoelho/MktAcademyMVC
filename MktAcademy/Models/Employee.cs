﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace MktAcademy.Models
{
    public class Employee
    {
        [Key]
        
        public int EmployeeID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must insert a {0}")]        
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must insert a {0}")]        
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Required (ErrorMessage = "You have to enter a date of birth!")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You have to enter an email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must enter a {0}")]
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }//guarda o Cartão de cidadão

        [NotMapped] //não é colocado na base de dados serve apenas para cálculo
        public int Age
        {
            get
            {
                var myAge = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth > DateTime.Now.AddYears(-myAge))
                {
                    myAge--;
                }
                return myAge;
            }

        }

        public virtual DocumentType DocumentType { get; set; }  //efetua a ligação por isso é propriedade virtual

    }
}