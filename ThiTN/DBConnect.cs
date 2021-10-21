using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTN
{
    class DBConnect
    {

        public static string username;
        //static SqlConnection sqlConnection = new SqlConnection(@"Data Source= OPREKIN-PC;Initial Catalog=BTLThiLaiXe; User ID=sa; Password=123;");
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ThiTN");
        static SqlDataAdapter sqlData;
            
        public static DataTable executeQuery(string sql)
        {
            sqlData = new SqlDataAdapter(sql, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            return dataTable;
        }
    }
}
