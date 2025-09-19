BEGIN TRANSACTION;

-- Таблица миграций (EF Core)
CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
    MigrationId TEXT PRIMARY KEY,
    ProductVersion TEXT NOT NULL
);

-- Таблица сотрудников
CREATE TABLE IF NOT EXISTS Employees (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    LastName TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    MiddleName TEXT,
    Position TEXT NOT NULL,
    Status TEXT NOT NULL
);

-- Таблица настроек коэффициентов
CREATE TABLE IF NOT EXISTS CoefficientSettings (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    MinValue REAL NOT NULL,
    MaxValue REAL NOT NULL,
    Weight REAL NOT NULL,
    ImpactType TEXT NOT NULL
);

-- Таблица рабочих смен
CREATE TABLE IF NOT EXISTS WorkShifts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    EmployeeId INTEGER NOT NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employees (Id)
);

-- Вставляем данные миграций
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES 
('20250914043312_InitialCreate', '9.0.9'),
('20250916053029_AddCoefficientSettings', '9.0.9'),
('20250916101731_AddSalaryCalculation', '9.0.9');

-- Вставляем сотрудников
INSERT INTO Employees (Id, LastName, FirstName, MiddleName, Position, Status) VALUES 
(2, 'Петров', 'Алексей', 'Александрович', 'Оператор', 'активен'),
(3, 'Фомина', 'Анастасия', 'Константиновна', 'оператор', 'активный'),
(4, 'Иванов', 'Иван', NULL, 'оператор', 'нанятый');

-- Вставляем настройки коэффициентов
INSERT INTO CoefficientSettings (Id, Name, MinValue, MaxValue, Weight, ImpactType) VALUES 
(1, 'Время первого ответа (сек)', 60.0, 120.0, 0.2, 'положительный'),
(3, 'Время последующих ответов (сек)', 120.0, 240.0, 0.15, 'положительный'),
(4, 'Количество ошибок (в день)', 0.0, 3.0, 0.15, 'отрицательный');

-- Вставляем рабочие смены
INSERT INTO WorkShifts (Id, StartTime, EndTime, EmployeeId) VALUES 
(1, '2025-09-16 06:26:15.3459952', '2025-09-16 06:26:35.4134661', 3),
(2, '2025-09-16 08:47:06.3206255', '2025-09-16 08:47:36.650123', 2),
(3, '2025-09-16 09:03:48.9800796', '2025-09-16 09:03:56.9449075', 3),
(4, '2025-09-16 09:40:01.2959919', '2025-09-16 09:40:14.6444275', 4);

COMMIT;