using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class CustomerPreferencesConfiguration : IEntityTypeConfiguration<CustomerPreference>
    {
        public void Configure(EntityTypeBuilder<CustomerPreference> builder) 
        {
            builder.HasKey(cp => new { cp.CustomerId, cp.PreferenceId });

            builder.HasOne(cp => cp.Customer)
                .WithMany(c => c.Preferences)
                .HasForeignKey(cp => cp.CustomerId);

            builder.HasOne(cp => cp.Preference)
                .WithMany()
                .HasForeignKey(cp => cp.PreferenceId);
        }
    }
}
