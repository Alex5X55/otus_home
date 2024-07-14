using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role> 
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
           // builder.HasKey(role => role.Id);
           // builder.HasIndex(role => role.Id);
            builder.HasAlternateKey(role => role.Name);
            builder.Property(role => role.Name).HasMaxLength(100).IsRequired();
            builder.Property(role => role.Description).HasMaxLength(100);
        }
    }
}
