using Ardalis.Result;
using Core.Entities.UserAggregate;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services.Base;
using SecureIdentity.Password;

namespace Core.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    //private readonly ITokenService _tokenService;

    public UserService(
        IUserRepository userRepository)
        //ITokenService tokenService)
    {
        _userRepository = userRepository;
        //_tokenService = tokenService;
    }

    public async Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            user.Password = PasswordHasher.Hash(user.Password);

            var createdUser = await _userRepository.CreateUserAsync(user, cancellationToken);

            return Result.Success(createdUser);
        }
        catch (Exception)
        {
            return Result.Error();
        }
    }

    public async Task<Result<User>> LoginUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user == null)
                return Result.NotFound("Usuário não encontrado");

            if (!PasswordHasher.Verify(user.Password, password))
                return Result.Invalid();


            return Result.Success(user);
        }
        catch (Exception)
        {
            return Result.Error("");
        }
    }

    public async Task<Result<User>> UpdateUserAsync(string id, User user, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (userId == null)
                return Result.NotFound("Usuário não encontrado");

            userId.Name = user.Name;
            userId.Surname = user.Surname;
            userId.Email = user.Email;
            userId.UpdatedAt = DateTimeOffset.UtcNow;

            var result = await _userRepository.UpdateUserAsync(id, userId, cancellationToken);

            if (result)
                return Result<User>.Success(user);

            return Result.Invalid();
        }
        catch (Exception)
        {
            return Result.Error();
        }
    }

    public async Task<Result<User>> DeleteUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var verifyUser = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (verifyUser == null)
                return Result.NotFound("Usuário não encontrado");

            var result = await _userRepository.DeleteUserByIdAsync(id, cancellationToken);

            if (result)
                return Result<User>.Success(verifyUser);

            return Result.Invalid();
        }
        catch (Exception)
        {
            return Result.Error();
        }
    }

    public async Task<Result<User?>> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return Result.NotFound("Usuário não encontrado");

            return Result<User>.Success(user);
        }
        catch (Exception)
        {
            return Result.Error();
        }
    }
}