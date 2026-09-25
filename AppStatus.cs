using System;

namespace labs_prog
{
    /// <summary>
    /// Шина событий для строки статуса на главной форме (требование п.3).
    /// </summary>
    public static class AppStatus
    {
        public static event Action<string, int> Changed;

        public static void Report(string name, int percent)
        {
            Changed?.Invoke(name, percent);
        }

        public static void Complete(string name)
        {
            Changed?.Invoke(name, 100);
        }

        public static void Idle()
        {
            Changed?.Invoke("Ожидание", 0);
        }
    }
}
