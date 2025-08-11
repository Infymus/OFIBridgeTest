using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataBase.Query
{
    public class OracleQuery
    {

        private string? connectionString;

        public OracleQuery(string? _connectionString)
        {
            connectionString = _connectionString;
        }
               
        /// <summary>
        /// Oracle Query to return Data Table
        /// </summary>
        /// <param name="inJob"></param>
        /// <returns></returns>
        public DataTable dbGetDataTable(string inID)
        {
            DataTable data = new DataTable();
            string sqlStr;
            sqlStr = @"SELECT * FROM SOMETABLE WHERE SOMETHING = :INID";

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.BindByName = true;
                command.Parameters.Add("INID", inID);
                command.CommandText = (sqlStr);

                using (var reader = new OracleDataAdapter(command))
                {
                    reader.Fill(data);
                }
                connection.Close();

            }
            return data;
        }

    }
}



