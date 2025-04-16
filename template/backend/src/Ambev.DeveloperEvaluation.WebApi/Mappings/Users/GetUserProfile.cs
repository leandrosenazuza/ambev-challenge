using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Users
{
    public class GetUserProfileRequest : Profile
    {
        public GetUserProfileRequest()
        {
            CreateMap<GetUserResult, GetUserResponse>();
        }

    }
}
