using Abp.Application.Services.Dto;
using ZKJL.Identity.Core.Menues;

namespace ZKJL.Identity.Application.Menues.Dto
{
    public class GetMenuOutput : IOutputDto
    {
        public MenuDto Menu { get; set; }
    }
}