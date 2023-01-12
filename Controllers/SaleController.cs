using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleContext _saleCtx;
        private readonly SellerContext _sellerCtx;
        private readonly CustomerContext _customerCtx;
        public SaleController(SaleContext saleContext, SellerContext sellerContext, CustomerContext customerContext)
        {
            _saleCtx = saleContext;
            _sellerCtx = sellerContext;
            _customerCtx = customerContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(Guid id)
        {
            try
            {
                var sale = _saleCtx.Sales.Find(id);
                if (sale == null)
                {
                    return NotFound();
                }
                return Ok(sale);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}