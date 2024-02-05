using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using WebAppwithData.Models;
using Microsoft.FeatureManagement;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        public async Task <List<Product>> GetProducts()
        {
            string functionURL = "https://appfunction1291.azurewebsites.net/api/GetProducts?code=dTWRk2Y-5Uu0IJV9xmdeZh9otV7FrcJ-HaLFutrJk36pAzFujs0FjA==";

            using( HttpClient client = new HttpClient() )
            {
                HttpResponseMessage resp = await client.GetAsync(functionURL);

                string content = await resp.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content);
            }
        }

        //public List<Product> GetProducts()
        //{

        //    var products = new List<Product>();
        //    SqlConnection conn = GetConnection();
        //    string sqlQuery = "SELECT ProductID, ProductName, Quantity from Products";
        //    try
        //    {

        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand(sqlQuery, conn);

        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                var prod = new Product()
        //                {
        //                    ProductID = sdr.GetInt32(0),
        //                    ProductName = sdr.GetString(1),
        //                    Quantity = sdr.GetInt32(2)
        //                };

        //                products.Add(prod);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.CloseAsync();
        //        }
        //    }


        //    return products;

        //}
    }
}
