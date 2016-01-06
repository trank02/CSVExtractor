using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayWatchCSVExtractor
{
    public class DatabaseWriter
    {

        public static void writeToBeachedTable(BEACHED consultant)
        {

            using (var context = new LPPEntities())
            {
                context.BEACHEDs.Add(consultant);
                var consultantEntry = context.Entry(consultant);

                Console.WriteLine("consultant EntityState: {0}", consultantEntry.State);

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var item in e.EntityValidationErrors)
                    {
                        Console.WriteLine(item.ValidationErrors.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine((e.InnerException.InnerException));
                }
            }
        }
    }
}
