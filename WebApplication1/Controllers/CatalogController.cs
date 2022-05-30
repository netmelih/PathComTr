using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Helpers;
using DbConnection.Models;
using Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private static ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            try
            {
                var productList = _catalogService.GetProducts();
                return Ok(productList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Product(Guid id)
        {
            try
            {
                var product = _catalogService.GetProduct(id);

                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AddProduct(Products product)
        {
            try
            {
                bool status = _catalogService.AddProduct(product);

                if (status)
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
            }
            return BadRequest();

        }
    }
}
