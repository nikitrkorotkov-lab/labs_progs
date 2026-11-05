USE AirLogger;
GO
INSERT dbo.Professor (LastName, FirstName, MiddleName, Department, Position, Email) VALUES
(N'Иванов',  N'Пётр',   N'Сергеевич',   N'Информатика',        N'Доцент',    N'ivanov@univ.ru'),
(N'Петрова', N'Анна',   N'Викторовна',  N'Программирование',   N'Профессор', N'petrova@univ.ru'),
(N'Сидоров', N'Олег',   N'Николаевич',  N'Информатика',        N'Старший преподаватель', N'sidorov@univ.ru'),
(N'Кузнецова', N'Мария', NULL,          N'Базы данных',        N'Доцент',    N'kuznetsova@univ.ru');

INSERT dbo.Student (LastName, FirstName, GroupName, Email) VALUES
(N'Коротков', N'Никита',  N'3437к', N'korotkov@student.ru'),
(N'Смирнов',  N'Алексей', N'3437к', N'smirnov@student.ru'),
(N'Попова',   N'Елена',   N'3438к', N'popova@student.ru'),
(N'Орлов',    N'Дмитрий', N'3438к', N'orlov@student.ru');

INSERT dbo.Project (ProjectName, Description, StartDate, ProfessorId) VALUES
(N'AirLogger',  N'Распределённая система журналирования исключений', '2026-09-01', 1),
(N'LogParser',  N'Парсинг лог-журналов регулярными выражениями',     '2026-09-15', 2);

INSERT dbo.Task (ProjectId, TaskTitle, Deadline) VALUES
(1, N'Лабораторная работа №2: БД и SQL-запросы', '2026-11-20'),
(1, N'Лабораторная работа №1: обработка массивов', '2026-10-10'),
(2, N'Лабораторная работа №4: DLL и парсинг',     '2026-12-05');

INSERT dbo.UserException (ProjectId, StudentId, ExceptionType, ExceptionMessage, StackTrace, FormName, Severity, OccurredAt) VALUES
(1, 1, N'System.NullReferenceException', N'Object reference not set to an instance of an object.', N'at Form1.btnSubmit_Click()', N'Form1', N'ERROR',   '2026-10-01 10:30:15'),
(1, 2, N'System.IndexOutOfRangeException', N'Index was outside the bounds of the array.', N'at Lab2Service.Process()', N'Form1', N'ERROR',   '2026-10-03 14:22:33'),
(1, 3, N'System.FormatException', N'Input string was not in a correct format.', N'at Form2.Parse()', N'Form2', N'WARNING', '2026-10-05 09:05:00'),
(2, 1, N'System.IO.IOException', N'The process cannot access the file.', N'at ExceptionLogger.AppendToFile()', NULL, N'FATAL', '2026-10-07 18:40:10'),
(2, NULL, N'System.DivideByZeroException', N'Attempted to divide by zero.', N'at Calc.Divide()', N'Form3', N'INFO', '2026-10-09 11:11:11');

INSERT dbo.ReportLabWork (StudentId, ProfessorId, TaskId, Title, SubmitDate, Grade, Status, Comment) VALUES
(1, 1, 2, N'Отчёт по ЛР1',  '2026-10-08', 90,   N'Принята',     N'Хорошая работа'),
(2, 1, 2, N'Отчёт по ЛР1',  '2026-10-09', 75,   N'Принята',     NULL),
(3, 2, 3, N'Отчёт по ЛР4',  '2026-11-01', NULL, N'На проверке', NULL),
(1, 1, 1, N'Отчёт по ЛР2',  '2026-11-05', NULL, N'Сдана',       N'Ожидает проверки'),
(4, 2, 3, N'Отчёт по ЛР4',  '2026-11-02', 40,   N'Отклонена',   N'Нет скриншотов');
GO
