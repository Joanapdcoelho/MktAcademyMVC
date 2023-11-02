using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MktAcademy.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Student(string firstName, string lastName, int age) 
        {
            firstName = FirstName;
            lastName = LastName;
            age = Age;
        }

        public override string ToString()
        {
            return $"Name:{FirstName} Last Name:{LastName} Age:{Age}";
        }


    }
}