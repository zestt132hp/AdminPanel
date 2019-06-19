using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IRepository<Announcement> _repositoryAnnouncement;
        private readonly IRepository<User> _repositoryUser;
        private readonly int _maxCountAnnouncement;

        public AnnouncementsController(IConfiguration config)
        {
            _repositoryAnnouncement = new AnnouncementRepos(config);
            _repositoryUser= new UserRepos(config);
            Int32.TryParse(config["MaxCountAnnouncement"], out var z);
            _maxCountAnnouncement = z;
        }
        /// <summary>
        /// Возвращает все объекты из базы данных
        /// </summary>
        /// <returns></returns>
        // GET: api/Announcements
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncement()
        {
            var result = await _repositoryAnnouncement.GetList();
            return result.Select(x => new AnnouncementDto() {Number = x.Number, UserName = x.User.Name, Text = x.Text});
        }

        /// <summary>
        /// Возвращает объект 
        /// </summary>
        /// <param name="number">номер объявления</param>
        /// <returns></returns>
        // GET: api/Announcements/5
        [HttpGet("{number}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)] 
        public async Task<IActionResult> GetAnnouncement(int number)
        {

            var announcements = await _repositoryAnnouncement.GetList();
            var annTmp = from b in announcements
                where b.Number == number
                select new AnnouncementDto()
                {
                    Number = b.Number,
                    UserName = b.User.Name,
                    Text = b.Text
                };
            annTmp = annTmp.ToList();
            if (!annTmp.Any())
                 return BadRequest();
            return Ok(annTmp.FirstOrDefault());
        }

        /// <summary>
        /// обновление объекта в базе данных
        /// </summary>
        /// <param name="number">номер объявления</param>
        /// <param name="announcementDto">объект</param>
        /// <returns></returns>
        // PUT: api/Announcements/5
        [HttpPut("{number}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutAnnouncement([FromRoute] int number, [FromBody] AnnouncementDto announcementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (number != announcementDto.Number)
            {
                return BadRequest();
            }

            var result = await _repositoryAnnouncement.GetList();
            var list = result.ToList();
            try
            {
                var annTmp = list.FirstOrDefault(x => x.Number == number);
                if (annTmp == null)
                    return NotFound();
                annTmp.Text = announcementDto.Text;
                annTmp.CreationDateTime = DateTime.Now;
                annTmp.Rate = 1;
                await _repositoryAnnouncement.Update(annTmp);
                _repositoryAnnouncement.Save();
                return Ok(announcementDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (list.Any(x=>x.Number == number))
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
        public async Task<IActionResult> PostAnnouncement([FromBody] AnnouncementDto announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repositoryUser.GetList();
            var enumerable = result.ToList();
            if (!(enumerable.Count() < _maxCountAnnouncement))
            {
                return CreatedAtAction("GetAnnouncement", new {number = announcement.Number},
                    "Превышен лимит количества объявлений в ситсеме");
            }
            var user = enumerable.FirstOrDefault(x => x.Name == announcement.UserName);
            if (user == null)
            {
                user = new User() {Name = announcement.UserName, UserId = new Guid()};
                await _repositoryUser.Create(user);
            }
            Announcement annTmp = new Announcement()
            {
                AnnouncementId = new Guid(), CreationDateTime = DateTime.Now,
                Number = enumerable.Count() + 1,
                Rate = 1,
                Text = announcement.Text,
                User = user
            };
            await _repositoryAnnouncement.Create(annTmp);
            _repositoryAnnouncement.Save();

            return CreatedAtAction("GetAnnouncement", new { number = announcement.Number }, announcement);
        }
        /// <summary>
        /// Удаляет объявление из базы данных
        /// </summary>
        /// <param name="number">уникальный guid объявления</param>
        /// <returns></returns>
        // DELETE: api/Announcements/5
        [HttpDelete("{number}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] int number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repositoryAnnouncement.GetList();
            result = result.ToList();
            var announcement = result.AsParallel().FirstOrDefault(x => x.Number == number);
            if (announcement == null)
            {
                return NotFound();
            }

            if (!await _repositoryAnnouncement.Delete(announcement.AnnouncementId))
                return BadRequest();

            _repositoryAnnouncement.Save();
            return Ok(announcement);
        }
    }
}