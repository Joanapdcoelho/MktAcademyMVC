using MktAcademy.Models;
using System.ComponentModel.DataAnnotations;

namespace MktAcademy.ViewModels
{
    public class CourseOrder : Course
    {


        [DataType(DataType.Currency)]//tipo moeda
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]//formatar 2 casas decimais, mas guarda com o formato que está na tabela
        [Required(ErrorMessage = "You must insert a {0}")]
        [Display(Name = "Quantity")]
        public float Quantity { get; set; }

        [DataType(DataType.Currency)]//tipo moeda
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]//formatar 2 casas decimais, mas guarda com o formato que está na tabela
        [Required(ErrorMessage = "You must insert a {0}")]
        [Display(Name = "Value")]
        public decimal Value { get { return Price * (decimal)Quantity; } }


    }
}