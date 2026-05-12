using System;
using System.Windows.Forms;
using labs_prog.Services.Lab1;

namespace labs_prog.Views
{
    public partial class Lab1Control : UserControl
    {
        private Lab1Service _service;

        public Lab1Control()
        {
            InitializeComponent();
            _service = new Lab1Service();
        }

        // Кнопка: Поразрядное сложение
        private void btnBitwiseAdd_Click(object sender, EventArgs e)
        {
            string surname = txtSurname.Text.Trim();
            string name = txtName.Text.Trim();

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите фамилию и имя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtResult.Text = _service.BitwiseAddition(surname, name);
        }

        // Кнопка: Нечетные делители
        private void btnOddDivisors_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumber.Text, out int number))
            {
                txtResult.Text = _service.FindOddDivisorsSum(number);
            }
            else
            {
                MessageBox.Show("Введите корректное число!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Кнопка: Равновеликие прямоугольники
        private void btnRectangles_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtArea.Text, out int area))
            {
                txtResult.Text = _service.FindEqualAreaRectangles(area);
            }
            else
            {
                MessageBox.Show("Введите корректную площадь!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
