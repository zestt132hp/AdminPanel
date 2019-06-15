using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdministratorPanelMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdministratorPanelMvc.Models;

namespace AdministratorPanelMvc.controllers
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

        // GET: api/Announcements
        [HttpGet]
        public IEnumerable<Announcement> GetAnnouncement()
        {
            return _context.Announcement.Include(u=> u.User).ToList(); 
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
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

        // PUT: api/Announcements/5
        [HttpPut("{id}")]
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
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Announcements
        [HttpPost]
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

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
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