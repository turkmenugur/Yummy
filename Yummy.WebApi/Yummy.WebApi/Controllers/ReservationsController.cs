using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Dtos.ReservationDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApiContext  _context;
        private readonly IMapper _mapper;

        public ReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ReservationList()
        {
            var reservationList = _context.Reservations.ToList();
            return Ok(reservationList);
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var value = _mapper.Map<Reservation>(createReservationDto);
            _context.Reservations.Add(value);
            _context.SaveChanges();
            return Ok("Rezervasyon ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return Ok("Rezervasyon silindi");
        }

        [HttpGet("GetReservation")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            return Ok(reservation);
        }

        [HttpPut]
        public IActionResult UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            _context.Reservations.Update(value);
            _context.SaveChanges();
            return Ok("Rezervasyon güncelleme işlemi başarılı");
        }
    }
}
