using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class CustomerPromoCodeConfiguration : IEntityTypeConfiguration<CustomerPromoCode>
    {
        public void Configure(EntityTypeBuilder<CustomerPromoCode> builder)
        {
            builder.HasKey(cp => new { cp.CustomerId, cp.PromoCodeId });

            builder.HasOne(cp => cp.Customer)
                .WithMany(c => c.PromoCodes)
                .HasForeignKey(cp => cp.CustomerId);

            builder.HasOne(cp => cp.PromoCode)
                .WithMany()
                .HasForeignKey(cp => cp.PromoCodeId);
        }
    }
}
