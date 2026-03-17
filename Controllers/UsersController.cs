using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static Dictionary<int, User> _users = new Dictionary<int, User>();
        private int currId = 1;

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll() => Ok(_users);

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            _users.TryGetValue(id, out var user);
            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> Create(User newUser)
        {
            if (!IsUserValid(newUser))
            {
                return BadRequest("User has missing or invalid data");
            }

            newUser.Id = currId;
            currId ++;
            _users.Add(newUser.Id, newUser);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            _users.TryGetValue(id, out var user);
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
            if (_users.ContainsKey(id)) return NotFound();

            _users.Remove(id);
            return NoContent();
        }

        #region private methods
        private bool IsUserValid(User newUser)
        {
            if (string.IsNullOrEmpty(newUser.FirstName) ||
                string.IsNullOrEmpty(newUser.LastName) ||
                string.IsNullOrEmpty(newUser.Email) ||
                string.IsNullOrEmpty(newUser.Department))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}