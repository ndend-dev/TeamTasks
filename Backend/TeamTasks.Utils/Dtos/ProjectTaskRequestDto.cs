using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeamTasks.Utils.Dtos
{
    public class ProjectTaskRequestDto
    {
        [Required]
        public Guid? ProjectId { get; set; } = default(Guid?);
        [AllowNull]
        public Guid? StatusId { get; set; }  = default(Guid?);
        [AllowNull]
        public Guid? DeveloperId { get; set; } = default(Guid?);

    }
}
