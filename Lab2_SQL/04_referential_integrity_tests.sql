/* Проверка ссылочной целостности. Каждый блок должен завершиться ОШИБКОЙ
   (кроме последнего), это и нужно показать на скриншотах. */
USE AirLogger;
GO

-- 1. Вставка отчёта с несуществующим студентом -> ошибка FK_Report_Student
BEGIN TRY
    INSERT dbo.ReportLabWork (StudentId, ProfessorId, TaskId, Title, SubmitDate, Status)
    VALUES (999, 1, 1, N'Тест', '2026-11-05', N'Сдана');
END TRY
BEGIN CATCH SELECT N'Тест 1' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 2. Вставка исключения с несуществующим проектом -> ошибка FK_UserException_Project
BEGIN TRY
    INSERT dbo.UserException (ProjectId, ExceptionType, ExceptionMessage, Severity)
    VALUES (999, N'Test', N'Test', N'ERROR');
END TRY
BEGIN CATCH SELECT N'Тест 2' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 3. Удаление преподавателя, на которого есть ссылки -> ошибка FK_Project_Professor
BEGIN TRY
    DELETE FROM dbo.Professor WHERE ProfessorId = 1;
END TRY
BEGIN CATCH SELECT N'Тест 3' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 4. Удаление проекта с задачами -> ошибка FK_Task_Project
BEGIN TRY
    DELETE FROM dbo.Project WHERE ProjectId = 1;
END TRY
BEGIN CATCH SELECT N'Тест 4' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 5. Недопустимая оценка / серьёзность -> ошибки CHECK
BEGIN TRY
    INSERT dbo.ReportLabWork (StudentId, ProfessorId, TaskId, Title, SubmitDate, Grade, Status)
    VALUES (1, 1, 1, N'Тест', '2026-11-05', 150, N'Сдана');
END TRY
BEGIN CATCH SELECT N'Тест 5a' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH
BEGIN TRY
    INSERT dbo.UserException (ProjectId, ExceptionType, ExceptionMessage, Severity)
    VALUES (1, N'Test', N'Test', N'BAD');
END TRY
BEGIN CATCH SELECT N'Тест 5b' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 6. Дубликат e-mail -> ошибка UQ_Professor_Email
BEGIN TRY
    INSERT dbo.Professor (LastName, FirstName, Department, Position, Email)
    VALUES (N'Тест', N'Тест', N'Тест', N'Тест', N'ivanov@univ.ru');
END TRY
BEGIN CATCH SELECT N'Тест 6' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg; END CATCH

-- 7. Удаление студента: ON DELETE SET NULL в UserException (ошибки НЕТ, StudentId станет NULL),
--    но у студента 1 есть отчёты -> удаление будет заблокировано. Берём студента 4 без исключений:
BEGIN TRY
    BEGIN TRAN;
    DELETE FROM dbo.Student WHERE StudentId = 2;   -- есть отчёт -> ошибка FK_Report_Student
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    SELECT N'Тест 7' AS Test, ERROR_NUMBER() AS Err, ERROR_MESSAGE() AS Msg;
END CATCH

-- 8. Корректная вставка - ошибки нет
INSERT dbo.UserException (ProjectId, StudentId, ExceptionType, ExceptionMessage, Severity)
VALUES (1, 1, N'System.Exception', N'Корректная запись', N'INFO');
SELECT TOP 1 * FROM dbo.UserException ORDER BY ExceptionId DESC;
GO
