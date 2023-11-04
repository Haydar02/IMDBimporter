using IMDBimporter;
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
    public class KnownForTitleInserter
    {
        public static void InsertKnownForTitle(SqlConnection sqlConn, List<Name> nameList)
        {
            HashSet<string> knownForTitles = new HashSet<string>();
            Dictionary<string, int> knownForTitleDic = new Dictionary<string, int>();

            foreach (var name in nameList) {
                foreach (var kownForTilte in name.KnownForTitles)
                {
                    knownForTitles.Add(kownForTilte);
                }
            }

            foreach (string knownForTitle in knownForTitles)
            {
                SqlCommand insertCommand = new SqlCommand(
                    "INSERT INTO KnownForTitle(knownForT) " +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@knownForTitle)", sqlConn);
                insertCommand.Parameters.AddWithValue("@knownForTitle", knownForTitle);

                try
                {
                    SqlDataReader reader = insertCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        int newId = (int)reader["ID"];
                        knownForTitleDic.Add(knownForTitle, newId);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(insertCommand.CommandText, ex);
                }
            }

            foreach(Name myName in nameList)
            {
                foreach (string knownForTitle in myName.KnownForTitles)
                {
                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO NconstKnownForTitle (Nconst,KnownForTitleID)" +
                        "VALUES" + "('"+ myName.nconst + "', '"
                        + knownForTitleDic[knownForTitle]+"')" ,sqlConn);
                 
                    try
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception (insertCommand.CommandText, ex);
                    }


                }
            }
            
        
        }
    }
}