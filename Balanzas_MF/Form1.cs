using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Balanzas_MF
{
    public partial class Form1 : Form
    {
        private DataTable salesDataTable;
        private List<DataTable> balDataTables;
        private DataTable errorsDataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
            salesDataTable = new DataTable();
            balDataTables = new List<DataTable>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            label_sales.Text = "";
            label_errors.Text = "";
            label_bal1.Text = "";
            label_bal2.Text = "";
            label_bal3.Text = "";
            label_bal4.Text = "";
            label_bal5.Text = "";
        }

        private void btn_load_sales_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadSalesData(filePath);
                label_sales.Text = "Cargado";
            }
        }

        private void LoadSalesData(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Buscar la columna "c_Producto" en cualquier fila del archivo
                int productoRowIndex = -1;
                int productoColumnIndex = -1;
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        string header = worksheet.Cells[row, col].Value?.ToString();
                        if (header == "c_Producto")
                        {
                            productoRowIndex = row;
                            productoColumnIndex = col;
                            break;
                        }
                    }
                    if (productoRowIndex != -1)
                    {
                        break; // Si encontramos la columna, salimos del bucle externo
                    }
                }

                if (productoRowIndex == -1)
                {
                    MessageBox.Show("No se encontró la columna 'c_Producto'.");
                    return;
                }

                // Buscar la columna "Producto" en la misma fila que "c_Producto"
                int descripcionColumnIndex = -1;
                for (int col = productoColumnIndex + 1; col <= colCount; col++)
                {
                    string header = worksheet.Cells[productoRowIndex, col].Value?.ToString();
                    if (header == "Producto")
                    {
                        descripcionColumnIndex = col;
                        break;
                    }
                }

                if (descripcionColumnIndex == -1)
                {
                    MessageBox.Show("No se encontró la columna 'Producto' en la fila de 'c_Producto'.");
                    return;
                }

                // Buscar la columna "SubTotal" en la misma fila que "c_Producto"
                int subtotalColumnIndex = -1;
                for (int col = productoColumnIndex + 1; col <= colCount; col++)
                {
                    string header = worksheet.Cells[productoRowIndex, col].Value?.ToString();
                    if (header == "SubTotal")
                    {
                        subtotalColumnIndex = col;
                        break;
                    }
                }

                if (subtotalColumnIndex == -1)
                {
                    MessageBox.Show("No se encontró la columna 'SubTotal' en la fila de 'c_Producto'.");
                    return;
                }

                // Leer los datos de las columnas encontradas
                salesDataTable.Clear();
                salesDataTable.Columns.Clear();
                salesDataTable.Columns.Add("c_Producto");
                salesDataTable.Columns.Add("Producto");
                salesDataTable.Columns.Add("SubTotal");

                // Iterar sobre las filas para obtener los datos
                for (int row = productoRowIndex + 1; row <= rowCount; row++)
                {
                    DataRow newRow = salesDataTable.Rows.Add();
                    newRow["c_Producto"] = worksheet.Cells[row, productoColumnIndex].Value?.ToString();
                    newRow["Producto"] = worksheet.Cells[row, descripcionColumnIndex].Value?.ToString();
                    newRow["SubTotal"] = worksheet.Cells[row, subtotalColumnIndex].Value?.ToString();
                }
            }
        }

        // Método para recibir los datos de errores desde FormErrores y cerrar FormErrores
        public void ReceiveErrorsData(DataTable errorsData)
        {
            // Copiar los datos de los errores al DataTable en Form1
            errorsDataTable = errorsData;

            // Aquí puedes trabajar con los datos de los errores en errorsDataTable según tus necesidades
            label_errors.Text = "Cargado";
            // Cerrar FormErrores
            CloseFormErrores();
        }

        // Método para cerrar FormErrores
        private void CloseFormErrores()
        {
            FormErrores formErrores = Application.OpenForms.OfType<FormErrores>().FirstOrDefault();
            if (formErrores != null)
            {
                formErrores.Close();
            }
        }

        private void btn_load_errors_Click(object sender, EventArgs e)
        {
            FormErrores formErrores = new FormErrores(this); // Pasar una referencia de Form1 a FormErrores
            formErrores.Show();
        }

        private void btn_load_bal1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    LoadBalData(filePath);
                }
                label_bal1.Text = "Cargado";
            }
        }

        private void btn_load_bal2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    LoadBalData(filePath);
                }
                label_bal2.Text = "Cargado";
            }
        }

        private void btn_load_bal3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    LoadBalData(filePath);
                }
                label_bal3.Text = "Cargado";
            }
        }

        private void btn_load_bal4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    LoadBalData(filePath);
                }
                label_bal4.Text = "Cargado";
            }
        }

        private void btn_load_bal5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    LoadBalData(filePath);
                }
                label_bal5.Text = "Cargado";
            }
        }

        private void LoadBalData(string filePath)
        {
            DataTable balDataTable = new DataTable();
            balDataTable.Columns.Add("CODIGO");
            balDataTable.Columns.Add("SUBTOTAL");

            bool startReading = false;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Si encontramos la línea que indica el inicio de los datos relevantes
                    if (line.Contains("NUMERO  DESCRIPCION"))
                    {
                        startReading = true;
                        continue;
                    }

                    // Si encontramos la línea que indica el final de los datos relevantes
                    if (line.Contains("TOTALES"))
                    {
                        break;
                    }

                    // Si estamos leyendo las líneas con los datos relevantes
                    if (startReading && !string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Asumimos que el código está en la primera columna y el subtotal en la última
                        if (parts.Length >= 2)
                        {
                            string codigo = parts[0];
                            

                            // Verificar si el código es numérico antes de agregar la fila
                            int codigoInt;
                            if (!string.IsNullOrEmpty(codigo) && int.TryParse(codigo, out codigoInt) && codigoInt >= 100)
                            {
                                string subtotal = parts[parts.Length - 1].Replace("$", "");
                                DataRow newRow = balDataTable.Rows.Add();
                                newRow["CODIGO"] = codigo;
                                newRow["SUBTOTAL"] = subtotal;
                            }
                        }
                    }
                }
            }

            balDataTables.Add(balDataTable);
        }


        private void btn_process_data_Click(object sender, EventArgs e)
        {
            // Crear una tabla para almacenar los resultados
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("codigo");
            resultTable.Columns.Add("producto");
            resultTable.Columns.Add("ventas_qendra", typeof(decimal));
            resultTable.Columns.Add("errores_qendra", typeof(decimal));
            resultTable.Columns.Add("ventas_ultranet", typeof(decimal));
            resultTable.Columns.Add("total", typeof(decimal));

            // Procesar los datos de los archivos txt
            foreach (DataTable balDataTable in balDataTables)
            {
                foreach (DataRow row in balDataTable.Rows)
                {
                    string codigo = row["CODIGO"].ToString();

                    // Verificar si el código no está vacío
                    if (!string.IsNullOrEmpty(codigo))
                    {
                        decimal subtotal = Convert.ToDecimal(row["SUBTOTAL"]);

                        // Buscar si el código ya existe en la tabla de resultados
                        DataRow existingRow = resultTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("codigo") == codigo);

                        if (existingRow != null)
                        {
                            // Si el código ya existe, sumar el subtotal a las ventas_qendra existentes
                            existingRow["ventas_qendra"] = Convert.ToDecimal(existingRow["ventas_qendra"]) + subtotal;
                        }
                        else
                        {
                            // Si el código no existe, agregar una nueva fila
                            resultTable.Rows.Add(codigo, null, subtotal, 0, 0, 0);
                        }
                    }
                }
            }

            // Procesar los errores de FormErrores
            foreach (DataRow row in errorsDataTable.Rows)
            {
                string codigo = row["Código"].ToString();

                // Verificar si el código no está vacío
                if (!string.IsNullOrEmpty(codigo))
                {
                    decimal errorAmount;

                    // Verificar si el valor en la columna "Monto" es DBNull antes de convertirlo a decimal
                    if (row["Monto"] != DBNull.Value)
                    {
                        errorAmount = Convert.ToDecimal(row["Monto"]);
                    }
                    else
                    {
                        // Asignar un valor predeterminado, por ejemplo, 0
                        errorAmount = 0;
                    }

                    // Buscar si el código ya existe en la tabla de resultados
                    DataRow existingRow = resultTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("codigo") == codigo);

                    if (existingRow != null)
                    {
                        // Si el código ya existe, agregar el monto de error
                        existingRow["errores_qendra"] = errorAmount;
                    }
                    else
                    {
                        // Si el código no existe, agregar una nueva fila
                        resultTable.Rows.Add(codigo, null, 0, errorAmount, 0, 0);
                    }
                }
            }

            // Procesar los datos del archivo xlsx
            foreach (DataRow row in salesDataTable.Rows)
            {
                string codigo = row["c_Producto"].ToString();

                // Verificar si el código no está vacío
                if (!string.IsNullOrEmpty(codigo))
                {
                    string producto = row["Producto"].ToString();
                    decimal subtotal = Convert.ToDecimal(row["SubTotal"]);

                    // Buscar si el código ya existe en la tabla de resultados
                    DataRow existingRow = resultTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("codigo") == codigo);

                    if (existingRow != null)
                    {
                        // Si el código ya existe, agregar la descripción y el subtotal de ventas_ultranet
                        existingRow["producto"] = producto;
                        existingRow["ventas_ultranet"] = subtotal;
                    }
                    /*else
                    {
                        // Si el código no existe, agregar una nueva fila
                        resultTable.Rows.Add(codigo, producto, 0, 0, subtotal, 0);
                    }*/
                }
            }

            // Calcular el total
            foreach (DataRow row in resultTable.Rows)
            {
                decimal ventas_qendra = Convert.ToDecimal(row["ventas_qendra"]);
                decimal errores_qendra = Convert.ToDecimal(row["errores_qendra"]);
                decimal ventas_ultranet = Convert.ToDecimal(row["ventas_ultranet"]);
                decimal total = ventas_ultranet - ventas_qendra - errores_qendra;
                row["total"] = total;
            }

            // Mostrar los resultados en dataGridView1
            dataGridView1.DataSource = resultTable;

            // Cambiar los nombres de los encabezados de columna
            dataGridView1.Columns["codigo"].HeaderText = "Código";
            dataGridView1.Columns["producto"].HeaderText = "Producto";
            dataGridView1.Columns["ventas_qendra"].HeaderText = "Ventas Qendra";
            dataGridView1.Columns["errores_qendra"].HeaderText = "Errores Qendra";
            dataGridView1.Columns["ventas_ultranet"].HeaderText = "Ventas UltraNet";
            dataGridView1.Columns["total"].HeaderText = "Total";

            // Ordenar tabla por código
            OrdenarDataGridView1("codigo");
        }

        private void btn_clean_fields_Click(object sender, EventArgs e)
        {
            // Limpiar los datos cargados de los archivos xlsx y txt
            salesDataTable.Clear();
            balDataTables.Clear();

            // Limpiar los errores provenientes de FormErrores
            errorsDataTable.Clear();

            // Limpiar dataGridView1
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Limpiar los labels
            label_sales.Text = "";
            label_errors.Text = "";
            label_bal1.Text = "";
            label_bal2.Text = "";
            label_bal3.Text = "";
            label_bal4.Text = "";
            label_bal5.Text = "";
        }

        private void OrdenarDataGridView1(string columnName)
        {
            // Verifica si hay datos en el dataGridView1
            if (dataGridView1.DataSource is DataTable dataTable)
            {
                // Ordena el DataTable por la columna 2
                dataTable.DefaultView.Sort = columnName + " ASC";

                // Vuelve a asignar la fuente de datos al DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btn_print_report_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay datos en dataGridView2
                if (dataGridView1.Rows.Count > 0)
                {
                    PrintDocument pd = new PrintDocument();

                    // Asocia el evento de impresión
                    pd.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                    // Configura el tamaño del papel
                    pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 210, 297); // Ancho x Alto en cien milésimas de pulgada

                    // Muestra el cuadro de diálogo de impresión
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = pd;

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        pd.Print();
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos para imprimir en el informe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (System.ComponentModel.Win32Exception win32Ex)
            {
                MessageBox.Show($"Error al imprimir: {win32Ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Otro error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fuente = new Font("Arial", 10, FontStyle.Regular);
        }
    }
}
