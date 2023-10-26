using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MktAcademy.Data
{
    public class MktAcademyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MktAcademyContext() : base("name=MktAcademyContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //Remover a regra de apagar em cascata (apagar os registos relacionados)
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<MktAcademy.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<MktAcademy.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<MktAcademy.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<MktAcademy.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<MktAcademy.Models.Customer> Customers { get; set; }
    }
}
