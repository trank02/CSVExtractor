using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayWatchCSVExtractor
{
    public class CSVRow {

        public BEACHED BeachedClass { get; set; }
        public ADDITIONAL_LANGUAGE AdditionalLanuageClass { get; set; }
        public ADDITIONAL_TECHNOLOGY AdditionalTechnologyClass { get; set; }
        public IN_HOUSE_TECHNOLOGY InHouseTechnology { get; set; }
        public BEACHED_ADDITIONAL_TECHNOLOGY BeachedAdditionalTechnologyClass { get; set; }
        public BEACHED_IN_HOUSE_TECHNOLOGY BeachedInHouseTechnologyClass { get; set; }
        public BEACHED_LANGUAGE BeachedLanguageClass { get; set; }
    }

    public class CSVParser
    {
        public CSVParser()
        {
            Cache = new List<CSVRow>();
        }

        public List<CSVRow> Cache { get; private set; }

        public void CSVFileToCache()
        {
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\kevin.tran\Desktop\CSV\Sheet_1.csv"))
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
            currentCSVRow.BeachedClass = RowToBeachedObject(fields);
            return currentCSVRow;
        }

        private void RowToAdditionalLanuageObject(string[] fields)
        {
            throw new NotImplementedException();
        }

        private BEACHED RowToBeachedObject(string[] fields)
        {
            BEACHED currentBeached = new BEACHED();
            long BeachedId;
            long.TryParse(fields[0], out BeachedId);
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
