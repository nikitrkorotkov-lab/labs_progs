/* Вариант 12:
   1) ПОИСК по всем полям сущности Professor
   2) СОРТИРОВКА по всем полям сущности UserException
   3) ФИЛЬТРАЦИЯ по всем полям сущности ReportLabWork */
USE AirLogger;
GO

/* ===== 1. ПОИСК: Professor (по всем полям, подстрока, без учёта регистра) ===== */
CREATE OR ALTER PROCEDURE dbo.sp_SearchProfessor @Query NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM dbo.Professor
    WHERE CAST(ProfessorId AS NVARCHAR(20)) LIKE N'%' + @Query + N'%'
       OR LastName   LIKE N'%' + @Query + N'%'
       OR FirstName  LIKE N'%' + @Query + N'%'
       OR MiddleName LIKE N'%' + @Query + N'%'
       OR Department LIKE N'%' + @Query + N'%'
       OR Position   LIKE N'%' + @Query + N'%'
       OR Email      LIKE N'%' + @Query + N'%';
END
GO

/* ===== 2. СОРТИРОВКА: UserException (по любому полю, ASC/DESC) ===== */
CREATE OR ALTER PROCEDURE dbo.sp_SortUserException
    @SortField NVARCHAR(50) = N'ExceptionId',
    @Direction NVARCHAR(4)  = N'ASC'
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM sys.columns
                   WHERE object_id = OBJECT_ID(N'dbo.UserException') AND name = @SortField)
    BEGIN RAISERROR(N'Недопустимое поле сортировки: %s', 16, 1, @SortField); RETURN; END
    IF UPPER(@Direction) NOT IN (N'ASC', N'DESC')
    BEGIN RAISERROR(N'Направление должно быть ASC или DESC', 16, 1); RETURN; END

    DECLARE @sql NVARCHAR(MAX) =
        N'SELECT * FROM dbo.UserException ORDER BY ' + QUOTENAME(@SortField) + N' ' + UPPER(@Direction);
    EXEC sp_executesql @sql;
END
GO

/* ===== 3. ФИЛЬТРАЦИЯ: ReportLabWork (по всем полям; NULL = условие не применяется) ===== */
CREATE OR ALTER PROCEDURE dbo.sp_FilterReportLabWork
    @ReportId       INT = NULL,
    @StudentId      INT = NULL,
    @ProfessorId    INT = NULL,
    @TaskId         INT = NULL,
    @Title          NVARCHAR(150) = NULL,   -- подстрока
    @SubmitDateFrom DATE = NULL,
    @SubmitDateTo   DATE = NULL,
    @GradeFrom      INT  = NULL,
    @GradeTo        INT  = NULL,
    @Status         NVARCHAR(20)  = NULL,
    @Comment        NVARCHAR(400) = NULL    -- подстрока
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.*
    FROM dbo.ReportLabWork r
    WHERE (@ReportId    IS NULL OR r.ReportId    = @ReportId)
      AND (@StudentId   IS NULL OR r.StudentId   = @StudentId)
      AND (@ProfessorId IS NULL OR r.ProfessorId = @ProfessorId)
      AND (@TaskId      IS NULL OR r.TaskId      = @TaskId)
      AND (@Title       IS NULL OR r.Title   LIKE N'%' + @Title   + N'%')
      AND (@SubmitDateFrom IS NULL OR r.SubmitDate >= @SubmitDateFrom)
      AND (@SubmitDateTo   IS NULL OR r.SubmitDate <= @SubmitDateTo)
      AND (@GradeFrom IS NULL OR r.Grade >= @GradeFrom)
      AND (@GradeTo   IS NULL OR r.Grade <= @GradeTo)
      AND (@Status  IS NULL OR r.Status = @Status)
      AND (@Comment IS NULL OR r.Comment LIKE N'%' + @Comment + N'%');
END
GO

/* ===== ПРИМЕРЫ ВЫЗОВА (делать скриншоты для отчёта) ===== */
-- Поиск
EXEC dbo.sp_SearchProfessor N'Иванов';
EXEC dbo.sp_SearchProfessor N'Информатика';
EXEC dbo.sp_SearchProfessor N'univ.ru';

-- Сортировка по каждому полю UserException
EXEC dbo.sp_SortUserException N'ExceptionId',      N'DESC';
EXEC dbo.sp_SortUserException N'ProjectId',        N'ASC';
EXEC dbo.sp_SortUserException N'StudentId',        N'ASC';
EXEC dbo.sp_SortUserException N'ExceptionType',    N'ASC';
EXEC dbo.sp_SortUserException N'ExceptionMessage', N'ASC';
EXEC dbo.sp_SortUserException N'StackTrace',       N'ASC';
EXEC dbo.sp_SortUserException N'FormName',         N'ASC';
EXEC dbo.sp_SortUserException N'Severity',         N'DESC';
EXEC dbo.sp_SortUserException N'OccurredAt',       N'DESC';

-- Фильтрация ReportLabWork
EXEC dbo.sp_FilterReportLabWork @StudentId = 1;
EXEC dbo.sp_FilterReportLabWork @ProfessorId = 2, @Status = N'На проверке';
EXEC dbo.sp_FilterReportLabWork @GradeFrom = 70, @GradeTo = 100;
EXEC dbo.sp_FilterReportLabWork @SubmitDateFrom = '2026-10-01', @SubmitDateTo = '2026-10-31';
EXEC dbo.sp_FilterReportLabWork @Title = N'ЛР4', @Comment = N'скриншот';
EXEC dbo.sp_FilterReportLabWork @ReportId = 1, @TaskId = 2;
GO
