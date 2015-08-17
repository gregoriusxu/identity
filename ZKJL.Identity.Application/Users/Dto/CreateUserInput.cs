using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Localization;

namespace ZKJL.Identity.Application.Users.Dto
{
    public class CreateUserInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public int? TenantId { get; set; }
    }
}