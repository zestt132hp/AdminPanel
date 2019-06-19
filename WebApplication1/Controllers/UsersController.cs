using System;
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
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _context;

        public UsersController(IConfiguration config)
        {
            _context = new UserRepos(config);
        }

        /// <summary>
        /// Возвращает имеющиеся в базе данных объекты
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<JsonResult> GetUser()
        {
            var result = await _context.GetList();
            return new JsonResult(result.AsParallel().Select(x=> new UserDto(){FirstName = x.Name}));
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

            var result = await _context.GetList();
            var users = result.FirstOrDefault(x=>x.UserId == id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        /// <summary>
        /// Обновление объекта в базе данных
        /// </summary>
        /// <param name="users">объект</param>
        /// <returns></returns>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutUsers([FromBody] UserDto users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = await _context.GetList();
            var userTmp = list.AsParallel().FirstOrDefault(x => x.Name == users.FirstName);
            if (userTmp == null)
                return NotFound();
            try
            {
                await _context.Update(userTmp);
                _context.Save();
                return Ok(users);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
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
        public async Task<IActionResult> PostUsers([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userTmp = new User(){Name = user.FirstName, UserId = new Guid()};
            await _context.Create(userTmp);
            _context.Save();

            return CreatedAtAction("GetUsers", new { name = user.FirstName }, user);
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

            var users = await _context.Get(id);
            if (users == null)
            {
                return NotFound();
            }
            await _context.Delete(users.UserId);
            _context.Save();

            return Ok(users);
        }
    }
}