/* Лабораторная работа №2, вариант 12. БД AirLogger (MS SQL Server 2019+).
   ВНИМАНИЕ: ER-диаграммы нет, поэтому набор полей сущностей выбран разумно
   (см. README_Lab2.md). Если у вас есть ER_AirLogger.pdf - сверьте поля. */
USE master;
GO
IF DB_ID(N'AirLogger') IS NOT NULL
BEGIN
    ALTER DATABASE AirLogger SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE AirLogger;
END
GO
CREATE DATABASE AirLogger;
GO
USE AirLogger;
GO

CREATE TABLE dbo.Professor (
    ProfessorId INT IDENTITY(1,1) CONSTRAINT PK_Professor PRIMARY KEY,
    LastName    NVARCHAR(50)  NOT NULL,
    FirstName   NVARCHAR(50)  NOT NULL,
    MiddleName  NVARCHAR(50)  NULL,
    Department  NVARCHAR(100) NOT NULL,
    Position    NVARCHAR(60)  NOT NULL,
    Email       NVARCHAR(100) NOT NULL CONSTRAINT UQ_Professor_Email UNIQUE
);

CREATE TABLE dbo.Student (
    StudentId INT IDENTITY(1,1) CONSTRAINT PK_Student PRIMARY KEY,
    LastName  NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    GroupName NVARCHAR(20) NOT NULL,
    Email     NVARCHAR(100) NOT NULL CONSTRAINT UQ_Student_Email UNIQUE
);

CREATE TABLE dbo.Project (
    ProjectId   INT IDENTITY(1,1) CONSTRAINT PK_Project PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(400) NULL,
    StartDate   DATE NOT NULL,
    ProfessorId INT NOT NULL
        CONSTRAINT FK_Project_Professor REFERENCES dbo.Professor(ProfessorId)
);

CREATE TABLE dbo.Task (
    TaskId    INT IDENTITY(1,1) CONSTRAINT PK_Task PRIMARY KEY,
    ProjectId INT NOT NULL
        CONSTRAINT FK_Task_Project REFERENCES dbo.Project(ProjectId),
    TaskTitle NVARCHAR(150) NOT NULL,
    Deadline  DATE NOT NULL
);

CREATE TABLE dbo.UserException (
    ExceptionId      INT IDENTITY(1,1) CONSTRAINT PK_UserException PRIMARY KEY,
    ProjectId        INT NOT NULL
        CONSTRAINT FK_UserException_Project REFERENCES dbo.Project(ProjectId),
    StudentId        INT NULL
        CONSTRAINT FK_UserException_Student REFERENCES dbo.Student(StudentId) ON DELETE SET NULL,
    ExceptionType    NVARCHAR(100) NOT NULL,
    ExceptionMessage NVARCHAR(400) NOT NULL,
    StackTrace       NVARCHAR(MAX) NULL,
    FormName         NVARCHAR(50)  NULL,
    Severity         NVARCHAR(10)  NOT NULL
        CONSTRAINT CK_UserException_Severity CHECK (Severity IN (N'INFO', N'WARNING', N'ERROR', N'FATAL')),
    OccurredAt       DATETIME2(0)  NOT NULL CONSTRAINT DF_UserException_OccurredAt DEFAULT SYSDATETIME()
);

CREATE TABLE dbo.ReportLabWork (
    ReportId    INT IDENTITY(1,1) CONSTRAINT PK_ReportLabWork PRIMARY KEY,
    StudentId   INT NOT NULL
        CONSTRAINT FK_Report_Student REFERENCES dbo.Student(StudentId),
    ProfessorId INT NOT NULL
        CONSTRAINT FK_Report_Professor REFERENCES dbo.Professor(ProfessorId),
    TaskId      INT NOT NULL
        CONSTRAINT FK_Report_Task REFERENCES dbo.Task(TaskId),
    Title       NVARCHAR(150) NOT NULL,
    SubmitDate  DATE NOT NULL,
    Grade       INT NULL CONSTRAINT CK_Report_Grade CHECK (Grade BETWEEN 0 AND 100),
    Status      NVARCHAR(20) NOT NULL
        CONSTRAINT CK_Report_Status CHECK (Status IN (N'Сдана', N'На проверке', N'Принята', N'Отклонена')),
    Comment     NVARCHAR(400) NULL
);
GO
