using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{

    [Route("api/[controller]")]
    public class ProductsController: Controller
    {
        private readonly VegaDbContext _context;

        public ProductsController(VegaDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var item = _context.Products.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Products.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = item.Id }, item);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = item.Name;
            product.Quantity = item.Quantity;
            product.UnitPrice = item.UnitPrice;
            product.CategoryId = item.CategoryId;
            product.ImageUrl = item.ImageUrl;

            _context.Products.Update(product);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}