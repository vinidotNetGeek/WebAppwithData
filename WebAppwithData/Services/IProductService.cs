using WebAppwithData.Models;

namespace WebAppwithData.Services
{
    public interface IProductService
    {
        Task <List<Product>> GetProducts();

        Task<bool> IsBeta();
    }
}