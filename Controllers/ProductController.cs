using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using _3MeePOSapi.Models;
using _3MeePOSapi.Services;

namespace _3MeePOSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProduct() => _productService.GetProductAll();

        [HttpGet("{brand}")]
        public ActionResult<List<Product>> GetProductByBrand(string brand)
        {
            var filter = _productService.GetProductByBrand(brand);
            return filter;
        }

        [HttpGet("{type}")]
        public ActionResult<List<Product>> GetProductBytype(string type)
        {
            var filter = _productService.GetProductByType(type);
            return filter;
        }

        [HttpGet("{datex}")]
        public ActionResult<List<Product>> GetProductByDate(DateTime datex)
        {
            var filter = _productService.FilterProductBydate(datex);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Product>> GetCustomerByRangeDate(DateTime datex1 ,DateTime datex2)
        {
            var filter = _productService.FilterProductByRangeDate(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }


        [HttpGet("{id}")]
        public ActionResult<Product> GetProductByid(string id)
        {
            var product = _productService.GetProductByid(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                _productService.CreateProduct(product);
                return Ok(new
                {
                    message = "Create Product Done",
                    status = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Create Product Fail",
                    exception = ex,
                    status = 400
                });
            }

            // var checkData = _productService.GetProductByid(product.ProductId);
            // if (checkData != null)
            // {
            //     return BadRequest();
            // }
            // _productService.CreateProduct(product);
            // return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditProduct([FromBody] Product product, string id)
        {
            var products = _productService.GetProductByid(id);
            if (products == null)
            {
                return NotFound();
            }
            product.ProductId = id;
            _productService.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeletedProduct(string id)
        {
            var products = _productService.GetProductByid(id);
            if (products == null)
            {
                return NotFound();
            }
            products.Status = "Close";
            _productService.DeletedProduct(id, products);
            return NoContent();
        }

    }
}