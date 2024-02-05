using WebAppwithData.Models;

namespace WebAppwithData.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}