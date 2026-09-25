using System;
using System.Windows.Forms;

namespace labs_prog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeExceptionLogging();
            InitializeStatusBar();
            InitializeMainMenu();
        }

        /// <summary>Требование п.1: обработанные исключения фиксируются на главной
        /// форме в TextBox и в текстовом файле с датой/временем, сообщением и стеком.</summary>
        private void InitializeExceptionLogging()
        {
            ExceptionLogger.Initialize(txtGlobalExceptions, "exceptions_log.txt");
        }

        /// <summary>Требование п.3: строка статуса с индикатором процесса,
        /// его наименованием и текущими системными датой/временем.</summary>
        private void InitializeStatusBar()
        {
            AppStatus.Changed += OnAppStatusChanged;

            timerClock.Interval = 1000;
            timerClock.Tick += (s, e) =>
                statusDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            timerClock.Start();
            statusDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            statusProcessName.Text = "Ожидание";
            statusProgress.Style = ProgressBarStyle.Continuous;
            statusProgress.Value = 0;
        }

        private void OnAppStatusChanged(string processName, int percent)
        {
            void Update()
            {
                statusProcessName.Text = processName;
                if (percent < 0)
                {
                    statusProgress.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    statusProgress.Style = ProgressBarStyle.Continuous;
                    statusProgress.Value = Math.Max(0, Math.Min(100, percent));
                }
            }

            if (IsDisposed) return;
            if (InvokeRequired) BeginInvoke((Action)Update);
            else Update();
        }

        /// <summary>Требование п.3: навигация по формам лабораторных работ
        /// выполняется через главное меню (MenuStrip), а не только через вкладки.</summary>
        private void InitializeMainMenu()
        {
            menuLab1.Click += (s, e) => tabControl.SelectedTab = tabLab1;
            menuLab2.Click += (s, e) => tabControl.SelectedTab = tabLab2;
            menuLab3.Click += (s, e) => tabControl.SelectedTab = tabLab3;
            menuLab4.Click += (s, e) => tabControl.SelectedTab = tabLab4;
            menuLab5.Click += (s, e) => tabControl.SelectedTab = tabLab5;

            menuExit.Click += (s, e) => Close();
        }
    }
}
