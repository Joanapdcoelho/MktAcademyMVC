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

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Area { get; set; }

        public string Remarks { get; set; }
    }
}