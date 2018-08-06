using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderApp
{
    class DataBase
    {
        private String connectionString;
        private SqlConnection sqlConn;

        internal DataBase(String connStr)
        {
            connectionString = connStr;
            sqlConn = new SqlConnection(connectionString);
        }

        internal List<Record> ExecuteSelectQuery(String query, Dictionary<String, String> parametrs)
        {
            List<Record> records = null;

            try
            {
                sqlConn.Open();

                SqlCommand command = sqlConn.CreateCommand();
                command.CommandText = query;

                if (parametrs != null)
                    foreach (var param in parametrs)
                        command.Parameters.AddWithValue(param.Key, param.Value);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    records = new List<Record>();
                    while (reader.Read())
                    {
                        records.Add(new Record
                        {
                            Id = (Int32)reader["Id"],
                            Text = (String)reader["Text"],
                            Author = (String)reader["Author"],
                            RecordDate = (DateTime)reader["RecordDate"],
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }

            return records;
        }

        internal Int32 ExecuteUpdateQuery(String query, Dictionary<String, String> parametrs)
        {
            Int32 countUpdatedRows = 0;

            try
            {
                sqlConn.Open();

                SqlCommand command = sqlConn.CreateCommand();
                command.CommandText = query;

                if (parametrs != null)
                    foreach (var param in parametrs)
                        command.Parameters.AddWithValue(param.Key, param.Value);

                countUpdatedRows = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }

            return countUpdatedRows;
        }
    }
}
