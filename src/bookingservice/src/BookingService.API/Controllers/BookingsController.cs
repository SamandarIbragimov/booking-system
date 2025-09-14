using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using BookingService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _repository;

        public BookingsController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _repository.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _repository.GetByIdAsync(id);
            return booking is null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            booking.Id = Guid.NewGuid();
            booking.Status = BookingStatus.Pending;

            await _repository.AddAsync(booking);

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking is null) return NotFound();

            booking.Status = BookingStatus.Confirmed;
            await _repository.UpdateAsync(booking);
            return Ok(booking);
        }

        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking is null) return NotFound();

            booking.Status = BookingStatus.Cancelled;
            await _repository.UpdateAsync(booking);
            return Ok(booking);
        }
    }
}
