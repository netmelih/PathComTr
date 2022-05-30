using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace PathComTr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        public ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetBasket(Guid productId)
        {
            try
            {
                return Ok(_cartService.GetCart());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("{productId}")]
        public IActionResult AddToBasket(Guid productId)
        {
            try
            {
                _cartService.AddToCart(productId);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult RemoveFromBasket(Guid productId)
        {
            try
            {
                _cartService.DeleteFromCart(productId);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CompleteCart()
        {
            try
            {
                _cartService.CompleteCart();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
