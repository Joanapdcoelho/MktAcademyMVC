using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [StringLength(30, ErrorMessage = "The {0} should be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must insert a {0}")]
        [Display(Name ="Course description")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]//tipo moeda
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]//formatar 2 casas decimais, mas guarda com o formato que está na tabela
        [Required(ErrorMessage = "You must insert a {0}")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]//tipo moeda
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]//formatar 2 casas decimais, mas guarda com o formato que está na tabela
        [Required(ErrorMessage = "You must insert a {0}")]
        public float Quantity { get; set; }

        public virtual Order Order { get; set; } //liga com a Order


    }
}