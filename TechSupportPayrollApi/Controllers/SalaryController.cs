using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;
using TechSupportPayrollApi.Services;

namespace TechSupportPayrollApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly SalaryCalculationService _salaryService;
        private readonly ApplicationDbContext _context;

        public SalaryController(SalaryCalculationService salaryService, ApplicationDbContext context)
        {
            _salaryService = salaryService;
            _context = context;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateSalary([FromBody] SalaryCalculationRequest request)
        {
            try
            {
                var result = await _salaryService.CalculateSalary(request.EmployeeId, request.Period);

                // Сохраняем результат в БД
                var calculation = new SalaryCalculation
                {
                    EmployeeId = request.EmployeeId,
                    Period = request.Period,
                    CalculationDate = DateTime.Now,
                    FinalResult = result.FinalCoefficient,
                    Parameter = "Общий коэффициент",
                    Value = 1.0
                };

                _context.SalaryCalculations.Add(calculation);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    result = result.FinalCoefficient,
                    calculationId = calculation.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCalculations()
        {
            var calculations = await _context.SalaryCalculations
                .Include(c => c.Employee)
                .OrderByDescending(c => c.CalculationDate)
                .ToListAsync();

            return Ok(calculations);
        }
    }
     
    public class SalaryCalculationRequest
    {
        public int EmployeeId { get; set; }
        public DateTime Period { get; set; }
    }
}