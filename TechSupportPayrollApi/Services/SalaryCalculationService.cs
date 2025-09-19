using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Services
{
    public class SalaryCalculationService
    {
        private readonly ApplicationDbContext _context;

        public SalaryCalculationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryCalculationResult> CalculateSalary(int employeeId, DateTime period)
        {
            // Получаем статистику за месяц
            var stats = await _context.OperatorStatistics
                .Where(s => s.EmployeeId == employeeId &&
                           s.Date.Month == period.Month &&
                           s.Date.Year == period.Year)
                .ToListAsync();

            // Получаем настройки коэффициентов
            var coefficients = await _context.CoefficientSettings.ToListAsync();

            var result = new SalaryCalculationResult();
            var parameterResults = new List<ParameterResult>();

            // Группируем статистику по параметрам
            var groupedStats = stats.GroupBy(s => s.ParameterName);

            foreach (var group in groupedStats)
            {
                var parameterName = group.Key;
                var avgValue = group.Average(s => s.Value);

                // Находим настройки для этого параметра
                var coefficient = coefficients.FirstOrDefault(c =>
                    c.ParameterName == parameterName);

                if (coefficient != null)
                {
                    double parameterCoef = CalculateParameterCoefficient(
                        avgValue,
                        coefficient.Base,
                        coefficient.Norm
                    );

                    parameterResults.Add(new ParameterResult
                    {
                        ParameterName = parameterName,
                        AverageValue = avgValue,
                        Coefficient = parameterCoef,
                        Weight = coefficient.Weight
                    });
                }
            }

            // Рассчитываем общий коэффициент
            result.FinalCoefficient = CalculateFinalCoefficient(parameterResults);
            result.CalculationDate = DateTime.Now;

            return result;
        }

        private double CalculateParameterCoefficient(double value, double baseValue, double normValue)
        {
            if (value <= baseValue)
            {
                return Math.Max(0.5, 1 - (baseValue - value) / (baseValue * 2));
            }
            else if (value >= normValue)
            {
                return 2.0;
            }
            else
            {
                return 1 + (value - baseValue) / (normValue - baseValue);
            }
        }

        private string GetCoefficientType(string parameterName)
        {
            switch (parameterName)
            {
                case "FirstResponseTime":
                case "SubsequentResponseTime":
                case "CompetenceRating":
                case "PolitenessRating":
                    return "положительный";
                case "ErrorCount":
                    return "негативный";
                default:
                    return "положительный";
            }
        }

        private double CalculateFinalCoefficient(List<ParameterResult> parameters)
        {
            double positiveSum = 0;
            double negativeSum = 0;
            double positiveWeightSum = 0;
            double negativeWeightSum = 0;

            foreach (var param in parameters)
            {
                var coefficientType = GetCoefficientType(param.ParameterName);

                if (coefficientType == "положительный")
                {
                    positiveSum += param.Coefficient * param.Weight;
                    positiveWeightSum += param.Weight;
                }
                else
                {
                    negativeSum += param.Coefficient * param.Weight;
                    negativeWeightSum += param.Weight;
                }
            }

            double positiveFinal = positiveWeightSum > 0 ? positiveSum / positiveWeightSum : 1;
            double negativeFinal = negativeWeightSum > 0 ? negativeSum / negativeWeightSum : 1;

            return positiveFinal * negativeFinal;
        }
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