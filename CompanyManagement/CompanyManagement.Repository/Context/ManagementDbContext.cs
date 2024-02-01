using CompanyManagement.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository.Context
{
    public class ManagementDbContext : DbContext
    {
        public DbSet<Company> Company {  get; set; }
        public DbSet<Industry> Industry { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STR"));
            builder.LogTo(Console.WriteLine);

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>().ToTable("company");
            builder.Entity<Company>().HasKey(x => x.Id);
            builder.Entity<Company>().Property(x => x.Id).HasColumnName("id");
            builder.Entity<Company>().Property(x => x.ParentCompany).HasColumnName("parent_company").IsRequired().HasDefaultValue("None");
            builder.Entity<Company>().Property(x => x.City).HasColumnName("city").HasMaxLength(50);
            builder.Entity<Company>().Property(x => x.IndustryId).HasColumnName("industry_id").IsRequired();
            builder.Entity<Company>().Property(x => x.CompanyName).HasColumnName("company_name").IsRequired().HasMaxLength(50);
            builder.Entity<Company>().Property(x => x.CompanyNo).HasColumnName("company_no").UseSerialColumn();
            builder.Entity<Company>().Property(x => x.Level).HasColumnName("level").IsRequired().HasDefaultValue(0);
            builder.Entity<Company>().Property(x => x.TotalEmployees).IsRequired();

            builder.Entity<Industry>().ToTable("industry");
            builder.Entity<Industry>().HasKey(x => x.Id);
            builder.Entity<Industry>().Property(x => x.Id).HasColumnName("id");
            builder.Entity<Industry>().Property(x => x.IndustryName).HasColumnName("industry_name").IsRequired();

            //relationships
            builder.Entity<Company>().HasOne(x => x.Industry).WithMany(x => x.Companys).HasForeignKey(x => x.IndustryId).IsRequired();

            //seed data
            builder.Entity<Industry>().HasData(
                new Industry { Id = 1, IndustryName = "Meat processing" },
                new Industry { Id = 2, IndustryName = "Gardening and landscaping" },
                new Industry { Id = 3, IndustryName = "IT services" },
                new Industry { Id = 4, IndustryName = "Aerospace technology" },
                new Industry { Id = 5, IndustryName = "Consumer electronics" }
            );
            base.OnModelCreating(builder);
        }

    }
}
