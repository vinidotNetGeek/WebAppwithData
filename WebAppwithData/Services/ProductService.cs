using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using WebAppwithData.Models;
using Microsoft.FeatureManagement;

namespace WebAppwithData.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;
        private readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration config, IFeatureManager featureManager)
        {
            _config = config;
            _featureManager = featureManager;
        }

        public async Task<bool> IsBeta()
        {
            return await _featureManager.IsEnabledAsync("beta") == true;
        }

        private SqlConnection GetConnection()
        {

            return new SqlConnection(_config["SQLConnection"]);
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
