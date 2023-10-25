using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        
        [Required(ErrorMessage = "You must insert a {0}")]
        public string Name { get; set; }
        

        [StringLength(30,ErrorMessage = "The {0} should be between {2} and {1} characters",MinimumLength = 3)]
        [Required (ErrorMessage = "You must insert a {0}")]
        public string Description { get; set; }
        

        [DataType(DataType.Currency)]//tipo moeda
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]//formatar 2 casas decimais, mas guarda com o formato que está na tabela
        [Required (ErrorMessage = "You must insert a {0}")]
        public decimal Price { get; set; }
         

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Buy")]
        public DateTime? LastBuy { get; set; }

        [DataType (DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float? Stock { get; set; }

        public string Area { get; set; }

        [DataType (DataType.MultilineText)]
        public string Remarks { get; set; }    
        
        public virtual ICollection<SupplierCourse> SupplierCourses { get; set; }

    }
}