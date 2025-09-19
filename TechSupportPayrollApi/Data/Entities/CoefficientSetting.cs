namespace TechSupportPayrollApi.Data.Entities
{
    public class CoefficientSetting
    {
        public int Id { get; set; }
        public string ParameterName { get; set; } = string.Empty;
        public double Norm { get; set; }
        public double Base { get; set; }
        public double Weight { get; set; }
        public string CoefficientType { get; set; } = string.Empty;
    }
}