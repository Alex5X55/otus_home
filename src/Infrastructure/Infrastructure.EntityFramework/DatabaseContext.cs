using System;
using Infrastructure.EntityFramework.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Сотрудники.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Клиенты.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// Партнеры.
        /// </summary>
        public DbSet<Partner> Partners { get; set; }

        /// <summary>
        /// Лимиты.
        /// </summary>
        public DbSet<PartnerPromoCodeLimit> PartnerPromoCodeLimits { get; set; }

        /// <summary>
        /// Промокоды.
        /// </summary>
        public DbSet<PromoCode> PromoCodes { get; set; }

        /// <summary>
        /// Предпочтения.
        /// </summary>
        public DbSet<Preference> Preferences { get; set; }

        /// <summary>
        /// Предпочтения рользователей.
        /// </summary>
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }

        /// <summary>
        /// Промокоды пользователей.
        /// </summary>
        public DbSet<CustomerPromoCode> CustomerPromoCode { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated(); //Migrate(); 


            //AddRange(FakeDataFactory.Roles);
            //AddRange(FakeDataFactory.Employees);

            //SaveChanges();
        }

        public void Initialize()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated(); 
            /////Database.Migrate(); 

            //AddRange(FakeDataFactory.Roles);
            //AddRange(FakeDataFactory.Preferences);
            //AddRange(FakeDataFactory.Employees);
            //AddRange(FakeDataFactory.Customers);
            //AddRange(FakeDataFactory.PromoCodes);
            
            //SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .ApplyConfiguration(new RoleConfiguration())
            .ApplyConfiguration(new PreferenceConfiguration())
            .ApplyConfiguration(new EmployeeConfiguration())
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new PromoCodeConfiguration())
            .ApplyConfiguration(new CustomerPreferencesConfiguration())
            .ApplyConfiguration(new CustomerPromoCodeConfiguration())
            .ApplyConfiguration(new PartnerConfiguration())
            .ApplyConfiguration(new PartnerPromoCodeLimitConfiguration());


            base.OnModelCreating(modelBuilder);
            }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }



    }
}
