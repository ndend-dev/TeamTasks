namespace TeamTasks.Utils.Dtos
{
    public class ProjectStatusSumamryDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public int TotalTask { get; set; }
        public int OpenTask { get; set; }
        public int CompleteTask { get; set; }
    }
}
