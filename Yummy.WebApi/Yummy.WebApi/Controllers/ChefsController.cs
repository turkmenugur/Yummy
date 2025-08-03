using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ChefList()
        {
            return Ok(_context.Chefs.ToList());
        }

        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return Ok("Şef ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var chef = _context.Chefs.Find(id);
            _context.Chefs.Remove(chef);
            _context.SaveChanges();
            return Ok("Şef silme işlemi başarılı");
        }

        [HttpGet("GetChef")]
        public IActionResult GetChef(int id)
        {
            return Ok(_context.Chefs.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            _context.Chefs.Update(chef);
            _context.SaveChanges();
            return Ok("Şef güncelleme işlemi başarılı");
        }
    }
}
