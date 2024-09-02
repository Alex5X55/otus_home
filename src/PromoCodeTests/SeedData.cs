using Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PromoCodeTests
{
    public static class SeedData
    {
        public static void SeedFakeData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DatabaseContext>();

                context.AddRange(FakeDataFactory.Partners);
               
                //AddRange(FakeDataFactory.Employees);
                //AddRange(FakeDataFactory.Customers);
                //AddRange(FakeDataFactory.PromoCodes);

                //SaveChanges();

                // var addResults = new List<bool>();
                // foreach (var procedureType in ProcedureTypeConstants.AllProcedureTypes)
                //  {
                //     addResults.Add(AddProcedureType(context, procedureType.Key, procedureType.Value));
                //  }

                //  if (addResults.Any(s => s == true))
                //  {
                context.SaveChanges();
              //  }
            }

        }
    }
}
