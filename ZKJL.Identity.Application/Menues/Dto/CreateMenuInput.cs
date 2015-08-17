using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Localization;

namespace ZKJL.Identity.Application.Menues.Dto
{
    public class CreateMenuInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string DisplayName { get; set; }

        public int? ParentId { get; set; }

        public int Order { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string RequiredPermissionName { get; set; }

        public bool RequiresAuthentication { get; set; }
    }
}