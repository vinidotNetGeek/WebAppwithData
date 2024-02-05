using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppwithData.Models;
using WebAppwithData.Services;

namespace WebAppwithData.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        private readonly ILogger<IndexModel> _logger;
        public List<Product> products;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            products = _productService.GetProducts();
        }
    }
}