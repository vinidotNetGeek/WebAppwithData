using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using WebAppwithData.Models;

namespace WebAppwithData.Services
{
    public class ProductService
    {

        private static string db_source = "appdbserver1291.database.windows.net";
        private static string db_username = "sqladmin";
        private static string db_password = "Password@123";
        private static string db_database = "webappdb";

        private SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder sqlConnStringBuilder = new SqlConnectionStringBuilder();
            sqlConnStringBuilder.DataSource = db_source;
            sqlConnStringBuilder.UserID = db_username;
            sqlConnStringBuilder.Password = db_password;
            sqlConnStringBuilder.InitialCatalog = db_database;
            return new SqlConnection(sqlConnStringBuilder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            
            var products = new List<Product>();
            SqlConnection conn = GetConnection();
            string sqlQuery = "SELECT ProductID, ProductName, Quantity from Products";
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var prod = new Product()
                        {
                            ProductID = sdr.GetInt32(0),
                            ProductName = sdr.GetString(1),
                            Quantity = sdr.GetInt32(2)
                        };

                        products.Add(prod);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.CloseAsync();
                }
            }


            return products;

        }
	}
}
