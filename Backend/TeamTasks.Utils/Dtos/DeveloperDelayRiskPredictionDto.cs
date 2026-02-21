namespace TeamTasks.Utils.Dtos
{
    public class DeveloperDelayRiskPredictionDto
    {
        public string DeveloperName { get; set; } = string.Empty;
        public int OpenTasksCount { get; set; }
        public double AvgDelayDays { get; set; }
        public  DateTime? NearestDueDate { get; set; }
        public  DateTime? LatestDueDate { get; set; }
        public DateTime? PredictedCompletionDate { get; set; }
        public int HighRiskFlag { get; set; }
    }
}
