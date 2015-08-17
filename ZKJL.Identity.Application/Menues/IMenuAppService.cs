using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ZKJL.Identity.Application.Menues.Dto;
using ZKJL.Identity.Core.Menues;

namespace ZKJL.Identity.Application.Menues
{
    public interface IMenuAppService : IApplicationService
    {
        PagedResultOutput<MenuDto> GetMenues(GetMenuesInput input);

        Task CreateMenu(CreateMenuInput input);

        Task EditMenu(EditMenuInput input);

        Task DeleteMenu(GetMenuInput input);

        GetMenuOutput GetMenu(GetMenuInput input);

        IEnumerable<MenuDto> GetAllMenues();

        void SetMenu();
    }
}
