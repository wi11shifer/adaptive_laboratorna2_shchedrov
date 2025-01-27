using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Adaptive1.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace Adaptive1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("validate")]
        public IActionResult ValidateUser([FromBody] User user)
        {
            return Ok(new { Message = "User is valid!" });
        }

        [HttpPost("save")]
        public IActionResult SaveUser([FromBody] User user)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Users.json");
            var users = new List<User>();

            if (System.IO.File.Exists(filePath))
            {
                var existingData = System.IO.File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<List<User>>(existingData) ?? new List<User>();
            }

            users.Add(user);
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(users, Formatting.Indented));

            _logger.LogInformation($"User {user.Name} saved successfully.");

            return Ok(new { Message = "User saved!" });
        }

        [HttpGet("regex")]
        public IActionResult RegexExample(string input)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            var isValid = regex.IsMatch(input);

            return Ok(new { Input = input, IsValid = isValid });
        }
    }
}
