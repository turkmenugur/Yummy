using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.NotificationDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public NotificationsController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        //Listeleme işleminde mapleme
        [HttpGet]
        public IActionResult NotificationList()
        {
            var notifications = _context.Notifications.ToList();
            return Ok(_mapper.Map<List<ResultNotificationDto>>(notifications));
        }
        
        //Oluşturma işleminde mapleme
        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var notification = _mapper.Map<Notification>(createNotificationDto);
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }
        
        //Silme işleminde mapleme yok
        [HttpDelete]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı");
        }
        
        //Id'ye göre getime işleminde mapleme
        [HttpGet("GetNotification")]
        public IActionResult GetNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            return Ok(_mapper.Map<GetNotificationByIdDto>(notification));
        }
        
        //Güncelleme işleminde mapleme
        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var notification = _mapper.Map<Notification>(updateNotificationDto);
            _context.Notifications.Update(notification);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }
    }
}
