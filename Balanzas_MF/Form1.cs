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
        private List<Rubro> rubros = new List<Rubro>();

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

        // Define una clase para representar los rubros y sus productos
        public class Rubro
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public List<string> Productos { get; set; }

            public Rubro(string codigo)
            {
                Codigo = codigo;
                Nombre = "";
                Productos = new List<string>();
            }
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
                    if (line.Contains("NUMERO") || line.Contains("CODIGO"))
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

            // Definir la lista de códigos de productos excluidos y la suma de sus montos
            List<string> codigosExcluidos = new List<string> { "154", "158", "164", "165", "166", 
                "167", "168", "199", "204", "227", "272", "273", "274", "276", "277", "278", "279", 
                "293", "302", "303", "304", "306", "308", "406", "407", "408", "409", "417", "418", 
                "954" };
            decimal totalExcluidos = 0;

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
                        // Verificar si el código está en la lista de códigos excluidos
                        if (codigosExcluidos.Contains(codigo))
                        {
                            // Sumar el monto del producto excluido al total de excluidos
                            decimal subtotal;
                            if (decimal.TryParse(row["SUBTOTAL"].ToString(), NumberStyles.Float, culture, out subtotal))
                            {
                                totalExcluidos += subtotal;
                            }
                            else
                            {
                                // Manejar el caso en que la conversión falle
                                MessageBox.Show("Error: No se pudieron leer correctamente los datos de las balanzas.");
                            }
                        }
                        else
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
            }
            // Verificar si se encontraron productos excluidos
            if (totalExcluidos > 0)
            {
                // Agregar una nueva fila al DataTable para los productos excluidos
                DataRow newRow = resultTable.NewRow();
                newRow["producto"] = "Productos excluidos";
                newRow["errores_qendra"] = totalExcluidos;
                resultTable.Rows.Add(newRow);
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

            // Crea instancias de la clase Rubro y asigna los productos correspondientes
            rubros = new List<Rubro>
            {
                new Rubro("1") { Nombre = "CARNES VACUNA", Productos = { "100", "1000","1006", "1007", 
                        "1017", "1018", "103", "105", "106", "107", "110", "112", "115", "116", "118", 
                        "119", "120", "125", "126", "129", "130", "133", "134", "135", "138", "139", 
                        "140", "144", "145", "150", "155", "156", "160", "170", "180", "185", "186", 
                        "188", "190", "191", "193", "901" } },
                new Rubro("10") { Nombre = "FIAMBRES", Productos = { "121", "122", "127", "136", 
                        "148", "195", "211", "275", "351", "352", "353", "354", "355", "356", "357", 
                        "358", "359", "360", "361", "362", "363", "364", "365", "366", "367", "368", 
                        "369", "370", "371", "372", "373", "374", "375", "376", "377", "378", "379", 
                        "380", "381", "382", "383", "384", "385", "386", "387", "388", "389", "390", 
                        "391", "392", "405", "415", "421", "432", "433", "434", "441", "442", "491", 
                        "492", "518", "531", "602", "603", "604", "605", "614", "615", "631", "684", 
                        "929", "930", "934" } },
                new Rubro("11") { Nombre = "QUESOS", Productos = { "137", "149", "162", "194", "212", 
                        "216", "235", "422", "425", "428", "450", "451", "452", "453", "454", "455", 
                        "456", "457", "458", "459", "460", "461", "462", "463", "464", "465", "466", 
                        "467", "468", "469", "470", "471", "472", "473", "474", "475", "476", "477", 
                        "478", "479", "480", "481", "482", "483", "484", "616", "624", "633", "638", 
                        "639", "640", "648", "676", "677", "678", "683", "867", "874", "896", "897",
                        "928", "931", "939", "940" } },
                new Rubro("112") { Nombre = "PANIFICAD. PESABL.", Productos = { "699", } },
                new Rubro("113") { Nombre = "DULCES X KG.", Productos = { "593", "594", "595", "596", 
                        "597", "598", "599", } },
                new Rubro("115") { Nombre = "ENCURTIDOS X KG.", Productos = { "742", "743", "747", 
                        "748", "991", "993" } },
                new Rubro("17") { Nombre = "CONGELADOS", Productos = { "154", "158", "164", "165", 
                        "166", "167", "168", "199", "204", "227", "272", "273", "274", "276", "277", 
                        "278", "279", "293", "302", "303", "304", "306", "308", "406", "407", "408", 
                        "409", "417", "418", "954" } },
                new Rubro("2") { Nombre = "CARNES DE CERDO", Productos = { "1001", "1008", "1009", 
                        "1015","1016", "1019", "176", "177", "200", "205", "206", "210", "215", "218", 
                        "219", "220", "230", "231", "240", "250", "260", "265", "266", "270", "280", 
                        "283", "286", "289", "290", "292", "295", "296", "298", "629", "900" } },
                new Rubro("3") { Nombre = "POLLOS", Productos = { "505", "510", "515", "520", "550", 
                        "705", "710", "715", "720", "721", "724", "750" } },
                new Rubro("4") { Nombre = "ACHURAS", Productos = { "1005", "899", "960", "961", "963", 
                        "964", "965", "966", "970", "988", "989", "990" } },
                new Rubro("5") { Nombre = "CABR.CORD.LECHON", Productos = { "307", "431", "819" } },
                new Rubro("6") { Nombre = "ELAB. DE CARNE", Productos = { "300", "305", "310", 
                        "400", "507", "508", "509", "521", "600", "601", "617", "831" } },
                new Rubro("7") { Nombre = "ELAB. DE CERDO", Productos = { "330", "331", "332", 
                        "350", "410", "610", "611", "618", "711", "800", "810", "820", "830", "840", 
                        "855", "865", "866", "875", "880", "885", "895" } },
                new Rubro("8") { Nombre = "ELAB. DE POLLO", Productos = { "320", "325", "420", 
                        "430", "506", "620", "623", "821", "881" } },
                new Rubro("9") { Nombre = "ELAB. MIXTOS", Productos = { "440", "522", "523", "524", 
                        "651", "652", "653", "698" } },
            };

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

            // Filtrar las filas con valores diferentes de DBNull y cero en las columnas relevantes
            var filteredRows = resultTable.AsEnumerable().Where(row =>
            {
                // Verificar y convertir las columnas relevantes a decimal
                decimal ventasQendra = 0;
                object ventasQendraObject = row["ventas_qendra"];
                if (ventasQendraObject != DBNull.Value)
                {
                    ventasQendra = Convert.ToDecimal(ventasQendraObject);
                }

                decimal erroresQendra = 0;
                object erroresQendraObject = row["errores_qendra"];
                if (erroresQendraObject != DBNull.Value)
                {
                    erroresQendra = Convert.ToDecimal(erroresQendraObject);
                }

                decimal ventasUltranet = 0;
                object ventasUltranetObject = row["ventas_ultranet"];
                if (ventasUltranetObject != DBNull.Value)
                {
                    ventasUltranet = Convert.ToDecimal(ventasUltranetObject);
                }

                // Devolver verdadero si alguna de las columnas es diferente de cero
                return ventasQendra != 0 || erroresQendra != 0 || ventasUltranet != 0;
            }).CopyToDataTable();

            // Verificar si se encontraron productos excluidos
            if (totalExcluidos > 0)
            {
                // Agregar una nueva fila al DataTable para los productos excluidos
                DataRow newRow = filteredRows.NewRow();
                newRow["codigo"] = DBNull.Value; // No hay código asociado
                newRow["producto"] = "Productos excluidos";
                newRow["ventas_qendra"] = 0; // No hay ventas Qendra asociadas
                newRow["errores_qendra"] = totalExcluidos;
                newRow["diferencia_qendra"] = -totalExcluidos;
                newRow["ventas_ultranet"] = 0; // No hay ventas UltraNet asociadas
                newRow["total"] = -totalExcluidos; // Usar un valor negativo para indicar que es una pérdida
                filteredRows.Rows.Add(newRow);
            }

            // Crear una nueva tabla para los totales de los rubros
            DataTable totalRubroTable = new DataTable();
            totalRubroTable.Columns.Add("codigo");
            totalRubroTable.Columns.Add("nombre");
            totalRubroTable.Columns.Add("ventas_qendra");
            totalRubroTable.Columns.Add("errores");
            totalRubroTable.Columns.Add("diferencia");
            totalRubroTable.Columns.Add("ventas_ultranet");
            totalRubroTable.Columns.Add("total", typeof(decimal));

            // Calcular total de cada rubro
            foreach (var rubro in rubros)
            {
                decimal totalRubro = 0;

                // Sumar los totales de los productos del rubro
                foreach (DataRow row in filteredRows.Rows)
                {
                    if (rubro.Productos.Contains(row["codigo"].ToString()))
                    {
                        totalRubro += Convert.ToDecimal(row["total"]);
                    }
                }

                // Agregar una fila para el total del rubro a la tabla de totales de rubro
                totalRubroTable.Rows.Add(null, rubro.Nombre, null, null, null, null, totalRubro);
            }

            // Agregar las filas de totales de rubro al filteredRows
            foreach (DataRow row in totalRubroTable.Rows)
            {
                filteredRows.Rows.Add(row.ItemArray);
            }

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
            dataGridView1.Columns["total"].HeaderText = "Diferencia total";

            // Ordenar tabla por código
            OrdenarDataGridView1(filteredRows);
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

        private void OrdenarDataGridView1(DataTable filteredRows)
        {
            // Crear una nueva tabla para los datos ordenados
            DataTable orderedData = filteredRows.Clone();

            // Calcular los totales de los rubros y agregar las filas al DataGridView
            foreach (var rubro in rubros)
            {
                decimal totalRubro = 0;

                // Agregar las filas de cada producto perteneciente a ese rubro
                foreach (var codigoProducto in rubro.Productos)
                {
                    DataRow productoRow = filteredRows.Select($"codigo = '{codigoProducto}'").FirstOrDefault();
                    if (productoRow != null)
                    {
                        totalRubro += Convert.ToDecimal(productoRow["total"]); // Sumar el total del producto al total del rubro
                        orderedData.Rows.Add(productoRow.ItemArray); // Agregar la fila del producto al orderedData
                    }
                }

                // Agregar una fila al orderedData con el total del rubro
                orderedData.Rows.Add(null, rubro.Nombre, null, null, null, null, totalRubro);
            }

            // Calcular el total de los productos excluidos
            DataRow productosExcluidosRow = filteredRows.Select("producto = 'Productos excluidos'").FirstOrDefault();
            decimal totalProductosExcluidos = productosExcluidosRow != null ? Convert.ToDecimal(productosExcluidosRow["errores_qendra"]) : 0;

            // Calcular el total general restando el total de productos excluidos
            decimal totalGeneral = orderedData.AsEnumerable()
                                               .Where(row => row.Field<string>("codigo") != null) // Filtrar filas de rubros
                                               .Sum(row => Convert.ToDecimal(row["total"])) - totalProductosExcluidos;

            // Agregar la fila de productos excluidos al orderedData si existe
            if (productosExcluidosRow != null)
            {
                orderedData.Rows.Add(productosExcluidosRow.ItemArray);
            }

            // Enlazar la nueva tabla al DataGridView
            dataGridView1.DataSource = orderedData;

            // Establecer el formato de las celdas para mostrar dos decimales
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.ValueType == typeof(decimal))
                {
                    column.DefaultCellStyle.Format = "N2";
                }
            }

            // Resaltar las filas de totales de rubros y productos excluidos en negrita
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["codigo"].Value == DBNull.Value)
                {
                    row.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                }
            }

            // Actualizar la etiqueta de la diferencia total
            label_total.Text = "$ " + totalGeneral;
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
                int excelRowIndex = 3; // Comenzar desde la tercera fila después del título y la fila de encabezados
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[excelRowIndex, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value;
                        worksheet.Cells[excelRowIndex, j + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        // Verifica si la fila es un total de rubro y aplica negrita si es así
                        if (dataGridView1.Rows[i].Cells["producto"].Value?.ToString() == "Productos excluidos")
                        {
                            worksheet.Cells[excelRowIndex, j + 1].Style.Font.Bold = true;
                        }
                    }
                    excelRowIndex++;
                }

                // Agregar la fila de "TOTALES"
                int lastRowIndex = dataGridView1.Rows.Count + 3; // Última fila de datos + 3 (título, fila de totales y nueva fila "TOTALES")
                worksheet.Cells[lastRowIndex, 2].Value = "TOTALES";

                // Calcular la suma total de cada columna
                for (int i = 2; i < dataGridView1.Columns.Count - 1; i++) // Ignorar las dos primeras columnas que contiene el título
                {
                    decimal totalSum = 0;
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                        {
                            decimal cellValue;
                            if (decimal.TryParse(dataGridView1.Rows[j].Cells[i].Value?.ToString(), out cellValue))
                            {
                                totalSum += cellValue;
                            }
                        }
                    }
                    worksheet.Cells[lastRowIndex, i + 1].Value = totalSum; // i + 1 porque Excel comienza desde la columna 1
                }

                // Calcular la suma total de "Diferencia Qendra" y "Ventas UltraNet"
                decimal totalDiferenciaQendra = 0;
                decimal totalVentasUltraNet = 0;

                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    object value_qendra = dataGridView1.Rows[j].Cells["diferencia_qendra"].Value;

                    if (value_qendra != DBNull.Value && value_qendra != null)
                    {
                        totalDiferenciaQendra += Convert.ToDecimal(value_qendra);
                    }
                    object value_un = dataGridView1.Rows[j].Cells["ventas_ultranet"].Value;

                    if (value_un != DBNull.Value && value_un != null)
                    {
                        totalVentasUltraNet += Convert.ToDecimal(value_un);
                    }
                }

                // Calcular el total general restando el total de "Diferencia Qendra" al total de "Ventas UltraNet"
                decimal totalGeneral = totalVentasUltraNet - totalDiferenciaQendra;
                worksheet.Cells[lastRowIndex, dataGridView1.Columns.Count].Value = totalGeneral;

                // Establecer el estilo de la fila "TOTALES" en negrita
                worksheet.Cells[lastRowIndex, 1, lastRowIndex, dataGridView1.Columns.Count].Style.Font.Bold = true;

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
