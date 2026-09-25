using System;
using System.IO;
using System.Windows.Forms;

namespace labs_prog
{
    /// <summary>
    /// Глобальный логгер исключений (требование задания п.1).
    /// </summary>
    public static class ExceptionLogger
    {
        private static TextBox _logBox;
        private static string _logFilePath = "exceptions_log.txt";
        private static readonly object _fileLock = new object();

        public static void Initialize(TextBox logBox, string logFilePath = null)
        {
            _logBox = logBox;
            if (!string.IsNullOrEmpty(logFilePath))
                _logFilePath = logFilePath;
        }

        public static void LogException(Exception ex)
        {
            if (ex == null) return;

            string entry = string.Format(
                "[{0:dd.MM.yyyy HH:mm:ss}] {1}: {2}\r\nСтек вызовов:\r\n{3}\r\n{4}\r\n",
                DateTime.Now, ex.GetType().Name, ex.Message, ex.StackTrace,
                new string('-', 70));

            AppendToTextBox(entry);
            AppendToFile(entry);
        }

        private static void AppendToTextBox(string entry)
        {
            if (_logBox == null || _logBox.IsDisposed) return;

            void Append() => _logBox.AppendText(entry + Environment.NewLine);

            try
            {
                if (_logBox.InvokeRequired)
                    _logBox.Invoke((Action)Append);
                else
                    Append();
            }
            catch
            {
            }
        }

        private static void AppendToFile(string entry)
        {
            lock (_fileLock)
            {
                try
                {
                    File.AppendAllText(_logFilePath, entry);
                }
                catch
                {
                }
            }
        }
    }
}
