namespace TeamTasks.Utils.Dtos
{
    public class UpcomingDeadlinesDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public string TaskTile { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string DeveloperName { get; set; } = string.Empty;
        public int DaysRemaining { get; set; }
    }
}
