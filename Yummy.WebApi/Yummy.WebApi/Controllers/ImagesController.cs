using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.ImageDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApiContext  _context;
        private readonly IMapper _mapper;

        public ImagesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ImageList()
        {
            var ImageList = _context.Images.ToList();
            return Ok(ImageList);
        }

        [HttpPost]
        public IActionResult CreateImage(CreateImageDto createImageDto)
        {
            var value = _mapper.Map<Image>(createImageDto);
            _context.Images.Add(value);
            _context.SaveChanges();
            return Ok("Görsel ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteImage(int id)
        {
            var Image = _context.Images.Find(id);
            _context.Images.Remove(Image);
            _context.SaveChanges();
            return Ok("Görsel silindi");
        }

        [HttpGet("GetImage")]
        public IActionResult GetImage(int id)
        {
            var Image = _context.Images.Find(id);
            return Ok(Image);
        }

        [HttpPut]
        public IActionResult UpdateImage(UpdateImageDto updateImageDto)
        {
            var value = _mapper.Map<Image>(updateImageDto);
            _context.Images.Update(value);
            _context.SaveChanges();
            return Ok("Görsel güncelleme işlemi başarılı");
        }
    }
}
