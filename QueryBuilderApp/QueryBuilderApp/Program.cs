using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderApp
{
    class Program
    {
        static DataBase dataBase;

        static void Main(string[] args)
        {
            dataBase = new DataBase(ConfigurationManager.ConnectionStrings["RecordsDB"].ConnectionString);

            Console.WriteLine("Input your query:");
            String lineQuery = Console.ReadLine();

            Dictionary<String, String> parametrs = null;
            if (lineQuery.Contains("@"))
                parametrs = GetParametrs(lineQuery);

            Console.WriteLine(Environment.NewLine + "Result:");
            if (lineQuery.Contains("SELECT"))
            {
                List<Record> records = dataBase.ExecuteSelectQuery(lineQuery, parametrs);
                if (records != null)
                {
                    foreach (var record in records)
                        Console.WriteLine($"{record.Id} {record.Text} {record.Author} {record.RecordDate}");
                }
            }
            else
            {
                Int32 count = dataBase.ExecuteUpdateQuery(lineQuery, parametrs);

                Console.WriteLine($"Updated {count} rows");
            }

            Console.ReadKey();
        }

        // Получить параметры для запроса
        static Dictionary<String, String> GetParametrs(String query)
        {
            Console.WriteLine(Environment.NewLine + "Input parametrs:");
            Dictionary<String, String> parametrs = new Dictionary<string, string>();

            int index = 0;
            Int32 countParametrs = query.Count(c => c == '@');
            while (index < countParametrs)
            {
                String[] parametr = Console.ReadLine().Split(' ');
                parametrs.Add(parametr[0], String.Join(" ", parametr, 1, parametr.Length - 1));

                index++;
            }

            return parametrs;
        }
    }
}
