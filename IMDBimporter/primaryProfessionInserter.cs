using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class primaryProfessionInserter
    {

        public static void InsertPrimaryProfession(SqlConnection sqlConn,
            List<Name> nameList)
        {
            HashSet<string> primaryprofessions = new HashSet<string>();
            Dictionary<string, int> primaryDict = new Dictionary<string, int>();
            foreach (var name in nameList)
            {
                foreach (var primaryProfission in name.primaryProfessions)
                {
                    primaryprofessions.Add(primaryProfission);
                }
            }
            foreach (string primaryP in primaryprofessions)
            {
                SqlCommand sqlComm = new SqlCommand(
                  "INSERT INTO PrimaryProfession(primaryProfession)" +
                  "OUTPUT INSERTED.ID " +
                  "VALUES ('" + primaryP + "')", sqlConn);



                try
                {

                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                        int newId = (int)reader["ID"];
                        primaryDict.Add(primaryP, newId);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(sqlComm.CommandText, ex);
                }
            }
           
            foreach (Name myList in nameList)
            {
                foreach (string primaryProfession in myList.primaryProfessions)
                {
                   
                    string checkQuery = "SELECT COUNT(*) FROM primaryProfessionName WHERE Nconst = '" +
                        myList.nconst + "' AND PrimaryProfessionID = '" + primaryDict[primaryProfession] + "'";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, sqlConn);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                      
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO primaryProfessionName(Nconst , PrimaryProfessionID) "
                            + "VALUES ('" + myList.nconst + "' , '"
                            + primaryDict[primaryProfession] + "' )", sqlConn);

                        try
                        {
                            cmd.ExecuteReader().Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(cmd.CommandText, ex);
                        }
                    }
                    else
                    {
                        
                    }
                }
            }


        }
    }



}
