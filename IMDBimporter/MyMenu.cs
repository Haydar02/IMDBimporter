
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------------------------");
            Console.WriteLine("|    HVAD VIL DU SØGE     |");
            Console.WriteLine("___________________________");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"1-  FOR AT DELETE ALT");
            Console.WriteLine("2-   FOR AT NORMAL INSERT");
            Console.WriteLine("3-   FOR PREPARED INSERT");
            Console.WriteLine("4-   FOR BULK INSERT");
            Console.WriteLine("___________________________");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("5-   RED FILE ()");
            Console.WriteLine("___________________________");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("6-   FOR AT NORMAL CREW INSERT");
            Console.WriteLine("7-   FOR PREPAED TITLE CREW INSERT");
            Console.WriteLine("8-   FOR BULK TITLE CREW INSEET");
            Console.WriteLine("9-   FOR AT DELETE TITLE CREW");
            Console.WriteLine("___________________________");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("10-  FOR AT NORMAL PERSON-NAME INSERT");
            Console.WriteLine("11-  FOR AT PREPARED PERSON-NAME INSERT");
            Console.WriteLine("12_  FOR AT BULK PERSON-NAME INSERT");
            Console.WriteLine("13_  FOR AT DELETE ALT I PERSON-NAME TABELLEN");
            Console.WriteLine("___________________________");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("14_  FOR AT DELETE FRA Nconst-KNOWNFORTITLE");

            Console.WriteLine("__________________________________");

          
            Console.ForegroundColor = ConsoleColor.Magenta;
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
                    while (rowsAffected > 0);

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
                    names = helper.NameReadFile();
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

                case "14":
                    SqlCommand DeleteCommand = new SqlCommand(
                       " DELETE FROM KnownForTitle;" +
                       " DELETE FROM NconstKnownForTitle",
                       sqlConn);
                    DeleteCommand.ExecuteNonQuery();
                    break;             

            }

            if (myInserter != null)
            {
                myInserter.InsertData(sqlConn, titles);
                myInserter.InsertDataCrew(sqlConn, crews);
                myInserter.InsertDataName(sqlConn, names);
                GenreInserter.InsertGenres(sqlConn, titles);
                primaryProfessionInserter.InsertPrimaryProfession(sqlConn, names);
                KnownForTitleInserter.InsertKnownForTitle(sqlConn, names);


            }

            sqlConn.Close();
            DateTime after = DateTime.Now;



            Console.WriteLine("Tid:" + (after - before));
            Menu();

        }


    }
}
