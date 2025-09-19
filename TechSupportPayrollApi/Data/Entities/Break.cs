namespace TechSupportPayrollApi.Data.Entities
{
    public class Break
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public Employee? Employee { get; set; }
        public WorkShift? Shift { get; set; }
    }
}