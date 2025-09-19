# TechSupportPayrollApi 

Backend API для системы расчета заработной платы операторов технической поддержки.

## 🚀 Технологии

- .NET 8.0
- Entity Framework Core
- SQLite
- Swagger/OpenAPI
- CORS

 ---
 
## 📦 Установка и запуск

- Клонируйте репозиторий: 
git clone https://github.com/TasFoma/TechSupportPayrollApi.git
cd TechSupportPayrollApi
- Восстановите зависимости:
dotnet restore
- Запустите приложение:
dotnet run
Откройте в браузере: https://localhost:5259/swagger

---

## 📊 API Endpoints

- GET /api/Employee - список сотрудников
- GET /api/CoefficientSettings - настройки коэффициентов
- GET /api/Salary - история расчетов зарплаты
- POST /api/Salary/calculate - расчет зарплаты за период

---

## 🎯 Пример запроса

POST /api/Salary/calculate
Content-Type: application/json

{
  "employeeId": 1,
  "period": "2025-09-01"
}

---

## 🗄️ База данных

Автоматически создается SQLite база techsupport.db с тестовыми данными.

---

## 🗄️ Frontend
https://github.com/TasFoma/techsupport-frontend
