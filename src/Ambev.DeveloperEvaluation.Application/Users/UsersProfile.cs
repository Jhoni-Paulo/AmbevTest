using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<CreateUserCommand, User>()
        .ForCtorParam("username", opt => opt.MapFrom(src => src.Username))
        .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
        .ForCtorParam("phone", opt => opt.MapFrom(src => src.Phone))
        .ForCtorParam("role", opt => opt.MapFrom(src => src.Role))
        .ForCtorParam("status", opt => opt.MapFrom(src => src.Status))    
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Password, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        
        CreateMap<User, CreateUserResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}