using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeamTasks.DAL.Models;

public partial class Task
{
    public Guid TaskId { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid AssignedId { get; set; }

    public Guid StatusId { get; set; }

    public Guid PriorityId { get; set; }

    public byte EstimatedComplexity { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Developer Assigned { get; set; } = null!;

    public virtual Priority Priority { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual TaskStatus Status { get; set; } = null!;
}
