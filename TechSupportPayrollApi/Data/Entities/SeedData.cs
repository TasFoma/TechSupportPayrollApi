using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Data.Entities
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Очищаем базу
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Добавляем сотрудников
            var employees = new[]
            {
                new Employee { LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Position = "оператор", Status = "активен" },
                new Employee { LastName = "Петрова", FirstName = "Мария", MiddleName = "Сергеевна", Position = "старший оператор", Status = "активен" }
            };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            // Добавляем коэффициенты 
            var coefficients = new[]
            {
    new CoefficientSetting { ParameterName = "время_первого_ответа", Norm = 60, Base = 120, Weight = 0.05, CoefficientType = "негативный" },
    new CoefficientSetting { ParameterName = "время_последующих_ответов", Norm = 30, Base = 60, Weight = 0.05, CoefficientType = "негативный" }, // ← ИСПРАВЛЕНО!
    new CoefficientSetting { ParameterName = "оценка_компетентности", Norm = 5, Base = 3, Weight = 0.07, CoefficientType = "положительный" },
    new CoefficientSetting { ParameterName = "оценка_вежливости", Norm = 5, Base = 3, Weight = 0.08, CoefficientType = "положительный" },
    new CoefficientSetting { ParameterName = "количество_ошибок", Norm = 0, Base = 3, Weight = 0.03, CoefficientType = "негативный" }
};
            context.CoefficientSettings.AddRange(coefficients);
            context.SaveChanges();

            // Добавляем смены для сотрудников
            var shifts = new[]
            {
                // Смены для Иванова (ID: 1)
                new WorkShift
                {
                    EmployeeId = 1,
                    StartDate = new DateTime(2025, 9, 1, 9, 0, 0),
                    EndDate = new DateTime(2025, 9, 1, 17, 0, 0)
                },
                new WorkShift
                {
                    EmployeeId = 1,
                    StartDate = new DateTime(2025, 9, 2, 9, 0, 0),
                    EndDate = new DateTime(2025, 9, 2, 17, 0, 0)
                },
                new WorkShift
                {
                    EmployeeId = 1,
                    StartDate = new DateTime(2025, 9, 3, 9, 0, 0),
                    EndDate = new DateTime(2025, 9, 3, 17, 0, 0)
                },

                // Смены для Петровой (ID: 2)
                new WorkShift
                {
                    EmployeeId = 2,
                    StartDate = new DateTime(2025, 9, 1, 10, 0, 0),
                    EndDate = new DateTime(2025, 9, 1, 18, 0, 0)
                },
                new WorkShift
                {
                    EmployeeId = 2,
                    StartDate = new DateTime(2025, 9, 2, 10, 0, 0),
                    EndDate = new DateTime(2025, 9, 2, 18, 0, 0)
                },
                new WorkShift
                {
                    EmployeeId = 2,
                    StartDate = new DateTime(2025, 9, 3, 10, 0, 0),
                    EndDate = new DateTime(2025, 9, 3, 18, 0, 0)
                }
            };
            context.WorkShifts.AddRange(shifts);
            context.SaveChanges();

            // Добавляем статистику для операторов
            var statistics = new[]
            {
                // Статистика для Иванова (ID: 1)
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 1,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "время_первого_ответа",
                    Value = 45.5
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 1,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "время_последующих_ответов",
                    Value = 22.3
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 1,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "оценка_компетентности",
                    Value = 4.8
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 1,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "оценка_вежливости",
                    Value = 4.9
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 1,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "количество_ошибок",
                    Value = 1
                },

                // Статистика для Иванова за второй день
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 2,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "время_первого_ответа",
                    Value = 38.2
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 2,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "время_последующих_ответов",
                    Value = 25.1
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 2,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "оценка_компетентности",
                    Value = 4.9
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 2,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "оценка_вежливости",
                    Value = 4.7
                },
                new OperatorStatistic
                {
                    EmployeeId = 1,
                    ShiftId = 2,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "количество_ошибок",
                    Value = 0
                },

                // Статистика для Петровой (ID: 2)
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 4,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "время_первого_ответа",
                    Value = 52.1
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 4,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "время_последующих_ответов",
                    Value = 28.7
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 4,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "оценка_компетентности",
                    Value = 4.6
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 4,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "оценка_вежливости",
                    Value = 4.8
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 4,
                    Date = new DateTime(2025, 9, 1),
                    ParameterName = "количество_ошибок",
                    Value = 2
                },

                // Статистика для Петровой за второй день
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 5,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "время_первого_ответа",
                    Value = 47.8
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 5,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "время_последующих_ответов",
                    Value = 26.4
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 5,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "оценка_компетентности",
                    Value = 4.7
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 5,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "оценка_вежливости",
                    Value = 4.9
                },
                new OperatorStatistic
                {
                    EmployeeId = 2,
                    ShiftId = 5,
                    Date = new DateTime(2025, 9, 2),
                    ParameterName = "количество_ошибок",
                    Value = 1
                }
            };
            context.OperatorStatistics.AddRange(statistics);
            context.SaveChanges();

            Console.WriteLine("Тестовые данные добавлены! Включая смены и статистику.");
        }
    }
}