using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly List<User> _users;

        public UsersController()
        {
            // Initialize with some dummy data
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Gabriel", LastName = "Dimitrovski", Age = 8, Email = "gabriel@example.com" },
                new User { Id = 2, FirstName = "Hristina", LastName = "Dimitrovska", Age = 36, Email = "hristina@example.com" },
                new User { Id = 3, FirstName = "Angelcho", LastName = "Dimitrovski", Age = 40, Email = "angelcho@example.com" }
            };
        }

        // GET: api/users
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Post(User user)
        {
            user.Id = _users.Count + 1; // Assign a new ID
            _users.Add(user);

            // Return the created user and its URL in the response
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
                return NotFound();

            _users.Remove(user);

            return NoContent();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
