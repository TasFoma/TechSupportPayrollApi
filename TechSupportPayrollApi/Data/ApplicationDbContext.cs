using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<CoefficientSetting> CoefficientSettings { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<Break> Breaks { get; set; }
    public DbSet<OperatorStatistic> OperatorStatistics { get; set; }
    public DbSet<SalaryCalculation> SalaryCalculations { get; set; }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<SalaryCalculation>()
            .HasOne(sc => sc.Employee)
            .WithMany()
            .HasForeignKey(sc => sc.EmployeeId);
    }
}