using AutoMapper;
using Core.Entities.UserAggregate;
using UserRegistration.Models;

namespace UserRegistration.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserRequestModel, User>();
        CreateMap<LoginResponseModel, User>().ReverseMap();
        CreateMap<UserResponseModel, User>().ReverseMap();
        CreateMap<UpdateUserRequestModel, User>();
    }
}