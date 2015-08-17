using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.UI;
using AutoMapper;
using ZKJL.Identity.Application.Configuration;
using ZKJL.Identity.Application.Menues.Dto;
using ZKJL.Identity.Application.Users.Dto;
using ZKJL.Identity.Core.Menues;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Application.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;

        public UserAppService(UserManager userManager, IRepository<User, long> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public PagedResultOutput<UserDto> GetUsers(GetUsersInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (input.MaxResultCount <= 0)
                {
                    input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.DefaultPageSize);
                }

                var userCount = _userRepository.Count();


                var users = _userRepository
                    .GetAll()
                    .Include(q => q.CreatorUser)
                    .OrderBy(input.Sorting)
                    .PageBy(input);


                return new PagedResultOutput<UserDto>
                       {
                           TotalCount = userCount,
                           Items = users.MapTo<List<UserDto>>()
                       };
            }
        }

        [AbpAuthorize("CanCreateUsers")] //An example of permission checking
        public async Task CreateUser(CreateUserInput input)
        {
            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, input.TenantId);

                await _userRepository.InsertAsync(new User(input.Name, input.UserName, input.Surname, input.EmailAddress, input.TenantId));
            }
        }

        [AbpAuthorize("CanEditUsers")] //An example of permission checking
        public async Task EditUser(EditUserInput input)
        {
            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, input.TenantId);
                var user = _userRepository.Get(input.Id);
                Mapper.CreateMap<EditUserInput, User>();
                input.MapTo<EditUserInput, User>(user);

                await _userRepository.UpdateAsync(user);
            }
        }

        [AbpAuthorize("CanDeleteUsers")] //An example of permission checking
        public async Task DeleteUser(GetUserInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user =
                   _userRepository
                       .GetAll()
                       .Include(q => q.CreatorUser)
                       .FirstOrDefault(q => q.Id == input.Id);

                if (user == null)
                {
                    throw new UserFriendlyException("There is no such a user. Maybe it's deleted.");
                }
                CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, user.TenantId);
                //await _userRepository.DeleteAsync(input.Id);
                await _userManager.DeleteAsync(user);
            }
        }

        public GetUserOutput GetUser(GetUserInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user =
                    _userRepository
                        .GetAll()
                        .Include(q => q.CreatorUser)
                        .FirstOrDefault(q => q.Id == input.Id);

                if (user == null)
                {
                    throw new UserFriendlyException("There is no such a user. Maybe it's deleted.");
                }

                return new GetUserOutput()
                {
                    User = user.MapTo<UserDto>()
                };
            }
        }
    }
}