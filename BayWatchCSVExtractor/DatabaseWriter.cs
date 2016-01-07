using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayWatchCSVExtractor
{

    public class DatabaseReader
    {
        public List<ADDITIONAL_LANGUAGE> getListOfLanguages()
        {
            List<ADDITIONAL_LANGUAGE> ExistingLanguages;
            using (var context = new LPPEntities())
            {
                ExistingLanguages = context.ADDITIONAL_LANGUAGE.ToList<ADDITIONAL_LANGUAGE>();
            }
            return ExistingLanguages;
        }
    }

    public class DatabaseWriter
    {

        public static bool writeRowToDatabase(CSVRow Row) {


            using (var context = new LPPEntities())
            {
                context.Database.Log = Console.Write;

                try
                {
                    writeToAdditionalLanguageTable(Row.AdditionalLanuageClass, context);
                    writeToBeachedTable(Row.BeachedClass, context);
                    writeToBeachedLanguageTable(context, Row.BeachedLanguageClass);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
                
                return true;
            }
        }

        private static void writeToBeachedLanguageTable(LPPEntities context, HashSet<BEACHED_LANGUAGE> hashSetBeachedLanguage)
        {
            //foreach (var item in hashSetBeachedLanguage)
            //{
                context.BEACHED_LANGUAGE.AddRange(hashSetBeachedLanguage);
           // }
        }


        private static void writeToAdditionalLanguageTable(HashSet<ADDITIONAL_LANGUAGE> Languages, LPPEntities context)
        {
            

            //using (var context = new LPPEntities())
            //{
               // context.Database.Log = Console.Write;

                foreach (var language in Languages)
                {
                    if (context.ADDITIONAL_LANGUAGE.FirstOrDefault(s => s.LANG_NAME.Equals(language.LANG_NAME.Trim())) == null)
                    {
                        context.ADDITIONAL_LANGUAGE.Add(language);
                    }
                }
               // context.SaveChanges();
           // }

        }

        private static void writeToBeachedTable(BEACHED consultant, LPPEntities context)
        {

            //using (var context = new LPPEntities())
            //{
               // context.Database.Log = Console.Write;

                var Existingbeached = context.BEACHEDs.FirstOrDefault(c => c.BEACHED_ID.Equals(consultant.BEACHED_ID));
                if (Existingbeached != null)
                {
                    context.BEACHEDs.Attach(Existingbeached);

                    Existingbeached = consultant;

                  //  context.SaveChanges();
                }
                else
                {
                    context.BEACHEDs.Add(consultant);
                    //try
                    //{
                    //    context.SaveChanges();
                    //}
                    //catch (DbEntityValidationException e)
                    //{
                    //    foreach (var item in e.EntityValidationErrors)
                    //    {
                    //        Console.WriteLine(item.ValidationErrors.ToString());
                    //    }

                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine((e.InnerException.InnerException));
                    //}
                }
            }
        }
    }

