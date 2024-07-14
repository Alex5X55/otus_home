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
    internal class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            // builder.HasKey(promoCode => promoCode.Id);
            // builder.HasIndex(promoCode => promoCode.Id);
            builder
                .HasOne(promoCode => promoCode.Preference)
                .WithMany()
                .HasForeignKey(promoCode => promoCode.PreferenceId);
            builder
                .HasOne(promoCode => promoCode.PartnerManager)
                .WithMany()
                .HasForeignKey(promoCode => promoCode.PartnerManagerId);
            builder
                .HasOne(promoCode => promoCode.Customer)
                .WithMany()
                .HasForeignKey(promoCode => promoCode.CustomerId);


            builder.Property(promoCode => promoCode.Code).HasMaxLength(50).IsRequired();
            builder.Property(promoCode => promoCode.ServiceInfo).HasMaxLength(100).IsRequired();
            builder.Property(promoCode => promoCode.PartnerName).HasMaxLength(50).IsRequired();

        }
    }
}
