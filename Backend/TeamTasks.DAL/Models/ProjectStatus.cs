using System.Text.Json.Serialization;

namespace TeamTasks.DAL.Models;

public partial class ProjectStatus
{
    public Guid StatusId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
