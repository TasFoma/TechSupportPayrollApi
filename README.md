# TechSupportPayrollApi 

Backend API для системы расчета заработной платы операторов технической поддержки.

## 🚀 Технологии

- .NET 8.0
- Entity Framework Core
- SQLite
- Swagger/OpenAPI
- CORS

## 📦 Установка и запуск

1. Клонируйте репозиторий:
```bash
git clone https://github.com/ваш-логин/TechSupportPayrollApi.git
cd TechSupportPayrollApi
Восстановите зависимости:

bash
dotnet restore
Запустите приложение:

bash
dotnet run
Откройте в браузере: https://localhost:5259/swagger

📊 API Endpoints
GET /api/Employee - список сотрудников

GET /api/CoefficientSettings - настройки коэффициентов

GET /api/Salary - история расчетов зарплаты

POST /api/Salary/calculate - расчет зарплаты за период

🎯 Пример запроса
bash
POST /api/Salary/calculate
Content-Type: application/json

{
  "employeeId": 1,
  "period": "2025-09-01"
}
🗄️ База данных
Автоматически создается SQLite база techsupport.db с тестовыми данными.
