using EF.Core.Seyid.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Seyid.Contexts
{
    public class AppDbConext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SNIONM0\\SQLEXPRESS;Database=StudentGroupDb;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

    }
}
