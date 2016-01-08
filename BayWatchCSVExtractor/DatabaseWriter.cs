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

        public static bool writeRowToDatabase(CSVRow Row)
        {

            try
            {
                writeToAdditionalLanguageTable(Row.AdditionalLanuageClass);
                writeToBeachedTable(Row.BeachedClass);
                writeToBeachedLanguageTable(Row, Row.AdditionalLanuageClass);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;

        }

        private static void writeToBeachedLanguageTable(CSVRow row, HashSet<ADDITIONAL_LANGUAGE> hashSetAttidionalLanguage)
        {
            foreach (var item in hashSetAttidionalLanguage)
            {
                using (var context = new LPPEntities())
                {
                    context.Database.Log = Console.Write;

                    var beached = context.BEACHEDs.FirstOrDefault(s => s.BEACHED_ID.Equals(row.BeachedClass.BEACHED_ID));
                    var language = context.ADDITIONAL_LANGUAGE.FirstOrDefault(s => s.LANG_NAME.Equals(item.LANG_NAME));

                    beached.BEACHED_LANGUAGE.Add(new BEACHED_LANGUAGE
                    {

                        FLUENCY = 2,
                        BEACHED = beached,
                        ADDITIONAL_LANGUAGE = language
                    });



                    context.SaveChanges();
                }
            }
        }


        private static void writeToAdditionalLanguageTable(HashSet<ADDITIONAL_LANGUAGE> Languages)
        {


            using (var context = new LPPEntities())
            {
                context.Database.Log = Console.Write;

                foreach (var language in Languages)
                {
                    if (context.ADDITIONAL_LANGUAGE.FirstOrDefault(s => s.LANG_NAME.Equals(language.LANG_NAME.Trim())) == null)
                    {
                        context.ADDITIONAL_LANGUAGE.Add(language);
                    }
                }
                context.SaveChanges();
            }

        }

        private static void writeToBeachedTable(BEACHED consultant)
        {

            using (var context = new LPPEntities())
            {
                context.Database.Log = Console.Write;

                var Existingbeached = context.BEACHEDs.FirstOrDefault(c => c.BEACHED_ID.Equals(consultant.BEACHED_ID));
                if (Existingbeached != null)
                {
                    context.BEACHEDs.Attach(Existingbeached);

                    Existingbeached.ACADEMY = consultant.ACADEMY;
                    Existingbeached.AVAILABLE = consultant.AVAILABLE;
                    Existingbeached.PREV_PLACEMENT = consultant.PREV_PLACEMENT;
                    Existingbeached.PREV_JOB_TITLE = consultant.PREV_JOB_TITLE;
                    Existingbeached.FULL_NAME = consultant.FULL_NAME;
                    Existingbeached.STREAM = consultant.STREAM;

                    context.SaveChanges();
                }
                else
                {
                    context.BEACHEDs.Add(consultant);
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
}

