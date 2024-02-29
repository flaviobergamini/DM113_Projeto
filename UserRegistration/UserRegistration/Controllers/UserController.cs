using Ardalis.Result;
using AutoMapper;
using Core.Entities.UserAggregate;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserRegistration.Models;

namespace UserRegistration.Controllers;

[ApiController]
[Route("controller")]
public class UserController : ControllerBase
{
    private Result<string> GetIdUser()
    {
        var user = User.Identity as ClaimsIdentity;

        var claims = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (claims == null)
            return Result.Unauthorized();

        return Result.Success(claims.Value);
    }


    [HttpGet("/GetUser")]
    public async Task<IActionResult> GetUserById(
       [FromServices] IUserService userService,
       [FromServices] IMapper mapper,
       CancellationToken cancellationToken)
    {
        try
        {
            var id = GetIdUser();

            if (!id.IsSuccess)
                return Unauthorized(new
                {
                    Code = "URAPI4010",
                    Message = "User Not Authorized"
                });

            var result = await userService.GetUserByIdAsync(id, cancellationToken);

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(new
                {
                    Code = "URAPI4041",
                    Message = "User Not Found"
                });
            }

            if (result.Status == ResultStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Code = "URAPI5003",
                    Message = $"Internal Server Error"
                });

            var response = mapper.Map<UserResponseModel>(result.Value);

            return Ok(new
            {
                Code = "URAPI2001",
                Message = $"Request Completed Successfully",
                response = response
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Code = "URAPI5004",
                Message = $"Internal Server Error: {ex.Message}"
            });
        }
    }

    [HttpPut("/UpdateUser")]
    public async Task<IActionResult> UpdateUser(
        [FromServices] IUserService userService,
        [FromServices] IMapper mapper,
        [FromBody] UpdateUserRequestModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Code = "URAPI4002",
                    Message = "Error filling out the form"
                });
            }

            var id = GetIdUser();

            if (!id.IsSuccess)
                return Unauthorized(new
                {
                    Code = "URAPI4011",
                    Message = "User Not Authorized"
                });

            var user = mapper.Map<User>(request);

            var result = await userService.UpdateUserAsync(id, user, cancellationToken);

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(new
                {
                    Code = "URAPI4042",
                    Message = "User Not Found"
                });
            }

            if (result.Status == ResultStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Code = "URAPI5005",
                    Message = $"Internal Server Error"
                });

            var response = mapper.Map<UserResponseModel>(result.Value);

            return Ok(new
            {
                Code = "URAPI2002",
                Message = $"Request Completed Successfully",
                response = response
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Code = "URAPI5006",
                Message = $"Internal Server Error: {ex.Message}"
            });
        }
    }

    [HttpDelete("/DeleteUser")]
    public async Task<IActionResult> DeleteUserById(
        [FromServices] IUserService userService,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = GetIdUser();

            if (!id.IsSuccess)
                return Unauthorized(new
                {
                    Code = "URAPI4012",
                    Message = "User Not Authorized"
                });

            var result = await userService.DeleteUserByIdAsync(id, cancellationToken);


            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(new
                {
                    Code = "URAPI4042",
                    Message = "User Not Found"
                });
            }

            if (result.Status == ResultStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Code = "URAPI5007",
                    Message = $"Internal Server Error"
                });

            return Ok(new
            {
                Code = "URAPI2003",
                Message = $"User {result.Value.Name} successfully deleted"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Code = "URAPI5008",
                Message = $"Internal Server Error: {ex.Message}"
            });
        }
    }
}