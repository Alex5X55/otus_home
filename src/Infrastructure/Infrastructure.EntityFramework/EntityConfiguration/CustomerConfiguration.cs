using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // builder.HasKey(customer => customer.Id);
            // builder.HasIndex(customer => customer.Id);
           // builder
            //     .HasOne(customer => customer.Promocode)
            //     .WithMany()
            //     .HasForeignKey(customer => customer.PromocodeId);

            builder.Property(customer => customer.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(customer => customer.LastName).HasMaxLength(50).IsRequired();
            builder.Property(customer => customer.Email).HasMaxLength(30).IsRequired();
            builder.HasIndex(customer => customer.Email).IsUnique();
        }

    }
}
