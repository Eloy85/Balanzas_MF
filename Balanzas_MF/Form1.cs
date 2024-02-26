using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            dataGridView1.CellClick += dataGridView1_CellClick;
            // Agregar las columnas al DataTable de errores
            errorsDataTable.Columns.Add("Código", typeof(string));
            errorsDataTable.Columns.Add("Monto", typeof(decimal));
        }

        // Método para obtener la tabla de errores
        public DataTable GetErrorsDataTable()
        {
            return errorsDataTable;
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
            label_diferencia.Text = "";
            label_total.Text = "";
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

                // Buscar las columnas "c_Producto", "Producto", "Sub Total" y "c_Rubro" en cualquier fila del archivo
                int productoRowIndex = -1;
                int productoColumnIndex = -1;
                int descripcionColumnIndex = -1;
                int subtotalColumnIndex = -1;
                int rubroColumnIndex = -1;
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        string header = worksheet.Cells[row, col].Value?.ToString();
                        if (header == "c_Producto")
                        {
                            productoRowIndex = row;
                            productoColumnIndex = col;
                        }
                        else if (header == "Producto")
                        {
                            descripcionColumnIndex = col;
                        }
                        else if (header == "Sub Total")
                        {
                            subtotalColumnIndex = col;
                        }
                        else if (header == "c_Rubro")
                        {
                            rubroColumnIndex = col;
                        }
                    }
                    if (productoRowIndex != -1 && descripcionColumnIndex != -1 && subtotalColumnIndex != -1 && rubroColumnIndex != -1)
                    {
                        break; // Si encontramos todas las columnas, salimos del bucle externo
                    }
                }

                if (productoRowIndex == -1 || descripcionColumnIndex == -1 || subtotalColumnIndex == -1 || rubroColumnIndex == -1)
                {
                    MessageBox.Show("No se encontraron todas las columnas necesarias.");
                    return;
                }

                // Inicializar un diccionario para rastrear los subtotales por código de producto
                Dictionary<string, Tuple<string, decimal>> subtotalDictionary = new Dictionary<string, Tuple<string, decimal>>();

                // Excluir ciertos códigos de rubro
                HashSet<string> excludedRubroCodes = new HashSet<string> { "12", "13", "14", "15", "17", "18", "19", "20", "22", "23", "24", "25", "100", "110", "111", "114" };

                // Iterar sobre las filas para obtener los datos
                for (int row = productoRowIndex + 1; row <= rowCount; row++)
                {
                    string codigo = worksheet.Cells[row, productoColumnIndex].Value?.ToString();
                    string rubro = worksheet.Cells[row, rubroColumnIndex].Value?.ToString();

                    // Verificar si el código de rubro está excluido
                    if (excludedRubroCodes.Contains(rubro))
                    {
                        continue; // Saltar esta fila
                    }

                    string producto = worksheet.Cells[row, descripcionColumnIndex].Value?.ToString();
                    string subtotalStr = worksheet.Cells[row, subtotalColumnIndex].Value?.ToString();
                    decimal subtotal = 0;

                    // Convertir el subtotal a decimal
                    if (!string.IsNullOrEmpty(subtotalStr) && decimal.TryParse(subtotalStr, out decimal parsedSubtotal))
                    {
                        subtotal = parsedSubtotal;
                    }

                    // Verificar si ya existe un subtotal para este código de producto
                    if (subtotalDictionary.ContainsKey(codigo))
                    {
                        // Si existe, sumar el subtotal actual al subtotal existente
                        var existingData = subtotalDictionary[codigo];
                        subtotalDictionary[codigo] = new Tuple<string, decimal>(producto, existingData.Item2 + subtotal);
                    }
                    else
                    {
                        // Si no existe, agregar el subtotal al diccionario
                        subtotalDictionary[codigo] = new Tuple<string, decimal>(producto, subtotal);
                    }
                }

                // Crear una tabla para almacenar los datos
                salesDataTable.Clear();
                salesDataTable.Columns.Clear();
                salesDataTable.Columns.Add("c_Producto");
                salesDataTable.Columns.Add("Producto");
                salesDataTable.Columns.Add("Sub Total");

                // Agregar los datos del diccionario a la tabla de datos
                foreach (var pair in subtotalDictionary)
                {
                    DataRow newRow = salesDataTable.Rows.Add();
                    newRow["c_Producto"] = pair.Key;
                    newRow["Producto"] = pair.Value.Item1;
                    newRow["Sub Total"] = pair.Value.Item2;
                }
            }
        }

        // Método para recibir los datos de errores desde FormErrores y cerrar FormErrores
        public void ReceiveErrorsData(DataTable errorsData)
        {
            // Limpiar errorsDataTable
            errorsDataTable.Rows.Clear();
            // Agregar los errores al DataTable global
            foreach (DataRow row in errorsData.Rows)
            {
                errorsDataTable.ImportRow(row);
            }

            // Cambiar el label de Errores
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
            formErrores.ShowDialog();
        }

        private void btn_load_bal1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Multiselect = false;

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
            openFileDialog.Multiselect = false;

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
            openFileDialog.Multiselect = false;

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
            openFileDialog.Multiselect = false;

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
            openFileDialog.Multiselect = false;

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
            resultTable.Columns.Add("diferencia_qendra", typeof(decimal));
            resultTable.Columns.Add("ventas_ultranet", typeof(decimal));
            resultTable.Columns.Add("total", typeof(decimal));

            // Diccionario para rastrear los montos de errores por código
            Dictionary<string, decimal> errorAmounts = new Dictionary<string, decimal>();

            // Variable para almacenar la suma de los valores de la columna "total"
            decimal totalSum = 0;

            // Procesar los datos de los archivos txt

            // Crear una instancia de CultureInfo con la cultura específica que deseas utilizar
            CultureInfo culture = new CultureInfo("es-ES"); // En este caso, la cultura sería para los decimales con coma (,)
            foreach (DataTable balDataTable in balDataTables)
            {
                foreach (DataRow row in balDataTable.Rows)
                {
                    string codigo = row["CODIGO"].ToString();

                    // Verificar si el código no está vacío
                    if (!string.IsNullOrEmpty(codigo))
                    {
                        // Convertir el valor de SUBTOTAL a decimal utilizando la cultura adecuada
                        decimal subtotal;
                        if (decimal.TryParse(row["SUBTOTAL"].ToString(), NumberStyles.Float, culture, out subtotal))
                        {
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
                        else
                        {
                            // Manejar el caso en que la conversión falla
                            MessageBox.Show("Error: No se pudieron leer correctamente los datos de las balanzas.");
                        }
                    }
                }
            }

            // Procesar los errores de FormErrores
            foreach (DataRow row in errorsDataTable.Rows)
            {
                string codigo = row["Código"].ToString();
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

                // Verificar si ya existe un monto para este código
                if (errorAmounts.ContainsKey(codigo))
                {
                    // Si existe, sumar el monto actual al monto existente
                    errorAmounts[codigo] += errorAmount;
                }
                else
                {
                    // Si no existe, agregar el monto al diccionario
                    errorAmounts[codigo] = errorAmount;
                }
            }

            // Agregar los montos de errores al resultado
            foreach (var pair in errorAmounts)
            {
                string codigo = pair.Key;
                decimal errorAmount = pair.Value;

                DataRow existingRow = resultTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("codigo") == codigo);
                if (existingRow != null)
                {
                    existingRow["errores_qendra"] = errorAmount;
                }
                else
                {
                    // Si el código no existe, agregar una nueva fila
                    resultTable.Rows.Add(codigo, null, 0, errorAmount, 0, 0);
                }
            }

            // Procesar los datos del archivo xlsx
            foreach (DataRow row in salesDataTable.Rows)
            {
                string codigo = row["c_Producto"].ToString();
                string producto = row["Producto"].ToString();
                decimal subtotal = Convert.ToDecimal(row["Sub Total"]);

                // Buscar si el código ya existe en la tabla de resultados
                DataRow existingRow = resultTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("codigo") == codigo);

                if (existingRow != null)
                {
                    // Si el código ya existe, agregar la descripción y el subtotal de ventas_ultranet
                    existingRow["producto"] = producto;
                    existingRow["ventas_ultranet"] = subtotal;
                }
                else
                {
                    // Si el código no existe, agregar una nueva fila
                    DataRow newRow = resultTable.NewRow();
                    newRow["codigo"] = codigo;
                    newRow["producto"] = producto;
                    newRow["ventas_ultranet"] = subtotal;
                    newRow["ventas_qendra"] = 0; // Establecer valores iniciales en 0
                    newRow["errores_qendra"] = 0;
                    newRow["diferencia_qendra"] = 0;
                    newRow["total"] = 0;
                    resultTable.Rows.Add(newRow);
                }
            }

            // Calcular el total y diferencia para cada fila
            foreach (DataRow row in resultTable.Rows)
            {
                decimal ventas_qendra = Convert.ToDecimal(row["ventas_qendra"]);
                decimal errores_qendra = Convert.ToDecimal(row["errores_qendra"]);
                decimal diferencia_qendra = ventas_qendra - errores_qendra;
                decimal ventas_ultranet = Convert.ToDecimal(row["ventas_ultranet"]);
                decimal total = ventas_ultranet - diferencia_qendra;
                row["diferencia_qendra"] = diferencia_qendra;
                row["total"] = total;
                totalSum += total;
            }

            // Filtrar las filas con valores iguales a 0 en las columnas relevantes
            var filteredRows = resultTable.AsEnumerable().Where(row =>
            {
                decimal ventasQendra = Convert.ToDecimal(row["ventas_qendra"]);
                decimal erroresQendra = Convert.ToDecimal(row["errores_qendra"]);
                decimal ventasUltranet = Convert.ToDecimal(row["ventas_ultranet"]);

                return ventasQendra != 0 || erroresQendra != 0 || ventasUltranet != 0;
            }).CopyToDataTable();

            // Mostrar los resultados en dataGridView1
            dataGridView1.DataSource = filteredRows;
            label_diferencia.Text = "Diferencia total:";
            label_total.Text = "$ " + totalSum;

            // Cambiar los nombres de los encabezados de columna
            dataGridView1.Columns["codigo"].HeaderText = "Código";
            dataGridView1.Columns["producto"].HeaderText = "Producto";
            dataGridView1.Columns["ventas_qendra"].HeaderText = "Ventas Qendra";
            dataGridView1.Columns["errores_qendra"].HeaderText = "Errores Qendra";
            dataGridView1.Columns["diferencia_qendra"].HeaderText = "Diferencia Qendra";
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
            label_diferencia.Text = "";
            label_total.Text = "";
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
            // Establecer el formato de las celdas para mostrar dos decimales
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.ValueType == typeof(decimal))
                {
                    column.DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btn_print_report_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo archivo Excel
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte");

                // Fusionar las celdas para el título
                worksheet.Cells["A1:G1"].Merge = true;
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                worksheet.Cells["A1"].Value = "CONTROL DE BALANZAS AL " + currentDate;
                worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;

                // Agregar los encabezados de las columnas
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cells[2, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                    worksheet.Cells[2, i + 1].Style.Font.Bold = true;
                }

                // Agregar los datos de dataGridView1 al archivo Excel
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 3, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value;
                        worksheet.Cells[i + 3, j + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    }
                }

                // Agregar la fila de "DIFERENCIA TOTAL"
                worksheet.Cells[dataGridView1.Rows.Count + 3, 2].Value = "DIFERENCIA TOTAL";

                // Calcular la suma de la columna "Total"
                decimal totalSum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    totalSum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Total"].Value);
                }
                worksheet.Cells[dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count].Value = totalSum;

                // Establecer el estilo de la fila "TOTAL" en negrita
                worksheet.Cells[dataGridView1.Rows.Count + 3, 1, dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count].Style.Font.Bold = true;

                // Establecer el ancho de las columnas y el formato de las celdas
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Aplicar formato para mostrar dos decimales en todas las celdas
                worksheet.Cells[worksheet.Dimension.Address].Style.Numberformat.Format = "0.00";

                // Ajustar la alineación de las columnas en el archivo Excel generado
                for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                {
                    if (i != 1 && i != 2) // Ignorar la primera columna que contiene el título
                    {
                        worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    }
                }

                // Configurar la propiedad de impresión para ajustar todas las columnas en una página
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;

                // Configurar el tamaño de papel por defecto como A4
                worksheet.PrinterSettings.PaperSize = (ePaperSize)Enum.Parse(typeof(ePaperSize), "A4");

                // Guardar el archivo
                string fileName = $"Reporte Balanzas {currentDate}.xlsx";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                // Abrir el archivo automáticamente
                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show("Error: No se pudo abrir el archivo.");
                }
            }
            catch (System.IO.IOException)
            {
                // Manejar la excepción cuando el archivo ya está abierto
                MessageBox.Show("El archivo está siendo utilizado por otra aplicación. Cierre el archivo y vuelva a intentarlo.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                MessageBox.Show("Ocurrió un error al guardar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
