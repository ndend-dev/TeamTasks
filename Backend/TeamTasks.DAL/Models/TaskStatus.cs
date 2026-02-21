using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeamTasks.DAL.Models;

public partial class TaskStatus
{
    public Guid StatusId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
