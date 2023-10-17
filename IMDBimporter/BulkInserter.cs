using IMDBimporter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBimporter
{
    public class BulkInserter : IInserte
    {
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
            DataTable titleTable = new DataTable("Titles");

            titleTable.Columns.Add("tconst", typeof(string));
            titleTable.Columns.Add("titleType", typeof(string));
            titleTable.Columns.Add("primaryTitle", typeof(string));
            titleTable.Columns.Add("originalTitle", typeof(string));
            titleTable.Columns.Add("isAdult", typeof(bool));
            titleTable.Columns.Add("startYear", typeof(int));
            titleTable.Columns.Add("endYear", typeof(int));
            titleTable.Columns.Add("runtimeMinutes", typeof(int));
            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                FillParameter(titleRow, "tconst", title.tconst);
                FillParameter(titleRow, "titleType", title.titleType);
                FillParameter(titleRow, "primaryTitle", title.primaryTitle);
                FillParameter(titleRow, "originalTitle", title.originalTitle);
                FillParameter(titleRow, "isAdult", title.isAdult);
                FillParameter(titleRow, "startYear", title.startYear);
                FillParameter(titleRow, "endYear", title.endYear);
                FillParameter(titleRow, "runtimeMinutes", title.runtimeMinutes);
                titleTable.Rows.Add(titleRow);


            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn,
                SqlBulkCopyOptions.KeepNulls,null);

            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(titleTable);
        }


        public void FillParameter(DataRow row,string columbName, object? value)
        {
            if (value != null)
            {
                row[columbName] = value;
            }
            else
            {
                row[columbName] = DBNull.Value;
            }

        }

        public void InsertDataCrew(SqlConnection sqlConn, List<TitleCrew> crews)
        {
            DataTable crewTable = new DataTable("TitleCrew");
            crewTable.Columns.Add("tconst", typeof(string));
            crewTable.Columns.Add("directors", typeof(string));
            crewTable.Columns.Add("writers", typeof(string));


            foreach (TitleCrew crew in crews)
            {
                DataRow crewRow = crewTable.NewRow();
                FillParameter(crewRow, "tconst", crew.tconst);
                FillParameter(crewRow, "directors", crew.directors);
                FillParameter(crewRow, "writers", crew.writers);
                crewTable.Rows.Add(crewRow);
            }
            SqlBulkCopy bulkCopyCrew = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepNulls,null);
            bulkCopyCrew.DestinationTableName = ("TitleCrew");
            bulkCopyCrew.BulkCopyTimeout = 0;
            bulkCopyCrew.WriteToServer(crewTable);
        }
        
       

        public void InsertDataName(SqlConnection sqlConn, List<Name> names)
        {
            DataTable nameTable = new DataTable("Name");
            nameTable.Columns.Add("nconst", typeof(string));
            nameTable.Columns.Add("primaryName", typeof(string));
            nameTable.Columns.Add("brithYear", typeof(int));
            nameTable.Columns.Add("deathYear", typeof(int));

            foreach (Name name in names)
            {
                DataRow nameRow = nameTable.NewRow();
                FillParameter(nameRow, "nconst", name.nconst);
                FillParameter(nameRow, "primaryName", name.primaryName);
                FillParameter(nameRow, "brithYear", name.brithYear);
                FillParameter(nameRow, "deathYear", name.deathYear);
                nameTable.Rows.Add(nameRow);
            }
            SqlBulkCopy bulkCopyName = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopyName.DestinationTableName = ("Name");
            bulkCopyName.BulkCopyTimeout = 0;
            bulkCopyName.WriteToServer(nameTable); ;
        }
    }
}
