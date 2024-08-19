using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Umbraco.Core.Collections.TopoGraph;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
               // Roles = new List<Role>()
              //  {
              //    Roles.FirstOrDefault(x => x.Name == "Admin")  
             //   },
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",

                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
               // Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
              //  Roles = new List<Role>()
              //  {
              //      Roles.FirstOrDefault(x => x.Name == "PartnerManager")  
              //  },
               // AppliedPromocodesCount = 10
            },
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a324"),
                Email = "garic@garicmail.ru",
                FirstName = "Гарик",
                LastName = "Погосян",
                RoleId = Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
                //Roles = new List<Role>()
                //{
                  //Roles.FirstOrDefault(x => x.Name == "Admin")
                //},
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435893"),
                Email = "marik@marikmail.ru",
                FirstName = "Марик",
                LastName = "Петрасян",
                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                /*Roles = new List<Role>()
                {
                    Roles.FirstOrDefault(x => x.Name == "PartnerManager")
                },*/
                AppliedPromocodesCount = 10
            },
           
        };

        public static IEnumerable<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            }
        };

        public static IEnumerable<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<PromoCode> PromoCodes => new List<PromoCode>()
        {
            new PromoCode()
            {
                Id = Guid.Parse("A1bc5299-92d7-459f-896e-078ed53ea99b"),
                Code = "Код 1",
                ServiceInfo = "Сервис инфо 1",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                //PartnerId = 
                PartnerManagerId = Employees.Where(x => x.Id == Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895")).SingleOrDefault().Id,
                PreferenceId = Preferences.Where(x => x.Name == "Театр").SingleOrDefault().Id,
                CustomerId = Customers.Where(x => x.Id == Guid.Parse("6c8c6b1a-4349-45b0-ab31-244740aaf0f0")).SingleOrDefault().Id,
            },
            new PromoCode()
            {
                Id = Guid.Parse("A2bc5299-92d7-459f-896e-078ed53ea98b"),
                Code = "Код 2",
                ServiceInfo = "Сервис инфо 2",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                //PartnerName = "Партнер2",
                PartnerManagerId = Employees.Where(x => x.Id == Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895")).SingleOrDefault().Id,
                PreferenceId = Preferences.Where(x => x.Name == "Семья").SingleOrDefault().Id,
                CustomerId = Customers.Where(x => x.Id == Guid.Parse("bc8c6b1a-4349-45b0-ab31-244740aaf0f1")).SingleOrDefault().Id,
            }//,
           // new PromoCode()
           // {
           //     Id = Guid.Parse("A3bc5299f-92d7-459f-896e-078ed53ea97b"),

          //  }
        };

        public static IEnumerable<Customer> Customers
        {
            get
            {
                var customerId = Guid.Parse("6c8c6b1a-4349-45b0-ab31-244740aaf0f0");
                var customerId2 = Guid.Parse("bc8c6b1a-4349-45b0-ab31-244740aaf0f1");
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = customerId,
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                        //PromocodeId = PromoCodes.Where(x => x.CustomerId == customerId).SingleOrDefault().Id,
                        //TODO: Добавить предзаполненный список предпочтений
                        Preferences = new List<CustomerPreference>()
                        {
                            new CustomerPreference()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customerId, 
                                PreferenceId = Preferences.Where(x => x.Name == "Театр").SingleOrDefault().Id
                            },
                            new CustomerPreference()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customerId,
                                PreferenceId = Preferences.Where(x => x.Name == "Дети").SingleOrDefault().Id
                            }
                        },
                        PromoCodes = new List<CustomerPromoCode>()
                        {
                            new CustomerPromoCode()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customerId,
                                PromoCodeId = Guid.Parse("A1bc5299-92d7-459f-896e-078ed53ea99b")
                            }
                        }
                    },
                    new Customer()
                    {
                        Id = customerId2,
                        Email = "alex_uporov@yandex.ru",
                        FirstName = "Алексей",
                        LastName = "Упоров",
                      //  PromocodeId = PromoCodes.Where(x => x.CustomerId == customerId2).SingleOrDefault().Id,
                        //TODO: Добавить предзаполненный список предпочтений
                        Preferences = new List<CustomerPreference>()
                        {
                            new CustomerPreference()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customerId2,
                                PreferenceId = Preferences.Where(x => x.Name == "Семья").SingleOrDefault().Id
                            },
                            new CustomerPreference()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customerId2,
                                PreferenceId = Preferences.Where(x => x.Name == "Дети").SingleOrDefault().Id
                            }
                        },
                        PromoCodes = new List<CustomerPromoCode>()
                        {
                            new CustomerPromoCode()
                            {
                                CustomerId = customerId2,
                                PromoCodeId = Guid.Parse("A2bc5299-92d7-459f-896e-078ed53ea98b")
                            }
                        }
                    }
                };

                return customers;
            }
        }
    }
}
