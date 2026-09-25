using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labs_prog
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ── Глобальная обработка необработанных исключений (требование п.1) ──

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => ExceptionLogger.LogException(e.Exception);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    ExceptionLogger.LogException(ex);
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                ExceptionLogger.LogException(e.Exception);
                e.SetObserved();
            };

            Application.Run(new Form1());
        }
    }
}
