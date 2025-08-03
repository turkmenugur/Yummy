using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.FeatureDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public FeaturesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        //Listeleme işleminde mapleme
        [HttpGet]
        public IActionResult FeatureList()
        {
            var features = _context.Features.ToList();
            return Ok(_mapper.Map<List<ResultFeatureDto>>(features));
        }
        
        //Oluşturma işleminde mapleme
        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var feature = _mapper.Map<Feature>(createFeatureDto);
            _context.Features.Add(feature);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }
        
        //Silme işleminde mapleme yok
        [HttpDelete]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _context.Features.Find(id);
            _context.Features.Remove(feature);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı");
        }
        
        //Id'ye göre getime işleminde mapleme
        [HttpGet("GetFeature")]
        public IActionResult GetFeature(int id)
        {
            var feature = _context.Features.Find(id);
            return Ok(_mapper.Map<GetByIdFeatureDto>(feature));
        }
        
        //Güncelleme işleminde mapleme
        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var feature = _mapper.Map<Feature>(updateFeatureDto);
            _context.Features.Update(feature);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }
    }
}
