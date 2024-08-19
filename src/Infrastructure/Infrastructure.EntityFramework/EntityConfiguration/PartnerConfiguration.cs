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
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            // builder.HasKey(employee => employee.Id);
            // builder.HasIndex(employee => employee.Id);
            builder
               .HasMany<PartnerPromoCodeLimit>(ppcl => ppcl.PartnerLimits)
               .WithOne(p => p.Partner)
               .HasForeignKey(p => p.PartnerId);
            //builder.HasAlternateKey(partner => partner.Name);
            builder.Property(partner => partner.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(partner => partner.Name).IsUnique();

        }
    }
}
