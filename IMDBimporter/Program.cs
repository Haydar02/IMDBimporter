
using IMDBimporter;
using Microsoft.Data.SqlClient;
using System.Data;

MyMenu M = new MyMenu();
M.Menu();

//var titles = helper.ReadFile();

//Console.WriteLine(titles.Count);
//Console.WriteLine("hvad vil du ");
//Console.WriteLine("1 for at delete alt");
//Console.WriteLine("2 for at normal insert");
//Console.WriteLine("3 for prepared insert");
//Console.WriteLine("4 for bulk insert");
//string input = Console.ReadLine();


//SqlConnection sqlConn = new SqlConnection(ConString);
//sqlConn.Open();

//IInserte? myInserter = null;


//switch (input)
//{
//    case "1":
//        SqlCommand cmd = new SqlCommand("DELETE FROM Titles", sqlConn);
//        cmd.ExecuteNonQuery();
//        break;

//        case "2":
//        myInserter = new NormalInsert();
//        break;

//        case "3":
//        myInserter = new PreparedInsert();
//        break;

//        case "4":
//        myInserter = new BulkInserter();
//        break;

//}
//DateTime before = DateTime.Now;

//if (myInserter != null)
//{
//    myInserter.InsertData(sqlConn,titles);
//}

//sqlConn.Close();
//DateTime after  = DateTime.Now;



//Console.WriteLine("Tid:"+(after - before));

