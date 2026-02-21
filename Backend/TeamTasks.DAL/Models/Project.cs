using System;
using System.Collections.Generic;

namespace TeamTasks.DAL.Models;

public partial class Project
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid StatudId { get; set; }

    public virtual ProjectStatus Statud { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
