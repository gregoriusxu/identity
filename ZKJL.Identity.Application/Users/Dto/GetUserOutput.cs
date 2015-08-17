using Abp.Application.Services.Dto;

namespace ZKJL.Identity.Application.Users.Dto
{
    public class GetUserOutput : IOutputDto
    {
        public UserDto User { get; set; }
    }
}