using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BayWatchCSVExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = ConfigurationManager.AppSettings.Get("Path");    

            CSVParser parser = new CSVParser();

            parser.CSVFileToCache(Path);

            foreach (var item in parser.Cache)
            {
                DatabaseWriter.writeRowToDatabase(item);
            }

        }
    }
}
