using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using labs_prog.Services.AirLogger;

namespace labs_prog.Views
{
    /// <summary>Вкладка «БД AirLogger» (ЛР2, вариант 12): поиск Professor, сортировка UserException,
    /// фильтрация ReportLabWork. Интерфейс строится в коде (без Designer-файла).</summary>
    public class AirLoggerDbControl : UserControl
    {
        private AirLoggerRepository _repo;
        private int _generated;

        private readonly TextBox txtServer = new TextBox { Text = @".\SQLEXPRESS", Width = 200 };
        private readonly Button btnConnect = new Button { Text = "Подключиться", AutoSize = true };
        private readonly Button btnDeploy = new Button { Text = "Создать / пересоздать БД", AutoSize = true };
        private readonly Label lblState = new Label
        {
            AutoSize = true, Text = "Нет подключения", ForeColor = Color.Firebrick, Margin = new Padding(10, 6, 0, 0)
        };
        private readonly TabControl tabs = new TabControl { Dock = DockStyle.Fill };

        // поиск
        private readonly TextBox txtSearch = new TextBox { Width = 300 };
        private readonly Button btnSearch = new Button { Text = "Найти", AutoSize = true };
        private readonly DataGridView gridSearch = NewGrid();

        // сортировка
        private readonly ComboBox cmbSortField = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 180 };
        private readonly ComboBox cmbSortDir = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 170 };
        private readonly Button btnSort = new Button { Text = "Отсортировать", AutoSize = true };
        private readonly Button btnGenerate = new Button { Text = "Сгенерировать исключение (запись в БД)", AutoSize = true };
        private readonly DataGridView gridSort = NewGrid();

        // фильтр
        private readonly TextBox fReportId = new TextBox(), fStudentId = new TextBox(), fProfessorId = new TextBox(), fTaskId = new TextBox();
        private readonly TextBox fTitle = new TextBox(), fComment = new TextBox(), fGradeFrom = new TextBox(), fGradeTo = new TextBox();
        private readonly DateTimePicker fDateFrom = NewDate(), fDateTo = NewDate();
        private readonly ComboBox fStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly Button btnFilter = new Button { Text = "Применить фильтр", AutoSize = true };
        private readonly Button btnReset = new Button { Text = "Сбросить", AutoSize = true };
        private readonly DataGridView gridFilter = NewGrid();

        public AirLoggerDbControl()
        {
            BuildUi();
        }

        private static DataGridView NewGrid() => new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
            BackgroundColor = SystemColors.Window
        };

        private static DateTimePicker NewDate() => new DateTimePicker
        {
            Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false
        };

        private static Control Field(string caption, Control c, int width)
        {
            var p = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Margin = new Padding(4)
            };
            p.Controls.Add(new Label { Text = caption, AutoSize = true });
            c.Width = width;
            p.Controls.Add(c);
            return p;
        }

        private void BuildUi()
        {
            // верхняя панель подключения
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(4) };
            top.Controls.Add(new Label { Text = "Сервер SQL:", AutoSize = true, Margin = new Padding(3, 6, 3, 0) });
            top.Controls.Add(txtServer);
            top.Controls.Add(btnConnect);
            top.Controls.Add(btnDeploy);
            top.Controls.Add(lblState);

            // вкладка поиска
            var pageSearch = new TabPage("Поиск: Professor");
            var pSearch = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(4) };
            pSearch.Controls.Add(new Label { Text = "Поиск по всем полям:", AutoSize = true, Margin = new Padding(3, 6, 3, 0) });
            pSearch.Controls.Add(txtSearch);
            pSearch.Controls.Add(btnSearch);
            pageSearch.Controls.Add(gridSearch);
            pageSearch.Controls.Add(pSearch);

            // вкладка сортировки
            var pageSort = new TabPage("Сортировка: UserException");
            cmbSortField.Items.AddRange(AirLoggerRepository.SortFields);
            cmbSortField.SelectedIndex = 0;
            cmbSortDir.Items.AddRange(new object[] { "ASC — по возрастанию", "DESC — по убыванию" });
            cmbSortDir.SelectedIndex = 0;
            var pSort = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(4) };
            pSort.Controls.Add(new Label { Text = "Поле:", AutoSize = true, Margin = new Padding(3, 6, 3, 0) });
            pSort.Controls.Add(cmbSortField);
            pSort.Controls.Add(cmbSortDir);
            pSort.Controls.Add(btnSort);
            pSort.Controls.Add(btnGenerate);
            pageSort.Controls.Add(gridSort);
            pageSort.Controls.Add(pSort);

            // вкладка фильтрации
            var pageFilter = new TabPage("Фильтр: ReportLabWork");
            fStatus.Items.AddRange(new object[] { "", "Сдана", "На проверке", "Принята", "Отклонена" });
            fStatus.SelectedIndex = 0;
            var pFilter = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(4) };
            pFilter.Controls.Add(Field("ReportId", fReportId, 70));
            pFilter.Controls.Add(Field("StudentId", fStudentId, 70));
            pFilter.Controls.Add(Field("ProfessorId", fProfessorId, 70));
            pFilter.Controls.Add(Field("TaskId", fTaskId, 70));
            pFilter.Controls.Add(Field("Title (подстрока)", fTitle, 150));
            pFilter.Controls.Add(Field("SubmitDate от", fDateFrom, 120));
            pFilter.Controls.Add(Field("SubmitDate до", fDateTo, 120));
            pFilter.Controls.Add(Field("Grade от", fGradeFrom, 60));
            pFilter.Controls.Add(Field("Grade до", fGradeTo, 60));
            pFilter.Controls.Add(Field("Status", fStatus, 120));
            pFilter.Controls.Add(Field("Comment (подстрока)", fComment, 150));
            var pBtn = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(4, 18, 4, 4) };
            pBtn.Controls.Add(btnFilter);
            pBtn.Controls.Add(btnReset);
            pFilter.Controls.Add(pBtn);
            pageFilter.Controls.Add(gridFilter);
            pageFilter.Controls.Add(pFilter);

            tabs.TabPages.Add(pageSearch);
            tabs.TabPages.Add(pageSort);
            tabs.TabPages.Add(pageFilter);
            Controls.Add(tabs);
            Controls.Add(top);

            btnConnect.Click += (s, e) => Connect();
            btnDeploy.Click += (s, e) => Deploy();
            btnSearch.Click += (s, e) => RunSearch();
            txtSearch.TextChanged += (s, e) => RunSearch();
            btnSort.Click += (s, e) => RunSort();
            cmbSortField.SelectedIndexChanged += (s, e) => RunSort();
            cmbSortDir.SelectedIndexChanged += (s, e) => RunSort();
            btnGenerate.Click += (s, e) => GenerateException();
            btnFilter.Click += (s, e) => RunFilter();
            btnReset.Click += (s, e) => { ResetFilter(); RunFilter(); };
        }

        private void SetState(bool ok, string text)
        {
            lblState.ForeColor = ok ? Color.ForestGreen : Color.Firebrick;
            lblState.Text = text;
        }

        private bool Ensure()
        {
            if (_repo != null) return true;
            SetState(false, "Сначала нажмите «Подключиться»");
            return false;
        }

        private void Connect()
        {
            try
            {
                var repo = new AirLoggerRepository(txtServer.Text);
                repo.TestConnection();
                _repo = repo;
                ExceptionLogger.DbWriter = ex => _repo?.InsertException(ex, "Form1");
                SetState(true, "Подключено: " + txtServer.Text.Trim() + " / AirLogger");
                RunSearch();
                RunSort();
                RunFilter();
            }
            catch (Exception ex)
            {
                _repo = null;
                ExceptionLogger.DbWriter = null;
                SetState(false, "Ошибка подключения (если БД нет — нажмите «Создать / пересоздать БД»): " + ex.Message);
                ExceptionLogger.LogException(ex);
            }
        }

        private void Deploy()
        {
            var answer = MessageBox.Show(
                "База AirLogger будет удалена и создана заново (таблицы, тестовые данные, процедуры). Продолжить?",
                "Создание БД AirLogger", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var repo = new AirLoggerRepository(txtServer.Text);
                repo.Deploy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab2_SQL"));
                Connect();
            }
            catch (Exception ex)
            {
                SetState(false, "Ошибка создания БД: " + ex.Message);
                ExceptionLogger.LogException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void Show(DataGridView grid, Func<DataTable> query)
        {
            if (!Ensure()) return;
            try
            {
                grid.DataSource = query();
            }
            catch (Exception ex)
            {
                SetState(false, "Ошибка запроса: " + ex.Message);
                ExceptionLogger.LogException(ex);
            }
        }

        private void RunSearch() => Show(gridSearch, () => _repo.SearchProfessors(txtSearch.Text));

        private void RunSort() => Show(gridSort, () =>
            _repo.SortUserExceptions(cmbSortField.SelectedItem.ToString(), cmbSortDir.SelectedIndex == 1));

        private void RunFilter()
        {
            if (!Ensure()) return;
            ReportFilter f;
            try
            {
                f = new ReportFilter
                {
                    ReportId = ParseInt(fReportId, "ReportId"),
                    StudentId = ParseInt(fStudentId, "StudentId"),
                    ProfessorId = ParseInt(fProfessorId, "ProfessorId"),
                    TaskId = ParseInt(fTaskId, "TaskId"),
                    GradeFrom = ParseInt(fGradeFrom, "Grade от"),
                    GradeTo = ParseInt(fGradeTo, "Grade до"),
                    Title = fTitle.Text,
                    Comment = fComment.Text,
                    Status = fStatus.SelectedItem?.ToString(),
                    SubmitDateFrom = fDateFrom.Checked ? (DateTime?)fDateFrom.Value.Date : null,
                    SubmitDateTo = fDateTo.Checked ? (DateTime?)fDateTo.Value.Date : null
                };
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ExceptionLogger.LogException(ex);
                return;
            }
            Show(gridFilter, () => _repo.FilterReports(f));
        }

        private static int? ParseInt(TextBox box, string name)
        {
            string s = box.Text.Trim();
            if (s.Length == 0) return null;
            if (!int.TryParse(s, out int v))
                throw new FormatException("Поле «" + name + "» должно быть целым числом.");
            return v;
        }

        private void ResetFilter()
        {
            foreach (var t in new[] { fReportId, fStudentId, fProfessorId, fTaskId, fTitle, fComment, fGradeFrom, fGradeTo })
                t.Clear();
            fDateFrom.Checked = false;
            fDateTo.Checked = false;
            fStatus.SelectedIndex = 0;
        }

        /// <summary>Создает реальное исключение при работе с массивом/числами, обрабатывает его
        /// и фиксирует в файле, на форме и в базе данных.</summary>
        private void GenerateException()
        {
            try
            {
                switch (_generated++ % 3)
                {
                    case 0:
                        var arr = new int[3];
                        int idx = int.Parse("5");
                        arr[idx] = 1;
                        break;
                    case 1:
                        int.Parse("abc");
                        break;
                    default:
                        int zero = int.Parse("0");
                        int r = 10 / zero;
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
            }
            if (_repo != null) RunSort();
        }
    }
}
