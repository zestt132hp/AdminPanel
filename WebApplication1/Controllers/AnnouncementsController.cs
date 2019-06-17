using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AdministratorContext _context;

        public AnnouncementsController(AdministratorContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращает все объекты из базы данных
        /// </summary>
        /// <returns></returns>
        // GET: api/Announcements
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Announcement> GetAnnouncement()
        {
            return _context?.Announcement?.Include(u=> u.User).ToList(); 
        }
        /// <summary>
        /// Возвращает объект 
        /// </summary>
        /// <param name="id">уникальный guid объекта</param>
        /// <returns></returns>
        // GET: api/Announcements/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAnnouncement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = await _context.Announcement.FindAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            return Ok(announcement);
        }
        /// <summary>
        /// обновление объекта в базе данных
        /// </summary>
        /// <param name="id">уникальный guid объекта</param>
        /// <param name="announcement">объект</param>
        /// <returns></returns>
        // PUT: api/Announcements/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutAnnouncement([FromRoute] Guid id, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != announcement.AnnouncementId)
            {
                return BadRequest();
            }

            _context.Entry(announcement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(announcement);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NoContent();
                }
                
            }
        }
        /// <summary>
        /// Проводит сохранение объявления 
        /// </summary>
        /// <param name="announcement">валидный объект для сохранения в базе данных</param>
        /// <returns></returns>
        // POST: api/Announcements
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAnnouncement([FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Announcement.Add(announcement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncement", new { id = announcement.AnnouncementId }, announcement);
        }
        /// <summary>
        /// Удаляет объявление из базы данных
        /// </summary>
        /// <param name="id">уникальный guid объявления</param>
        /// <returns></returns>
        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = await _context.Announcement.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcement.Remove(announcement);
            await _context.SaveChangesAsync();

            return Ok(announcement);
        }

        private bool AnnouncementExists(Guid id)
        {
            return _context.Announcement.Any(e => e.AnnouncementId == id);
        }
    }
}