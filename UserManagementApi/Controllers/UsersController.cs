using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("allusers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("createuser")]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Length < 2)
            {
                return BadRequest(new { message = "FullName is required and must be at least 2 characters long." });
            }

            if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest(new { message = "Email is required and must be a valid email format." });
            }

            if (user.DateOfBirth > DateTime.Today)
            {
                return BadRequest(new { message = "DateOfBirth can`t be in the future." });
            }

            var newUser = _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userRepository.DeleteUser(id);
            if (!deleted)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
