using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

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

        [HttpPost]
        public IActionResult SaleRegister(Sale sale)
        {
            var seller = _sellerCtx.Sellers.Find(sale.SellerId);
            var customer = _customerCtx.Customers.Find(sale.CustomerId);

            if (sale.Items == null || sale.Items.Count() < 1)
            {
                return BadRequest(new { Erro = "Not received item list or quantity less than 1!" });
            }
            if (sale.CustomerId == null || !customer.Active)
            {
                return BadRequest(new { Erro = "Invalid and/or inactive customer" });
            }
            if (sale.SellerId == null || !seller.Active)
            {
                return BadRequest(new { Erro = "Invalid and/or inactive seller" });
            }
            sale.Status = EnumSaleStatus.AwaitingPayment;
            try
            {
                _saleCtx.Sales.Add(sale);
                _saleCtx.SaveChanges();
                return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Error = "Invalid sale data" });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
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

        [HttpPatch("{id}")]
        public IActionResult UpdateSaleStatus(Guid id, Sale sale)
        {
            string ErrorMessage = "invalid status";
            var saleDatabase = _saleCtx.Sales.Find(id);
            if (saleDatabase == null)
            {
                return NotFound();
            }

            switch (saleDatabase.Status)
            {
                case EnumSaleStatus.AwaitingPayment:
                    if (sale.Status != EnumSaleStatus.PaymentAccept && sale.Status != EnumSaleStatus.canceled)
                    {
                        return BadRequest(new { Error = ErrorMessage });
                    }
                    break;
                case EnumSaleStatus.PaymentAccept:
                    if (sale.Status != EnumSaleStatus.SentToCarrier && sale.Status != EnumSaleStatus.canceled)
                    {
                        return BadRequest(new { Error = ErrorMessage });
                    }
                    break;
                case EnumSaleStatus.SentToCarrier:
                    if (sale.Status != EnumSaleStatus.Delivered)
                    {
                        return BadRequest(new { Error = ErrorMessage });
                    }
                    break;
            }
            saleDatabase.Status = sale.Status;
            try
            {
                _saleCtx.Sales.Update(saleDatabase);
                _saleCtx.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Error = "Invalid sale data" });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}