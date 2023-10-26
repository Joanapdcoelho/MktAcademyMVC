using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; } //tem um customer

        public OrderStatus OrderStatus { get; set; }//tem uma encomenda

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } //liga com OrderDetail muitas encomendas de Courses

        public virtual Customer Customer { get; set; }//liga com o customer que fez a encomenda

    }
}