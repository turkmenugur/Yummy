using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.ProductDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _validator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IValidator<Product> validator, ApiContext context, IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage));
            }
            else
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok(new {message = "Ürün başarıyla eklendi", data = product});
            }
        }
        
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound("Ürün bulunamadı");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Ürün başarıyla silindi");
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage));
            }
            else
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return Ok(new {message = "Ürün başarıyla güncellendi", data = product});
            }
        }

        [HttpPost("CreateProductWithCategory")]
        public IActionResult CreateProductWithCategory(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(new { message = "Ürün başarıyla eklendi", data = product });
        }

        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var products = _context.Products.Include(x => x.Category).ToList();
            return Ok(_mapper.Map<List<ResultProductWithCategoryDto>>(products));
        }
    }
}
