using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    internal class CSVReader
    {
        // CSVの中身を取得する
        public static object[] ReadCSV(string filePath)
        {
            var list = new List<object>();
            if (!File.Exists(filePath))
            {
                return null;
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    list.Add(values);
                   
                }
            }
            return list.ToArray();
        }
    }
}
