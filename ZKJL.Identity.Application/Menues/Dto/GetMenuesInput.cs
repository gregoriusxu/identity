using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;

namespace ZKJL.Identity.Application.Menues.Dto
{
    public class GetMenuesInput : IInputDto, IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        [Range(0, 1000)]
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "CreationTime DESC", "Name DESC", "DisplayName DESC", "Order DESC" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
}