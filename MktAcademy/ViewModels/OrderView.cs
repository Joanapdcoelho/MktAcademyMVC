using MktAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MktAcademy.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }

        public List<CourseOrder> Courses { get; set; }
    }
}