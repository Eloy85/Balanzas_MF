using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Balanzas_MF
{
    public partial class FormAddCategory : Form
    {
        private FormConfig formConfig;
        public FormAddCategory(FormConfig parent)
        {
            InitializeComponent();
            formConfig = parent;
        }

        private void FormAddCategory_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            if (num_category.Enabled == true)
            {
                num_category.Select(0, num_category.Text.Length);
            }
        }

        private void btn_category_ok_Click(object sender, EventArgs e)
        {
            string codigo = num_category.Text;
            string nombre = txt_category_name.Text;
            string productos = txt_products.Text.Replace(" ", "");
            string[] productosSeparados = productos.Split(',');
            productosSeparados = productosSeparados.OrderBy(p => int.Parse(p)).ToArray();
            if (Text == "Agregar rubro")
            {
                formConfig.RecieveNewCategory(codigo, nombre, productosSeparados);
            }
            if (Text == "Editar rubro")
            {
                formConfig.RecieveEditedCategory(codigo, nombre, productosSeparados);
            }
            
            this.Close();
        }

        private void btn_category_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void RecieveCategory(string codigo, string nombre, string productos)
        {
            num_category.Text = codigo;
            num_category.Enabled = false;
            txt_category_name.Text = nombre;
            txt_products.Text = productos;
        }
    }
}
