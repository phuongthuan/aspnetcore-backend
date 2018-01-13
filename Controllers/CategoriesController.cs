using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController: Controller
    {
        private readonly VegaDbContext _context;

        public CategoriesController(VegaDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}", Name = "GetCat")]
        public IActionResult GetById(int id)
        {
            var item = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Categories.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCat", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = item.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}