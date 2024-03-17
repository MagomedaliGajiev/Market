using Example.Abstractions;
using Example.Models;
using Example.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [HttpGet("getProducts")]
        public ActionResult GetProducts()
        {
            return Ok(_productRepository.GetProducts());
        }

        [HttpPost("addProduct")]
        public ActionResult AddProduct([FromBody] ProductModel productModel)
        {
            return Ok(_productRepository.AddProduct(productModel));
        }

        [HttpGet("getProductCategories")]
        public ActionResult GetProductCategorries()
        {
            return Ok(_productRepository.GetProductCategories());
        }

        [HttpPost("addProductCategories")]
        public ActionResult AddProductCategorie([FromBody] ProductCategoryModel productCategoryModel)
        {
            return Ok(_productRepository.AddProductCategory(productCategoryModel));
        }

        private string GetCsv(IEnumerable<ProductModel> products)
        {
            var sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"{product.Id};{product.Name};{product.Description}\n");
            }
            return sb.ToString();
        }
    }
}