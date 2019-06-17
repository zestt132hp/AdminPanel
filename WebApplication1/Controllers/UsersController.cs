using System;
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
    public class UsersController : ControllerBase
    {
        private readonly AdministratorContext _context;

        public UsersController(AdministratorContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает имеющиеся в базе данных объекты
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200)]
        public JsonResult GetUser()
        {
            return new JsonResult(_context.Users.ToList());
        }
        /// <summary>
        /// Возвращает объект по Id
        /// </summary>
        /// <param name="id">уникальный guid</param>
        /// <returns></returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        /// <summary>
        /// Обновление объекта в базе данных
        /// </summary>
        /// <param name="id">уникальный идентификатор объекта</param>
        /// <param name="users">объект</param>
        /// <returns></returns>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutUsers([FromRoute] Guid id, [FromBody] User users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(users);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        /// добавление в базу данных нового объекта
        /// </summary>
        /// <param name="user">новый "валидный" объект</param>
        /// <returns></returns>
        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostUsers([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.UserId }, user);
        }
        /// <summary>
        /// Проводит удаление объекта из базы данных
        /// </summary>
        /// <param name="id">уникальный идентификатор объекта</param>
        /// <returns></returns>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUsers([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}