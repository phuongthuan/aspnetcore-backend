using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController: Controller
    {
        private readonly VegaDbContext _context;

        public OrdersController(VegaDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetById(int id)
        {
            var item = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}