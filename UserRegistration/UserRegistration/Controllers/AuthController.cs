using Ardalis.Result;
using AutoMapper;
using Core.Entities.UserAggregate;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Controllers;

[ApiController]
[Route("controller")]
public class AuthController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("/CreateUser")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequestModel request,
        [FromServices] IMapper mapper,
        [FromServices] IUserService userService,
        [FromServices] ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Code = "URAPI4000",
                    Message = "Error filling out the form"
                });
            }

            var newUser = mapper.Map<User>(request);

            var createdUser = await userService.CreateUserAsync(newUser, cancellationToken);

            if (createdUser.Status == ResultStatus.Invalid) return BadRequest(new
            {
                Code = "URAPI4003",
                Message = "Email already in use"
            });

            var response = mapper.Map<LoginResponseModel>(createdUser.Value);

            response.AccessToken = tokenService.GenerateToken(createdUser.Value);

            return Created("User Created", new
            {
                Code = "URAPI2010",
                Message = $"Request Completed Successfully",
                response = response
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Code = "URAPI5000",
                Message = $"Internal Server Error: {ex.Message}"
            });
        }
    }

    [AllowAnonymous]
    [HttpPost("/Login")]
    public async Task<IActionResult> Login(
        [FromServices] IMapper mapper,
        [FromServices] IUserService userService,
        [FromServices] ITokenService tokenService,
        [FromBody] LoginRequestModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Code = "URAPI4001",
                    Message = "Error filling out the form"
                });
            }

            var user = await userService.LoginUserAsync(request.Email, request.Password, cancellationToken);

            if (user.Status == ResultStatus.NotFound)
            {
                return NotFound(new
                {
                    Code = "URAPI4040",
                    Message = "User Not Found"
                });
            }

            if (user.Status == ResultStatus.Invalid)
            {
                return BadRequest(new
                {
                    Code = "URAPI4002",
                    Message = "Incorrect Password"
                });
            }

            if (user.Status == ResultStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Code = "URAPI5001",
                    Message = $"Internal Server Error"
                });

            var response = mapper.Map<LoginResponseModel>(user.Value);

            response.AccessToken = tokenService.GenerateToken(user.Value);

            return Ok(new
            {
                Code = "URAPI2000",
                Message = $"Request Completed Successfully",
                response = response
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Code = "URAPI5002",
                Message = $"Internal Server Error: {ex.Message}"
            });
        }
    }
}