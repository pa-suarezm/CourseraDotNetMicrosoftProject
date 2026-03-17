using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Alice", LastName = "Tech", Email = "alice@techhive.com", Department = "IT" }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll() => Ok(_users);

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> Create(User newUser)
        {
            newUser.Id = _users.Max(u => u.Id) + 1;
            _users.Add(newUser);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _users.Remove(user);
            return NoContent();
        }
    }
}