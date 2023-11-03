using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class SupplierCourse
    {
        [Key]
        public int SupplierCourseID { get; set; }

        public int SupplierID { get; set;}

        public int CourseID { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Course Course { get; set;}
    }
}