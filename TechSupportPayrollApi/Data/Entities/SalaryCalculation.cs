using TechSupportPayrollApi.Data.Entities;

public class SalaryCalculation
{
    public int Id { get; set; }
    public DateTime Period { get; set; }
    public DateTime CalculationDate { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public string Parameter { get; set; }
    public double Value { get; set; }
    public double Result { get; set; }

    // Добавьте это свойство
    public double FinalResult { get; set; }
}