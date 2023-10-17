
using IMDBimporter.Model;
using Microsoft.Data.SqlClient;

namespace IMDBimporter
{
    public interface IInserte
    {

        void InsertData(SqlConnection sqlConn, List<Title> titles);
        void InsertDataCrew(SqlConnection sqlConn, List<TitleCrew> crews);
        void InsertDataName(SqlConnection sqlConn, List<Name> names);




    }
}