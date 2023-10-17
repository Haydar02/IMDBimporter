using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class PreparedInsert : IInserte
    {
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {


            SqlCommand sqlComm = new SqlCommand("" +
                "INSERT INTO [dbo].[Titles]([tconst] " +
                ",[titleType] ,[primaryType],[originalTitle]," +
                "[isAdult],[startYear]" +
                ",[endYear],[runtimeMinutes])" + "VALUES" +
                  $"(@tconst," +
                  $"@titleType," +
                  $"@primaryTitle," +
                  $"@originalTitle," +
                  $"@isAdult," +
                  $"@startYear," +
                  $"@endYear," +
                  $"@runtimeMinutes)"
                    , sqlConn);
            SqlParameter tconstParameter = new SqlParameter("@tconst",
                           System.Data.SqlDbType.VarChar, 10);
            sqlComm.Parameters.Add(tconstParameter);

            SqlParameter titleTypeParameter = new SqlParameter("@titleType",
                System.Data.SqlDbType.VarChar, 50);
            sqlComm.Parameters.Add(titleTypeParameter);

            SqlParameter primaryTitleParameter = new SqlParameter("@primaryTitle",
                System.Data.SqlDbType.VarChar, 8000);
            sqlComm.Parameters.Add(primaryTitleParameter);

            SqlParameter originalTitleParameter = new SqlParameter("@originalTitle",
                System.Data.SqlDbType.VarChar, 8000);
            sqlComm.Parameters.Add(originalTitleParameter);

            SqlParameter isAdultParameter = new SqlParameter("@isAdult",
                System.Data.SqlDbType.Bit);
            sqlComm.Parameters.Add(isAdultParameter);

            SqlParameter startYearParameter = new SqlParameter("@startYear",
                System.Data.SqlDbType.Int);
            sqlComm.Parameters.Add(startYearParameter);

            SqlParameter endYearParameter = new SqlParameter("@endYear",
                System.Data.SqlDbType.Int);
            sqlComm.Parameters.Add(endYearParameter);

            SqlParameter runtimeMinutesParameter = new SqlParameter("@runtimeMinutes",
                System.Data.SqlDbType.Int);
            sqlComm.Parameters.Add(runtimeMinutesParameter);

            sqlComm.Prepare();

            foreach (Title title in titles)
            {
                FillParameter(tconstParameter, title.tconst);
                FillParameter(titleTypeParameter, title.titleType);
                FillParameter(primaryTitleParameter, title.primaryTitle);
                FillParameter(originalTitleParameter, title.originalTitle);
                FillParameter(isAdultParameter, title.isAdult);
                FillParameter(startYearParameter, title.startYear);
                FillParameter(endYearParameter, title.endYear);
                FillParameter(runtimeMinutesParameter, title.runtimeMinutes);
                try
                {
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlComm.CommandText);
                    Console.ReadKey();
                }
            }
        }

        public void FillParameter(SqlParameter parameter, object? value)
        {
            if (value != null)
            {
                parameter.Value = value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }

        public void InsertDataCrew(SqlConnection sqlConn, List<TitleCrew> crews)
        {
            SqlCommand sqlComm = new SqlCommand("" +
          "INSERT INTO [dbo].[TitleCrew] + ([tconst],[directors]" +
                   ",[writers])" + "VALUES" +
                   $"(@tcont," +
                   $"directors ," +
                   $"writers)", sqlConn);

            SqlParameter tconstParameter = new SqlParameter("@tconst",
                System.Data.SqlDbType.VarChar, 10);
            sqlComm.Parameters.Add(tconstParameter);

            SqlParameter directorsParameter = new SqlParameter("@directors",
                System.Data.SqlDbType.VarChar,8000);
            sqlComm.Parameters.Add(directorsParameter);

            SqlParameter writersParameter = new SqlParameter("@writers",
                System.Data.SqlDbType.VarChar, 8000);

            
            foreach (TitleCrew titleCrew in crews)
            {
                FillParameter(tconstParameter, titleCrew.tconst);
                FillParameter(directorsParameter, titleCrew.directors);
                FillParameter(writersParameter, titleCrew.writers);
            }
            try
            {
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                Console.WriteLine(sqlComm.CommandText);
                Console.ReadKey();
            }
        }

        

        public void InsertDataName(SqlConnection sqlConn, List<Name> names)
        {
            SqlCommand sqlComm = new SqlCommand("" +
                "INSERT INTO[dbo].[Name] +([nconst], [primaryName] " +
                    "[birthYear], [deathYear])" +
                    "VALUES" +
                    $"(@ nconst , " +
                    $"primaryName," +
                    $"brithYear," +
                    $"deathYear)", sqlConn);
            SqlParameter nconstParameter = new SqlParameter("@nconst",
                System.Data.SqlDbType.VarChar, 10);
            sqlComm.Parameters.Add(nconstParameter);

            SqlParameter pramiryNameParameter = new SqlParameter("@primaryName",
                System.Data.SqlDbType.VarChar, 8000);
            sqlComm.Parameters.Add(pramiryNameParameter);

            SqlParameter birthYearParameter = new SqlParameter("@birthYear",
                System.Data.SqlDbType.Int);
            sqlComm.Parameters.Add(birthYearParameter);

            SqlParameter deathYearParameter = new SqlParameter("@deathYear",
                System.Data.SqlDbType.Int);
            sqlComm.Parameters.Add(deathYearParameter);


            foreach (Name name in names)
            {
                FillParameter(nconstParameter, name.nconst);
                FillParameter(pramiryNameParameter, name.primaryName);
                FillParameter(birthYearParameter, name.brithYear);
                FillParameter(deathYearParameter, name.deathYear);


            }
            try
            {
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(sqlComm.CommandText);
                Console.ReadKey();
            }
;
        }
    }
}

     