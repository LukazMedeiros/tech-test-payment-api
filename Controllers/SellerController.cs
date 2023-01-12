
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly SellerContext _sellerCtx;
        public SellerController(SellerContext selerContext)
        {
            _sellerCtx = selerContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetSellerById(Guid id)
        {
            try
            {
                var seller = _sellerCtx.Sellers.Find(id);
                if (seller == null)
                {
                    return NotFound();
                }
                return Ok(seller);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}