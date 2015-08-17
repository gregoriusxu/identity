using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ZKJL.Identity.Application.Users.Dto;

namespace ZKJL.Identity.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        PagedResultOutput<UserDto> GetUsers(GetUsersInput input);

        Task CreateUser(CreateUserInput input);

        Task EditUser(EditUserInput input);

        Task DeleteUser(GetUserInput input);

        GetUserOutput GetUser(GetUserInput input);
    }
}
