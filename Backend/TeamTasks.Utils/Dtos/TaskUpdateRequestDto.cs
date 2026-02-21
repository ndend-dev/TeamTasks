namespace TeamTasks.Utils.Dtos
{
    public class TaskUpdateRequestDto
    {
        public Guid StatusId { get; set; }

        public Guid PriorityId { get; set; }

        public byte EstimatedComplexity { get; set; }

    }
}
