using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
           // builder.HasKey(employee => employee.Id);
           // builder.HasIndex(employee => employee.Id);
            builder
                .HasOne(employee => employee.Role)
                .WithMany()
                .HasForeignKey(employee => employee.RoleId);

            builder.Property(employee => employee.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(employee => employee.LastName).HasMaxLength(50).IsRequired();
            builder.Property(employee => employee.Email).HasMaxLength(30).IsRequired();
            builder.HasIndex(employee => employee.Email).IsUnique();

        }
    }
}