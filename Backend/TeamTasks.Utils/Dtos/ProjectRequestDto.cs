namespace TeamTasks.Utils.Dtos
{
    public class ProjectRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid StatusId { get; set; }
    }
}
