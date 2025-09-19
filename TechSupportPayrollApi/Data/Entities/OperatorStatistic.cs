using TechSupportPayrollApi.Data.Entities;

public class OperatorStatistic
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string ParameterName { get; set; } = string.Empty;
    public double Value { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public int ShiftId { get; set; }  
    public WorkShift Shift { get; set; }  
}