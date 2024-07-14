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
    public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
    {
        public void Configure(EntityTypeBuilder<Preference> builder)
        {
            // builder.HasKey(preference => preference.Id);
            // builder.HasIndex(preference => preference.Id);
            builder.HasAlternateKey(preference => preference.Name);
            builder.Property(preference => preference.Name).HasMaxLength(100).IsRequired();
        }
    }
}
