namespace TechSupportPayrollApi.Data.Entities
{
    public class WorkShift
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // Добавляем свойство для связи с перерывами
        public List<Break> Breaks { get; set; } = new List<Break>();
    }
}