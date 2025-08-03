using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.AboutDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext  _context;
        private readonly IMapper _mapper;

        public AboutsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var AboutList = _context.Abouts.ToList();
            return Ok(AboutList);
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            var value = _mapper.Map<About>(createAboutDto);
            _context.Abouts.Add(value);
            _context.SaveChanges();
            return Ok("Hakkımda alanı ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            _context.Abouts.Remove(value);
            _context.SaveChanges();
            return Ok("Hakkımda alanı silindi");
        }

        [HttpGet("GetAbout")]
        public IActionResult GetAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var value = _mapper.Map<About>(updateAboutDto);
            _context.Abouts.Update(value);
            _context.SaveChanges();
            return Ok("Hakkımda alanı güncelleme işlemi başarılı");
        }
    }
}
