using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.ContactDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContactsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _context.Contacts.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            Contact contact = new Contact();
            contact.MapLocation = createContactDto.MapLocation;
            contact.Address = createContactDto.Address;
            contact.Phone = createContactDto.Phone;
            contact.Email = createContactDto.Email;
            contact.OpenHours = createContactDto.OpenHours;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı");
        }

        [HttpGet("GetContact")]
        public IActionResult GetContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            return Ok(contact);
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            Contact contact = new Contact();
            contact.MapLocation = updateContactDto.MapLocation;
            contact.Address = updateContactDto.Address;
            contact.Phone = updateContactDto.Phone;
            contact.Email = updateContactDto.Email;
            contact.OpenHours = updateContactDto.OpenHours;
            contact.ContactId = updateContactDto.ContactId;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }
    }
}
