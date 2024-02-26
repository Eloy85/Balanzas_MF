using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Balanzas_MF
{
    public partial class FormErrores : Form
    {
        private Form1 parentForm;
        public FormErrores(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

            // Agregar las columnas al DataGridView
            dataGridViewErrors.Columns.Add("Code", "Código");
            dataGridViewErrors.Columns.Add("Amount", "Monto");

            num_code.Enter += NumericUpDown_Enter;
            num_code.KeyDown += NumericUpDown_KeyDown;
            num_amount.KeyDown += NumericUpDown_KeyDown;
            dataGridViewErrors.KeyPress += dataGridViewErrors_KeyPress;
            dataGridViewErrors.CellClick += dataGridViewErrors_CellClick;
        }

        private void FormErrores_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            // Limpiar el DataGridView antes de cargar los datos
            dataGridViewErrors.Rows.Clear();
            DataTable errorsDataTable = parentForm.GetErrorsDataTable();
            foreach (DataRow row in errorsDataTable.Rows)
            {
                string code = row["Código"].ToString();
                string amount = row["Monto"].ToString();
                dataGridViewErrors.Rows.Add(code, amount);
            }
        }

        private void btn_add_error_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los NumericUpDown
            string code = num_code.Value.ToString();
            string amount = num_amount.Value.ToString("N2");

            // Agregar los valores al DataGridView
            dataGridViewErrors.Rows.Add(code, amount);

            // Limpiar los NumericUpDown
            num_code.Value = 0;
            num_amount.Value = 0;
        }

        private void dataGridViewErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewErrors.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btn_errors_ok_Click(object sender, EventArgs e)
        {
            // Obtener los datos del DataGridView
            DataTable errorsData = new DataTable();
            foreach (DataGridViewColumn column in dataGridViewErrors.Columns)
            {
                errorsData.Columns.Add(column.HeaderText);
            }

            foreach (DataGridViewRow row in dataGridViewErrors.Rows)
            {
                DataRow dataRow = errorsData.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value;
                }
                errorsData.Rows.Add(dataRow);
            }

            // Pasar los datos de los errores a Form1 y cerrar FormErrores
            parentForm.ReceiveErrorsData(errorsData);
        }

        private void btn_errors_cancel_Click(object sender, EventArgs e)
        {
            // Cerrar FormErrores
            this.Close();
        }

        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            num_code.Select(0, num_code.Text.Length);
        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (num_code.Focused)
                {
                    num_amount.Focus();
                    num_amount.Select(0, num_amount.Text.Length);
                }
                else if (num_amount.Focused)
                {
                    btn_add_error_Click(sender, e);
                    num_code.Focus();
                    num_code.Select(0, num_code.Text.Length);
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                // Cierra la ventana actual (CantidadForm)
                this.Close();

                // Vuelve al formulario principal (Form1)
                Form1.ActiveForm.Focus();
            }
        }

        private void dataGridViewErrors_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                e.Handled = true;
                btn_errors_ok.Focus();
            }
        }
    }
}
