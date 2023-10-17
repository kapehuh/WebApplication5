using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.Internal;
using System.Text;
using System.Text.Json;
using WebApplication5.Abstractions;
using WebApplication5.Domain.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        //private readonly ILogger<UserController> _logger;
        //public UserController(ILogger<UserController> logger)
        //{
        //    _logger = logger;
        //}

        // ================================================================================================
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        // ================================================================================================
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> Get(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest($"Cant find user with id = {id}");
            }
        }

        // ================================================================================================
        // POST api/<UserController>
        [HttpPost]
        public async Task<User> Post([FromBody] User entity)
        {
            //entity.id = Guid.NewGuid();
            await _userRepository.AddAsync(entity);
            return entity;
        }

        // ================================================================================================
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User entity)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return BadRequest("Пользователь не существует");
            }

            await _userRepository.UpdateAsync(entity);
            return Ok();
        }

        // ================================================================================================
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return BadRequest("Пользователь не существует");
            }

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }


        
    }
}
