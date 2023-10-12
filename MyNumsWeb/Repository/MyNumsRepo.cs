using MyNumsWeb.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MyNumsWeb.Repository
{
    public class MyNumsRepo : IMyNumsRepo
    {

        string DbPath = System.Web.HttpRuntime.AppDomainAppPath + "mynums.db";

        public int AddNewNumbers(int[] numbers)
        {
            using (var conn = new SQLiteConnection($"Data Source={DbPath}"))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = $"SELECT number FROM nums WHERE number IN ({string.Join(",", numbers)});";
                var sqlReader = cmd.ExecuteReader();
                var newNums = new List<int>(numbers.Distinct());

                while (sqlReader.Read())
                {
                    int numInDb = sqlReader.GetInt32(0);

                    if (newNums.Contains(numInDb))
                        newNums.Remove(numInDb);
                }
                sqlReader.Close();

                using (var trans = conn.BeginTransaction())
                {
                    foreach(var newNum in newNums)
                    {
                        cmd.CommandText = $"INSERT INTO nums(number) VALUES('{newNum}');";
                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }

                conn.Close();

                return newNums.Count();
            }
        }

        public IEnumerable<MyNum> GetNumbers()
        {
            var nums = new List<MyNum>();

            using (var conn = new SQLiteConnection($"Data Source={DbPath}"))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = @"SELECT 
                                        number, 
                                        CASE WHEN note IS null THEN '' ELSE note END AS note, 
                                        created
                                    FROM nums 
                                    WHERE note IS null OR note <> 'BAD'
                                    ORDER BY note, created";
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    nums.Add(new MyNum()
                    {
                        Number = sqlReader.GetInt32(0),
                        Note = sqlReader.GetString(1),
                        Created = sqlReader.GetDateTime(2)
                    });
                }

                conn.Close();
            }

            return nums;
        }

        public void UpdateNum(int id, string note)
        {
            using (var conn = new SQLiteConnection($"Data Source={DbPath}"))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = $"UPDATE nums SET note = @note WHERE number = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue ("note", note);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}