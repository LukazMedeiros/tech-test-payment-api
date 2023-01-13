
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

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

        [HttpPost]
        public IActionResult SellerRegister(Seller seller)
        {
            Seller s = new Seller();
            if (seller.Name == null)
            {
                return BadRequest(new { Error = "Invalid or not received seller name" });
            }
            if (seller.CPF == null || !s.IsCpfMatch(seller.CPF))
            {
                return BadRequest(new { Error = "Invalid or not received seller CPF" });
            }
            seller.Active = true;
            try
            {
                _sellerCtx.Sellers.Add(seller);
                _sellerCtx.SaveChanges();
                return CreatedAtAction(nameof(GetSellerById), new { id = seller.Id }, seller);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Error = "Invalid seller data" });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
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