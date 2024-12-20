namespace SW_Parcial_v3.Dtos
{
    public class TaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int ProjectId { get; set; }
    }
}
