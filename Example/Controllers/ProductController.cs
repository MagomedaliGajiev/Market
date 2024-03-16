using Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet("getProducts")]
        public ActionResult GetProducts()
        {
            var products = _context.Products.Select(x => new Product()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
            return Ok(products);
        }

        [HttpPost("postProducts")]
        public ActionResult PostProducts([FromQuery] string name, int categoryId, decimal cost, string description)
        {
            if (!_context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
            {
                _context.Products.Add(new Product()
                {
                    Id = categoryId,
                    Name = name,
                    Cost = cost,
                    Description = description
                });
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [HttpDelete("deleteProduct")]
        public ActionResult DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("deleteCategory")]
        public ActionResult DeleteCategory(int categoryId)
        {
            var category = _context.ProductCategories.Find(categoryId);
            if (category != null)
            {
                _context.ProductCategories.Remove(category);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("updateProductPrice")]
        public ActionResult UpdateProductPrice(int productId, decimal newPrice)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Cost = newPrice;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}