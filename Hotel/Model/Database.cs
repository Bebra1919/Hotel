using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Hotel.Model
{
    internal class Database
    {
        private SqlConnection connection = new SqlConnection(
            "Data Source = KOMPUTER\\SQLEXPRESS; Initial Catalog = Hotel; Integrated Security = True;"
            );
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetSqlConnection()
        {
            return connection;
        }
    }
}