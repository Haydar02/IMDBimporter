
using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class MyMenu
    {
        Helper helper = new Helper();


        public void Menu()
        {
            string ConString = "server= HAYDAR-AL-GHAZA\\MSSQLSERVER19;database=IMDB;"
           + "user id=MySQLH;password= JaaYal1!1;TrustServerCertificate=True";



            var titles = new List<Title>();
            var crews = new List<TitleCrew>();
            var names = new List<Name>();


            Console.WriteLine("hvad vil du ");
            Console.WriteLine("1 for at delete alt");
            Console.WriteLine("2 for at normal insert");
            Console.WriteLine("3 for prepared insert");
            Console.WriteLine("4 for bulk insert");
            Console.WriteLine("5 Read File");
            Console.WriteLine("6 for at normal Crew insert");
            Console.WriteLine("7 for prepared Title Crew insert");
            Console.WriteLine("8 for bulk Title Crew insert");
            Console.WriteLine("9 for at delete Title Crew");
            Console.WriteLine("10 for at normal Person name insert");
            Console.WriteLine("11 for at prepared person name insert ");
            Console.WriteLine("12 for at bulk person name insert");
            Console.WriteLine("13 for at delete alt i persons tabel");
            
            
            string input = Console.ReadLine();


            SqlConnection sqlConn = new SqlConnection(ConString);
            sqlConn.Open();
            int batchSize = 100000;
            IInserte? myInserter = null;
            DateTime before = DateTime.Now;

            switch (input)
            { 
                
                case "1":
                    string deleteQurey = "DELETE TOP (" + batchSize + ") " +
                                                      "FROM Titles;" +
                                                      "DELETE FROM Genres;" +
                                                      "DELETE FROM TitleGenres ;";

                    SqlCommand cmd = new SqlCommand(deleteQurey, sqlConn);
                    int rowsAffected;
                    do
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    while(rowsAffected > 0);

                    sqlConn.Close();
                    break;



                    //    "DELETE FROM Titles; " +
                    //    "DELETE FROM Genres; " +
                    //    "DELETE FROM TitleGenres;",
                    //    sqlConn);
                    //cmd.ExecuteNonQuery();
                    //break;

                case "2":
                    before = DateTime.Now;
                    titles = helper.TitleReadFile();
                    myInserter = new NormalInsert();
                    break;

                case "3":
                    before = DateTime.Now;
                    titles = helper.TitleReadFile();
                    myInserter = new PreparedInsert();
                    break;

                case "4":
                    before = DateTime.Now;
                    titles = helper.TitleReadFile();
                    myInserter = new BulkInserter();
                    break;
                case "5":
                    before = DateTime.Now;
                    titles = helper.TitleReadFile();

                    break;
                case "6":
                    before = DateTime.Now;
                    crews = helper.CrewReadFile();
                    myInserter = new NormalInsert();
                    break;

                case "7":
                    before = DateTime.Now;
                    crews = helper.CrewReadFile();
                    myInserter = new PreparedInsert();
                    break;


                case "8":
                    before = DateTime.Now;
                    crews = helper.CrewReadFile();
                    myInserter = new BulkInserter();

                    break;

                case "9":                   

                    SqlCommand sqlComm = new SqlCommand(
                        " DELETE FROM TitleCrew;",
                        sqlConn);
                    sqlComm.ExecuteNonQuery();
                    break;

                case "10":
                    before = DateTime.Now;
                    names=helper.NameReadFile();
                    myInserter = new NormalInsert();
                    break;

                case "11":
                    before = DateTime.Now;
                    names = helper.NameReadFile();
                    myInserter = new PreparedInsert();
                    break;

                case "12":
                    before = DateTime.Now;
                    names = helper.NameReadFile();
                    myInserter = new BulkInserter();
                    break;

                case "13":
                    string deleteQuery = "DELETE TOP (" + batchSize + ") FROM Name; " +
                                        "DELETE FROM PrimaryProfession; " +
                                        "DELETE FROM primaryProfessionName;";

                    SqlCommand sqlCom = new SqlCommand(deleteQuery, sqlConn);
                    int rowAffected;
                    do
                    {
                        rowAffected = sqlCom.ExecuteNonQuery();
                    }
                    while (rowAffected > 0);
                    
                    break;



            }


            if (myInserter != null)
            {
                myInserter.InsertData(sqlConn, titles);
                myInserter.InsertDataCrew(sqlConn, crews);
                myInserter.InsertDataName(sqlConn, names);
                GenreInserter.InsertGenres(sqlConn, titles);
                primaryProfessionInserter.InsertPrimaryProfession(sqlConn, names);
            }

            sqlConn.Close();
            DateTime after = DateTime.Now;



            Console.WriteLine("Tid:" + (after - before));
            Menu();

        }


    }
}
