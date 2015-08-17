using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using ZKJL.Identity.Application.Configuration;
using ZKJL.Identity.Application.Menues;
using ZKJL.Identity.Application.Menues.Dto;
using ZKJL.Identity.Core.Menues;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Application.Menues
{
    [AbpAuthorize]
    public class MenuAppService : ApplicationService, IMenuAppService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly MenuManager _menuManager;
        private readonly INavigationManager _navigationManager;

        public IAbpSession AbpSession { get; set; }

        public MenuAppService(INavigationManager navigationManager, MenuManager menuManager, IRepository<Menu> menuRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _menuRepository = menuRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _menuManager = menuManager;
            _navigationManager = navigationManager;
        }

        public PagedResultOutput<MenuDto> GetMenues(GetMenuesInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.DefaultPageSize);
            }

            var menuCount = _menuRepository.Count();
            var menues = _menuRepository
                        .GetAll()
                        .Include(q => q.CreatorUser)
                        .OrderBy(input.Sorting)
                        .PageBy(input);

            return new PagedResultOutput<MenuDto>
                   {
                       TotalCount = menuCount,
                       Items = menues.MapTo<List<MenuDto>>()
                   };
        }

        public IEnumerable<MenuDto> GetAllMenues()
        {
            var menues = _menuRepository
                         .GetAll()
                         .Include(q => q.CreatorUser);

            return menues.MapTo<IEnumerable<MenuDto>>();
        }

        public void SetMenu()
        {
            IQueryable<Menu> menues;
            if (AbpSession.TenantId.HasValue)
            {
                menues = _menuRepository
                        .GetAll()
                        .Where(p => p.TenantId == AbpSession.TenantId)
                        .Include(q => q.CreatorUser);
            }
            else
            {
                menues = _menuRepository
                       .GetAll()
                       .Include(q => q.CreatorUser);
            }

            if (_navigationManager.MainMenu.Items.Count > 0)
                _navigationManager.MainMenu.Items.Clear();

            _menuManager.SetMenu(_navigationManager.MainMenu, menues.MapTo<IEnumerable<MenuDto>>());
        }

        [AbpAuthorize("CanCreateMenues")] //An example of permission checking
        public async Task CreateMenu(CreateMenuInput input)
        {
            await _menuRepository.InsertAsync(new Menu(input.Name, input.DisplayName, AbpSession.TenantId, input.ParentId, input.Icon, input.Url, input.RequiresAuthentication, input.RequiredPermissionName, input.Order));
        }

        [AbpAuthorize("CanEditMenues")] //An example of permission checking
        public async Task EditMenu(EditMenuInput input)
        {
            var menu = _menuRepository.Get(input.Id);
            Mapper.CreateMap<EditMenuInput, Menu>();
            input.MapTo<EditMenuInput, Menu>(menu);
            await _menuRepository.UpdateAsync(menu);
        }

        [AbpAuthorize("CanDeleteMenues")] //An example of permission checking
        public async Task DeleteMenu(GetMenuInput input)
        {
            await _menuRepository.DeleteAsync(input.Id);
        }

        public GetMenuOutput GetMenu(GetMenuInput input)
        {
            var menu =
                _menuRepository
                    .GetAll()
                    .Include(q => q.CreatorUser)
                    .Include(q => q.Items)
                    .FirstOrDefault(q => q.Id == input.Id);

            if (menu == null)
            {
                throw new UserFriendlyException("There is no such a menu. Maybe it's deleted.");
            }

            return new GetMenuOutput()
                   {
                       Menu = menu.MapTo<MenuDto>()
                   };
        }
    }
}