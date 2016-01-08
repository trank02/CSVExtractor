using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayWatchCSVExtractor
{
    public class CSVRow
    {

        public BEACHED BeachedClass { get; set; }
        public HashSet<ADDITIONAL_LANGUAGE> AdditionalLanuageClass { get; set; }
        public HashSet<ADDITIONAL_TECHNOLOGY> AdditionalTechnologyClass { get; set; }
        public HashSet<IN_HOUSE_TECHNOLOGY> InHouseTechnology { get; set; }
        public BEACHED_ADDITIONAL_TECHNOLOGY BeachedAdditionalTechnologyClass { get; set; }
        public BEACHED_IN_HOUSE_TECHNOLOGY BeachedInHouseTechnologyClass { get; set; }
        public HashSet<BEACHED_LANGUAGE> BeachedLanguageClass { get; set; }
    }

    public class CSVParser
    {
        public CSVParser()
        {
            Cache = new List<CSVRow>();
        }

        public List<CSVRow> Cache { get; private set; }

        public void CSVFileToCache(string Dir)
        {
            //using (TextFieldParser parser = new TextFieldParser(@"C:\Users\kevin.tran\Desktop\CSV\Sheet_1.csv"))
            using (TextFieldParser parser = new TextFieldParser(@Dir))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {

                    string[] fields = parser.ReadFields();
                    if (parser.LineNumber >= 4)
                    {
                        CSVRow currentCSVRow = RowToCSVRowObject(fields);
                        Cache.Add(currentCSVRow);
                    }

                }
            }
        }


        private CSVRow RowToCSVRowObject(string[] fields)
        {
            CSVRow currentCSVRow = new CSVRow();
            currentCSVRow.AdditionalLanuageClass = RowToAdditionalLanguageObject(fields);
            currentCSVRow.BeachedClass = RowToBeachedObject(fields);
            currentCSVRow.BeachedLanguageClass = RowToBeachedLanguageObject(currentCSVRow.AdditionalLanuageClass, currentCSVRow.BeachedClass);

            return currentCSVRow;
        }

        private HashSet<BEACHED_LANGUAGE> RowToBeachedLanguageObject(HashSet<ADDITIONAL_LANGUAGE> additionLanguages, BEACHED beached)
        {
            var listOfBeachedLangauge = new HashSet<BEACHED_LANGUAGE>();

            foreach (var additionLanguage in additionLanguages)
            {
                var beachedLanguage = new BEACHED_LANGUAGE();
               // beachedLanguage.ADDITIONAL_LANGUAGE = additionLanguage;
                beachedLanguage.LANG_ID = additionLanguage.LANG_ID;
                beachedLanguage.BEACHED_ID = beached.BEACHED_ID;
               
                listOfBeachedLangauge.Add(beachedLanguage);
            }

            return listOfBeachedLangauge;

        }

        private HashSet<ADDITIONAL_LANGUAGE> RowToAdditionalLanguageObject(string[] fields)
        {
            //62- 66
            var languages = new HashSet<ADDITIONAL_LANGUAGE>();

            for (int i = 62; i <= 66; i++)
            {
                if (fields[i].ToString().Trim().Length != 0)
                {
                    languages.Add(new ADDITIONAL_LANGUAGE { LANG_NAME = fields[i].Trim() });
                }
                else
                {
                    break;
                }
            }
            return languages;

        }

        private BEACHED RowToBeachedObject(string[] fields)
        {
            BEACHED currentBeached = new BEACHED();

            long BeachedId;
            Int64.TryParse(fields[0], out BeachedId);
            currentBeached.BEACHED_ID = BeachedId;
            currentBeached.FULL_NAME = fields[9] + " " + fields[10];
            currentBeached.STREAM = fields[60];
            currentBeached.ACADEMY = fields[19];
            currentBeached.PREV_PLACEMENT = fields[11];
            currentBeached.PREV_JOB_TITLE = fields[12];
            currentBeached.AVAILABLE = 1;

            return currentBeached;
        }

    }
}
