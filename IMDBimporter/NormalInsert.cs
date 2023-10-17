using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class NormalInsert : IInserte
    {
        //public string ConString = "server= (localdb)\\MSSQLLocalDB;database=IMDB;"
        //     + "user id=MySQLH;password= JaaYal1!1;TrustServerCertificate=True";



        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {

            foreach (Title title in titles)
            {
                SqlCommand sqlCom = new SqlCommand("" +
                    "INSERT INTO [dbo].[Titles]+([tconst] " +
                    ",[titleType] ,[primaryType],[originalTitle]," +
                    "[isAdult],[startYear]" +
                    ",[endYear],[runtimeMinutes])" + "VALUES" +
                  $"('{title.tconst}','{title.titleType}'," +
                        $"'{title.primaryTitle.Replace("'", "''")}'," +
                        $"'{title.originalTitle.Replace("'", "''")}'," +
                        $"'{title.isAdult}',{CheckIntForNull(title.startYear)}," +
                        $"{CheckIntForNull(title.endYear)},{CheckIntForNull(title.runtimeMinutes)})"
                        , sqlConn);


                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine(sqlCom.CommandText);
                }

            }


        }
        public string? CheckIntForNull(int? input)
        {
            if (input == null)
            {
                return "NULL";
            }
            else
            {
                return "" + input;
            }
        }


        public void InsertDataCrew(SqlConnection sqlConn, List<TitleCrew> crews)
        {
            foreach (TitleCrew crew in crews)
            {
                SqlCommand cmd = new SqlCommand("" +
                   "INSERT INTO [dbo].[TitleCrew] + ([tconst],[directors]" +
                   ",[writers])" + "VALUES" + $"('{crew.tconst}', " +
                   $"'{crew.directors}','{crew.writers}')",
                   sqlConn);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine(cmd.CommandText);
                }


            }

        }


        public void InsertDataName(SqlConnection sqlConn, List<Name> names)
        {
            foreach (Name name in names)
            {
                SqlCommand sqlComm = new SqlCommand("" +
                    "INSERT INTO[dbo].[Name] +([nconst], [primaryName] " +
                    "[birthYear], [deathYear])" +
                    "VALUES" + $"('{name.nconst}', " + 
                    $" '{name.primaryName}' " +
                    $" '{name.brithYear}' " +
                    $" '{name.deathYear}')",
                     sqlConn);
                try
                {
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(sqlComm.CommandText);
                }
            }
        }
    }
}
