using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayWatchCSVExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVParser parser = new CSVParser();
            parser.CSVFileToCache();

            foreach (var item in parser.Cache)
            {
                DatabaseWriter.writeRowToDatabase(item);
            }

        }
    }
}
