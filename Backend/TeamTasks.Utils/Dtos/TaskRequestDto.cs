namespace TeamTasks.Utils.Dtos
{
    public class TaskRequestDto
    {
        public Guid ProjectId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public Guid AssignedId { get; set; }

        public Guid PriorityId { get; set; }

        public byte EstimatedComplexity { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}
