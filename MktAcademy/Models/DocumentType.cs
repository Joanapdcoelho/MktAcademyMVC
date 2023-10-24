using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }

        [Display (Name = "Document Description")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }//efetua a ligação com os empregados e o tipo de documento que é registado
    }
}