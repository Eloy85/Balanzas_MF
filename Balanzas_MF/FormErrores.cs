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
            //btn_errors_ok.Click += btn_errors_ok_Click;

            // Agregar las columnas al DataGridView
            dataGridViewErrors.Columns.Add("Code", "Código");
            dataGridViewErrors.Columns.Add("Amount", "Monto");

        }

        private void FormErrores_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_error_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los NumericUpDown
            string code = num_code.Value.ToString();
            string amount = num_amount.Value.ToString();

            // Agregar los valores al DataGridView
            dataGridViewErrors.Rows.Add(code, amount);

            // Limpiar los NumericUpDown
            num_code.Value = 0;
            num_amount.Value = 0;
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
    }
}
