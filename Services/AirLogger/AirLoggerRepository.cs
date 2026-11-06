using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace labs_prog.Services.AirLogger
{
    /// <summary>Фильтр по всем полям ReportLabWork (null = условие не применяется).</summary>
    public class ReportFilter
    {
        public int? ReportId, StudentId, ProfessorId, TaskId, GradeFrom, GradeTo;
        public string Title, Status, Comment;
        public DateTime? SubmitDateFrom, SubmitDateTo;
    }

    /// <summary>Доступ к БД AirLogger (MS SQL Server) через хранимые процедуры варианта 12.</summary>
    public class AirLoggerRepository
    {
        public static readonly string[] SortFields =
        {
            "ExceptionId", "ProjectId", "StudentId", "ExceptionType", "ExceptionMessage",
            "StackTrace", "FormName", "Severity", "OccurredAt"
        };

        private readonly string _server;

        public AirLoggerRepository(string server)
        {
            _server = string.IsNullOrWhiteSpace(server) ? @".\SQLEXPRESS" : server.Trim();
        }

        private string Cs(string db) =>
            "Server=" + _server + ";Database=" + db + ";Integrated Security=True;Connect Timeout=5";

        /// <summary>Выбрасывает исключение, если сервер или БД AirLogger недоступны.</summary>
        public void TestConnection()
        {
            using (var c = new SqlConnection(Cs("AirLogger")))
                c.Open();
        }

        /// <summary>Выполняет скрипты 01, 02, 03 из папки Lab2_SQL (ПЕРЕСОЗДАЕТ БД).</summary>
        public void Deploy(string scriptsDir)
        {
            SqlConnection.ClearAllPools();
            foreach (var name in new[] { "01_create_db.sql", "02_seed_data.sql", "03_variant12_queries.sql" })
            {
                string path = Path.Combine(scriptsDir, name);
                if (!File.Exists(path))
                    throw new FileNotFoundException("Не найден SQL-скрипт: " + path);
                RunScript(File.ReadAllText(path, Encoding.UTF8));
            }
        }

        private void RunScript(string script)
        {
            using (var conn = new SqlConnection(Cs("master")))
            {
                conn.Open();
                foreach (string batch in Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(batch)) continue;
                    using (var cmd = new SqlCommand(batch, conn) { CommandTimeout = 60 })
                        cmd.ExecuteNonQuery();
                }
            }
        }

        private static SqlParameter P(string name, SqlDbType type, object value, int size = 0)
        {
            var p = size > 0 ? new SqlParameter(name, type, size) : new SqlParameter(name, type);
            p.Value = value ?? DBNull.Value;
            return p;
        }

        private static string S(string s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        private DataTable Proc(string name, params SqlParameter[] ps)
        {
            var table = new DataTable();
            using (var c = new SqlConnection(Cs("AirLogger")))
            using (var cmd = new SqlCommand(name, c) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddRange(ps);
                using (var a = new SqlDataAdapter(cmd))
                    a.Fill(table);
            }
            return table;
        }

        // 1) Поиск по всем полям Professor
        public DataTable SearchProfessors(string query) =>
            Proc("dbo.sp_SearchProfessor", P("@Query", SqlDbType.NVarChar, (query ?? "").Trim(), 100));

        // 2) Сортировка UserException по любому полю
        public DataTable SortUserExceptions(string field, bool descending) =>
            Proc("dbo.sp_SortUserException",
                 P("@SortField", SqlDbType.NVarChar, field, 50),
                 P("@Direction", SqlDbType.NVarChar, descending ? "DESC" : "ASC", 4));

        // 3) Фильтрация ReportLabWork по всем полям
        public DataTable FilterReports(ReportFilter f) =>
            Proc("dbo.sp_FilterReportLabWork",
                 P("@ReportId", SqlDbType.Int, f.ReportId),
                 P("@StudentId", SqlDbType.Int, f.StudentId),
                 P("@ProfessorId", SqlDbType.Int, f.ProfessorId),
                 P("@TaskId", SqlDbType.Int, f.TaskId),
                 P("@Title", SqlDbType.NVarChar, S(f.Title), 150),
                 P("@SubmitDateFrom", SqlDbType.Date, f.SubmitDateFrom),
                 P("@SubmitDateTo", SqlDbType.Date, f.SubmitDateTo),
                 P("@GradeFrom", SqlDbType.Int, f.GradeFrom),
                 P("@GradeTo", SqlDbType.Int, f.GradeTo),
                 P("@Status", SqlDbType.NVarChar, S(f.Status), 20),
                 P("@Comment", SqlDbType.NVarChar, S(f.Comment), 400));

        /// <summary>Записывает обработанное исключение в таблицу UserException (проект №1 = AirLogger).</summary>
        public void InsertException(Exception ex, string formName, int projectId = 1)
        {
            if (ex == null) return;
            const string sql =
                "INSERT dbo.UserException (ProjectId, StudentId, ExceptionType, ExceptionMessage, StackTrace, FormName, Severity, OccurredAt) " +
                "VALUES (@p, NULL, @t, @m, @s, @f, N'ERROR', SYSDATETIME())";
            string type = ex.GetType().FullName ?? ex.GetType().Name;
            string msg = ex.Message ?? "";
            using (var c = new SqlConnection(Cs("AirLogger")))
            using (var cmd = new SqlCommand(sql, c))
            {
                cmd.Parameters.Add(P("@p", SqlDbType.Int, projectId));
                cmd.Parameters.Add(P("@t", SqlDbType.NVarChar, type.Length > 100 ? type.Substring(0, 100) : type, 100));
                cmd.Parameters.Add(P("@m", SqlDbType.NVarChar, msg.Length > 400 ? msg.Substring(0, 400) : msg, 400));
                cmd.Parameters.Add(P("@s", SqlDbType.NVarChar, ex.StackTrace, -1));
                cmd.Parameters.Add(P("@f", SqlDbType.NVarChar, formName, 50));
                c.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
