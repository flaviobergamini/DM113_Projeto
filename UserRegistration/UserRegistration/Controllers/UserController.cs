using Ardalis.Result;
using Core.Entities.UserAggregate;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserRegistration.Controllers
{
    [ApiController]
    [Route("controller")]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("/CreateUser")]
        public async Task<IActionResult> CreateUser(
       [FromServices] IUserService userService,
       CancellationToken cancellationToken)
        {

            var newUser = new User
            {
                Name = "Flávio",
                Email = "t@t.com",
                Password = "123456",
                Deleted = false,
                Surname = "Bergamini",
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await userService.CreateUserAsync(newUser, cancellationToken);

            if (createdUser.Status == ResultStatus.Invalid) return BadRequest(createdUser.Errors);


            return Ok("Funcionou");
        }

    }
}
