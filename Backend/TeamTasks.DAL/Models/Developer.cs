using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeamTasks.DAL.Models;

public partial class Developer
{
    public Guid DeveloperId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
