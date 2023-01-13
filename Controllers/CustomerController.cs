using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _customerCtx;
        public CustomerController(CustomerContext customerContext)
        {
            _customerCtx = customerContext;
        }

        [HttpPost]
        public IActionResult CustomerRegister(Customer customer)
        {
            Customer c = new Customer();
            if (customer.Name == null)
            {
                return BadRequest(new { Error = "Invalid or not received customer name" });
            }
            if (customer.Address == null)
            {
                return BadRequest(new { Error = "Invalid or not received customer address" });
            }
            if (customer.CPF == null || !c.IsCpfMatch(customer.CPF))
            {
                return BadRequest(new { Error = "Invalid or not received customer CPF" });
            }
            customer.Active = true;
            try
            {
                _customerCtx.Customers.Add(customer);
                _customerCtx.SaveChanges();
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Error = "Invalid customer data" });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(Guid id)
        {
            try
            {
                var customer = _customerCtx.Customers.Find(id);

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}