using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.FeatureDtos;
using Yummy.WebApi.Dtos.MessageDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext  _context;

        public MessagesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var messages = _context.Messages.ToList();
            return Ok(_mapper.Map<List<ResultMessageDto>>(messages));
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            var  message = _mapper.Map<Message>(createMessageDto);
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok("Mesaj oluşturma işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.Find(id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return Ok("Mesaj silindi");
        }

        [HttpGet("GetMessage")]
        public IActionResult GetMessage(int id)
        {
            var message = _context.Messages.Find(id);
            return Ok(_mapper.Map<GetByIdMessageDto>(message));
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var message = _mapper.Map<Message>(updateMessageDto);
            _context.Messages.Update(message);
            _context.SaveChanges();
            return Ok("Mesaj güncellendi");
        }

        [HttpGet("MessageListByIsReadFalse")]
        public IActionResult MessageListByIsReadFalse()
        {
            var messages = _context.Messages.Where(m => m.IsRead == false).ToList();
            return Ok(messages);
            //return Ok(_mapper.Map<List<ResultMessageDto>>(messages));
        }
        
    }
}
