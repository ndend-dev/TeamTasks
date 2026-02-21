namespace TeamTasks.Utils.Dtos
{
    public class DeveloperWorkloadDto
    {
        public string DeveloperName { get; set; } = string.Empty;
        public int OpenTasksCount { get; set; }
        public double AverageEstimatedComplexity { get; set; }
    }
}
