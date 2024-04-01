using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Balanzas_MF
{
    public partial class FormConfig : Form
    {
        private Form1 parentForm;
        public string filepath;
        public HashSet<int> numerosHashSet = new HashSet<int>();
        public List<int> numerosProductos = new List<int>();
        public bool ReportSaveChecked => cb_report_save.Checked;
        public DataTable tablaProductos = new DataTable();
        public DataTable tablaRubros = new DataTable();

        public FormConfig(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            txt_excluded_categories.KeyPress += txt_excluded_categories_KeyPress;
            txt_excluded_products.KeyPress += txt_excluded_products_KeyPress;
            dataGridViewCategories.CellClick += dataGridView_CellClick;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            var generalSettings = ConfigurationManager.GetSection("GeneralSettings") as NameValueCollection;
            filepath = generalSettings["filepath"];
            txt_report_location.Text = filepath;
            cb_report_save.Checked = Convert.ToBoolean(generalSettings["askReportLocation"]);
            txt_excluded_categories.Text = generalSettings["excludedCategories"];
            txt_excluded_products.Text = generalSettings["excludedProducts"];

            // Cargar el archivo App.config
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            // Buscar la sección de RubrosSection
            var rubrosSection = xmlDoc.SelectSingleNode("//RubrosSection");

            // Si la sección existe
            if (rubrosSection != null)
            {
                // Limpiar el DataGridView
                dataGridViewCategories.Rows.Clear();
                tablaRubros.Columns.Add("rubro");
                tablaRubros.Columns.Add("descripcion");
                tablaRubros.Columns.Add("productos");

                // Recorrer los elementos Rubro dentro de RubrosSection
                foreach (XmlNode rubroNode in rubrosSection.ChildNodes)
                {
                    if (rubroNode.Name == "Rubro" && rubroNode.Attributes != null)
                    {
                        // Obtener los atributos del Rubro (codigo y nombre)
                        var codigo = rubroNode.Attributes["codigo"].Value;
                        var nombre = rubroNode.Attributes["nombre"].Value;

                        // Crear una lista de códigos de productos
                        List<string> productos = new List<string>();
                        foreach (XmlNode productoNode in rubroNode.SelectNodes("productos/producto"))
                        {
                            productos.Add(productoNode.Attributes["codigo"].Value);
                        }

                        // Agregar una fila al DataGridView con los datos del Rubro
                        tablaRubros.Rows.Add(codigo, nombre, string.Join(", ", productos));
                    }
                }
                dataGridViewCategories.DataSource = tablaRubros;
                dataGridViewCategories.Columns["rubro"].HeaderText = "Rubro";
                dataGridViewCategories.Columns["descripcion"].HeaderText = "Descripción";
                dataGridViewCategories.Columns["productos"].HeaderText = "Productos";
                string nombreColumna = dataGridViewCategories.Columns[0].Name;
                OrdenarDataGridView(nombreColumna);
            }
        }

        public class ProductoElement : ConfigurationElement
        {
            [ConfigurationProperty("codigo", IsKey = true, IsRequired = true)]
            public int Codigo
            {
                get { return (int)this["codigo"]; }
                set { this["codigo"] = value; }
            }
        }

        public class RubroElement : ConfigurationElement
        {
            [ConfigurationProperty("codigo", IsKey = true, IsRequired = true)]
            public int Codigo
            {
                get { return (int)this["codigo"]; }
                set { this["codigo"] = value; }
            }

            [ConfigurationProperty("nombre", IsRequired = true)]
            public string Nombre
            {
                get { return (string)this["nombre"]; }
                set { this["nombre"] = value; }
            }

            [ConfigurationProperty("productos")]
            public ProductoElementCollection Productos
            {
                get { return (ProductoElementCollection)this["productos"]; }
            }
        }

        public class ProductoElementCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new ProductoElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((ProductoElement)element).Codigo;
            }
        }

        // Método para obtener la configuración de un rubro específico
        public RubroElement ObtenerRubroConfig(int codigoRubro)
        {
            var rubrosSection = ConfigurationManager.GetSection("RubrosSection") as NameValueCollection;
            if (rubrosSection != null)
            {
                var rubro = rubrosSection.AllKeys
                    .Select(key => rubrosSection[key])
                    .Cast<RubroElement>()
                    .FirstOrDefault(r => r.Codigo == codigoRubro);
                return rubro;
            }
            return null;
        }

        private void btn_edit_location_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.SelectedPath.ToString();
                txt_report_location.Text = filepath;
            }
        }

        private void btn_excluded_categories_Click(object sender, EventArgs e)
        {
            if (btn_excluded_categories.Text == "Editar")
            {
                txt_excluded_categories.Enabled = true;
                btn_excluded_categories.Text = "Aceptar";
                txt_excluded_categories.SelectionStart = txt_excluded_categories.Text.Length;
                txt_excluded_categories.SelectionLength = 0;
                txt_excluded_categories.Focus();
            }
            else if (btn_excluded_categories.Text == "Aceptar")
            {
                string txtCategories = txt_excluded_categories.Text.Replace(" ", "");
                string[] rubrosSeparados = txtCategories.Split(',');
                rubrosSeparados = rubrosSeparados.OrderBy(p => int.Parse(p)).ToArray();
                txt_excluded_categories.Text = string.Join(", ", rubrosSeparados);
                txt_excluded_categories.Enabled = false;
                btn_excluded_categories.Text = "Editar";
            }
        }

        private void btn_excluded_products_Click(object sender, EventArgs e)
        {
            if (btn_excluded_products.Text == "Editar")
            {
                txt_excluded_products.Enabled = true;
                btn_excluded_products.Text = "Aceptar";
                txt_excluded_products.SelectionStart = txt_excluded_products.Text.Length;
                txt_excluded_products.SelectionLength = 0;
                txt_excluded_products.Focus();
            }
            else if (btn_excluded_products.Text == "Aceptar")
            {
                string txtProducts = txt_excluded_products.Text.Replace(" ", "");
                string[] productosSeparados = txtProducts.Split(',');
                productosSeparados = productosSeparados.OrderBy(p => int.Parse(p)).ToArray();
                txt_excluded_products.Text = string.Join(", ", productosSeparados);
                txt_excluded_products.Enabled = false;
                btn_excluded_products.Text = "Editar";
            }
        }

        private void txt_excluded_categories_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado es un número, una coma o una tecla de control como borrar o pegar.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)32)
            {
                // Si el carácter ingresado no es un número, una coma o una tecla de control, se desecha.
                e.Handled = true;
            }
        }

        private void txt_excluded_products_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado es un número, una coma o una tecla de control como borrar o pegar.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)32)
            {
                // Si el carácter ingresado no es un número, una coma o una tecla de control, se desecha.
                e.Handled = true;
            }
        }

        private void btn_add_category_Click(object sender, EventArgs e)
        {
            FormAddCategory formAddCategory = new FormAddCategory(this);
            formAddCategory.ShowDialog();
        }

        public void RecieveNewCategory(string codigo, string nombre, string[] productos)
        {
            // Comprueba si el código del rubro ya existe en la tabla
            bool existeRubro = false;
            foreach (DataRow row in tablaRubros.Rows)
            {
                if (row["rubro"].ToString() == codigo)
                {
                    existeRubro = true;
                    break;
                }
            }

            // Si no existe, agrega el nuevo rubro
            if (existeRubro == false)
            {
                DataRow nuevaFila = tablaRubros.NewRow();

                // Asignar los valores a las columnas de la nueva fila
                nuevaFila["rubro"] = codigo;
                nuevaFila["descripcion"] = nombre;
                nuevaFila["productos"] = string.Join(", ", productos);

                // Agregar la nueva fila a tablaRubros
                tablaRubros.Rows.Add(nuevaFila);
                string nombreColumna = dataGridViewCategories.Columns[0].Name;
                OrdenarDataGridView(nombreColumna);
            }
            else
            {
                MessageBox.Show("El rubro ingresado ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RecieveEditedCategory(string codigo, string nombre, string[] productos)
        {
            foreach (DataRow row in tablaRubros.Rows)
            {
                if (row["rubro"].ToString() == codigo)
                {
                    row["descripcion"] = nombre;
                    row["productos"] = string.Join(", ", productos);
                }
            }
        }

        // Selecciona toda la fila al hacer click en cualquier celda del dataGridView
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btn_edit_category_Click(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en dataGridViewCategories
            if (dataGridViewCategories.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dataGridViewCategories.SelectedRows[0];

                // Obtener los valores de las celdas de la fila seleccionada y asignarlos a variables
                string codigo = filaSeleccionada.Cells["rubro"].Value.ToString();
                string nombre = filaSeleccionada.Cells["descripcion"].Value.ToString();
                string productos = filaSeleccionada.Cells["productos"].Value.ToString();

                FormAddCategory formAddCategory = new FormAddCategory(this);
                formAddCategory.Text = "Editar rubro";
                formAddCategory.RecieveCategory(codigo, nombre, productos);
                formAddCategory.ShowDialog();
            }
            else
            {
                // Si no hay filas seleccionadas, mostrar un mensaje de error
                MessageBox.Show("Por favor, seleccione una fila para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_category_Click(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en dataGridViewCategories
            if (dataGridViewCategories.SelectedRows.Count > 0)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar la fila seleccionada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Si el usuario elige "Sí", eliminar la fila seleccionada
                if (resultado == DialogResult.Yes)
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow filaSeleccionada = dataGridViewCategories.SelectedRows[0];

                    // Eliminar la fila del dataGridViewCategories
                    dataGridViewCategories.Rows.Remove(filaSeleccionada);
                }
            }
            else
            {
                // Si no hay filas seleccionadas, mostrar un mensaje de error
                MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_config_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("GeneralSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "filepath")
                        {
                            node.Attributes[1].Value = filepath;
                        }
                        if (node.Attributes[0].Value == "askReportLocation")
                        {
                            node.Attributes[1].Value = cb_report_save.Checked.ToString();
                        }
                        if (node.Attributes[0].Value == "excludedCategories")
                        {
                            node.Attributes[1].Value = txt_excluded_categories.Text;

                            // Obtener el texto del TextBox y separar los números por comas
                            string textoTextBox = txt_excluded_categories.Text;
                            string[] numerosSeparados = textoTextBox.Split(',');

                            // Iterar sobre los números separados
                            foreach (string numeroTexto in numerosSeparados)
                            {
                                // Convertir el número de texto a int
                                if (int.TryParse(numeroTexto.Trim(), out int numero))
                                {
                                    // Agregar el número convertido al HashSet
                                    numerosHashSet.Add(numero);
                                }
                                else
                                {
                                    // El número no es válido, puedes manejar esta situación según tus necesidades
                                    MessageBox.Show("Número no válido: " + numeroTexto, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        if (node.Attributes[0].Value == "excludedProducts")
                        {
                            node.Attributes[1].Value = txt_excluded_products.Text;

                            // Obtener el texto del TextBox y separar los números por comas
                            string textoTextBox = txt_excluded_products.Text;
                            string[] numerosSeparados = textoTextBox.Split(',');

                            // Iterar sobre los números separados
                            foreach (string numeroTexto in numerosSeparados)
                            {
                                // Convertir el número de texto a int
                                if (int.TryParse(numeroTexto.Trim(), out int numero))
                                {
                                    // Agregar el número convertido al HashSet
                                    numerosProductos.Add(numero);
                                }
                                else
                                {
                                    // El número no es válido, puedes manejar esta situación según tus necesidades
                                    MessageBox.Show("Número no válido: " + numeroTexto, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }

            var rubrosSection = xmlDoc.SelectSingleNode("//RubrosSection");

            if (rubrosSection != null)
            {
                // Limpiar los rubros existentes en la configuración
                rubrosSection.RemoveAll();

                // Agregar los rubros desde el DataGridView
                foreach (DataGridViewRow row in dataGridViewCategories.Rows)
                {
                    if (!row.IsNewRow) // Ignorar la fila de edición nueva
                    {
                        XmlElement rubroElement = xmlDoc.CreateElement("Rubro");
                        rubroElement.SetAttribute("codigo", row.Cells[0].Value.ToString());
                        rubroElement.SetAttribute("nombre", row.Cells[1].Value.ToString());

                        XmlElement productosElement = xmlDoc.CreateElement("productos");
                        string[] productos = row.Cells[2].Value.ToString().Split(',');

                        foreach (string productoCodigo in productos)
                        {
                            XmlElement productoElement = xmlDoc.CreateElement("producto");
                            productoElement.SetAttribute("codigo", productoCodigo.Trim());
                            productosElement.AppendChild(productoElement);
                        }

                        rubroElement.AppendChild(productosElement);
                        rubrosSection.AppendChild(rubroElement);
                    }
                }
            }

            foreach (DataGridViewColumn column in dataGridViewCategories.Columns)
            {
                tablaProductos.Columns.Add(column.HeaderText);
            }

            foreach (DataGridViewRow row in dataGridViewCategories.Rows)
            {
                DataRow filaNueva = tablaProductos.NewRow();

                for (int i = 0; i < dataGridViewCategories.Columns.Count; i++)
                {
                    filaNueva[i] = row.Cells[i].Value;
                }

                tablaProductos.Rows.Add(filaNueva);
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("GeneralSettings");
            ConfigurationManager.RefreshSection("RubrosSection");

            parentForm.ReceiveConfigData(filepath, cb_report_save.Checked, numerosHashSet, numerosProductos, tablaProductos);

            this.Close();
        }

        private void btn_config_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrdenarDataGridView(string columnName)
        {
            // Verifica si hay datos en el dataGridView
            if (dataGridViewCategories.DataSource is DataTable dataTable)
            {
                // Ordena el DataTable por la columna 0
                dataTable.DefaultView.Sort = columnName + " ASC";

                // Vuelve a asignar la fuente de datos al DataGridView
                dataGridViewCategories.DataSource = dataTable;
            }
        }
    }


    public class RubrosSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public RubroCollection Rubros
        {
            get { return (RubroCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    [ConfigurationCollection(typeof(RubroElement))]
    public class RubroCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RubroElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RubroElement)element).Codigo;
        }
    }

    public class RubroElement : ConfigurationElement
    {
        [ConfigurationProperty("codigo", IsKey = true, IsRequired = true)]
        public int Codigo
        {
            get { return (int)this["codigo"]; }
            set { this["codigo"] = value; }
        }

        [ConfigurationProperty("nombre", IsRequired = true)]
        public string Nombre
        {
            get { return (string)this["nombre"]; }
            set { this["nombre"] = value; }
        }

        [ConfigurationProperty("productos")]
        public ProductoCollection Productos
        {
            get { return (ProductoCollection)this["productos"]; }
        }
    }

    [ConfigurationCollection(typeof(ProductoElement))]
    public class ProductoCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProductoElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProductoElement)element).Codigo;
        }
    }

    public class ProductoElement : ConfigurationElement
    {
        [ConfigurationProperty("codigo", IsKey = true, IsRequired = true)]
        public int Codigo
        {
            get { return (int)this["codigo"]; }
            set { this["codigo"] = value; }
        }
    }
}
