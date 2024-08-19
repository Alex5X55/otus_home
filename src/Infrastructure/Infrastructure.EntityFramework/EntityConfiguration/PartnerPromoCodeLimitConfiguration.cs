using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class PartnerPromoCodeLimitConfiguration : IEntityTypeConfiguration<PartnerPromoCodeLimit>
    {
        public void Configure(EntityTypeBuilder<PartnerPromoCodeLimit> builder)
        {
            // builder.HasKey(partnerPromoCodeLimit => partnerPromoCodeLimit.Id);
            // builder.HasIndex(partnerPromoCodeLimit => partnerPromoCodeLimit.Id);
            //builder
            //   .HasOne(partnerPromoCodeLimit => partnerPromoCodeLimit.Partner)
            //   .WithMany()
            //   .HasForeignKey(partnerPromoCodeLimit => partnerPromoCodeLimit.PartnerId);

            //builder.HasAlternateKey(partnerPromoCodeLimit => partnerPromoCodeLimit.Name);
            //builder.Property(role => role.Name).HasMaxLength(100).IsRequired();
            //builder.Property(role => role.Description).HasMaxLength(100);
        }
    }
}
