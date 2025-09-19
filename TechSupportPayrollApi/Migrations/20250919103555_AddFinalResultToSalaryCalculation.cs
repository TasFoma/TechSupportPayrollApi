using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupportPayrollApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalResultToSalaryCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoefficientSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParameterName = table.Column<string>(type: "TEXT", nullable: false),
                    Norm = table.Column<double>(type: "REAL", nullable: false),
                    Base = table.Column<double>(type: "REAL", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    CoefficientType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoefficientSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Period = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Parameter = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Result = table.Column<double>(type: "REAL", nullable: false),
                    FinalResult = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryCalculations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Breaks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShiftId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Breaks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breaks_WorkShifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "WorkShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperatorStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParameterName = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShiftId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatorStatistics_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperatorStatistics_WorkShifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "WorkShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breaks_EmployeeId",
                table: "Breaks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Breaks_ShiftId",
                table: "Breaks",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorStatistics_EmployeeId",
                table: "OperatorStatistics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorStatistics_ShiftId",
                table: "OperatorStatistics",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryCalculations_EmployeeId",
                table: "SalaryCalculations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_EmployeeId",
                table: "WorkShifts",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breaks");

            migrationBuilder.DropTable(
                name: "CoefficientSettings");

            migrationBuilder.DropTable(
                name: "OperatorStatistics");

            migrationBuilder.DropTable(
                name: "SalaryCalculations");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
