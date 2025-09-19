namespace TechSupportPayrollApi.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Position { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}