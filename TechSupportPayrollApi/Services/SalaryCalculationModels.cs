namespace TechSupportPayrollApi.Services
{
    namespace TechSupportPayrollApi.Services
    {
        public class SalaryCalculationRequest
        {
            public int EmployeeId { get; set; }
            public DateTime Period { get; set; }
        }

        public class SalaryCalculationResult
        {
            public double FinalCoefficient { get; set; }
            public DateTime CalculationDate { get; set; }
            public List<ParameterResult> ParameterResults { get; set; } = new();
        }

        public class ParameterResult
        {
            public string ParameterName { get; set; }
            public double AverageValue { get; set; }
            public double Coefficient { get; set; }
            public double Weight { get; set; }
        }
    }
}
