namespace Balanzas_MF
{
    partial class FormConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.txt_excluded_products = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_excluded_products = new System.Windows.Forms.Button();
            this.btn_config_cancel = new System.Windows.Forms.Button();
            this.btn_save_config = new System.Windows.Forms.Button();
            this.btn_delete_category = new System.Windows.Forms.Button();
            this.btn_edit_category = new System.Windows.Forms.Button();
            this.txt_excluded_categories = new System.Windows.Forms.TextBox();
            this.btn_add_category = new System.Windows.Forms.Button();
            this.dataGridViewCategories = new System.Windows.Forms.DataGridView();
            this.btn_excluded_categories = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_edit_location = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_report_save = new System.Windows.Forms.CheckBox();
            this.txt_report_location = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCategories)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_excluded_products
            // 
            this.txt_excluded_products.Enabled = false;
            this.txt_excluded_products.Location = new System.Drawing.Point(6, 42);
            this.txt_excluded_products.Name = "txt_excluded_products";
            this.txt_excluded_products.Size = new System.Drawing.Size(402, 20);
            this.txt_excluded_products.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_excluded_products);
            this.groupBox4.Controls.Add(this.txt_excluded_products);
            this.groupBox4.Location = new System.Drawing.Point(12, 285);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(414, 112);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Productos excluidos";
            // 
            // btn_excluded_products
            // 
            this.btn_excluded_products.Location = new System.Drawing.Point(167, 68);
            this.btn_excluded_products.Name = "btn_excluded_products";
            this.btn_excluded_products.Size = new System.Drawing.Size(75, 23);
            this.btn_excluded_products.TabIndex = 6;
            this.btn_excluded_products.Text = "Editar";
            this.btn_excluded_products.UseVisualStyleBackColor = true;
            this.btn_excluded_products.Click += new System.EventHandler(this.btn_excluded_products_Click);
            // 
            // btn_config_cancel
            // 
            this.btn_config_cancel.Location = new System.Drawing.Point(275, 667);
            this.btn_config_cancel.Name = "btn_config_cancel";
            this.btn_config_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_config_cancel.TabIndex = 12;
            this.btn_config_cancel.Text = "Cancelar";
            this.btn_config_cancel.UseVisualStyleBackColor = true;
            this.btn_config_cancel.Click += new System.EventHandler(this.btn_config_cancel_Click);
            // 
            // btn_save_config
            // 
            this.btn_save_config.Location = new System.Drawing.Point(87, 667);
            this.btn_save_config.Name = "btn_save_config";
            this.btn_save_config.Size = new System.Drawing.Size(75, 23);
            this.btn_save_config.TabIndex = 11;
            this.btn_save_config.Text = "Guardar";
            this.btn_save_config.UseVisualStyleBackColor = true;
            this.btn_save_config.Click += new System.EventHandler(this.btn_save_config_Click);
            // 
            // btn_delete_category
            // 
            this.btn_delete_category.Location = new System.Drawing.Point(293, 180);
            this.btn_delete_category.Name = "btn_delete_category";
            this.btn_delete_category.Size = new System.Drawing.Size(75, 23);
            this.btn_delete_category.TabIndex = 10;
            this.btn_delete_category.Text = "Eliminar";
            this.btn_delete_category.UseVisualStyleBackColor = true;
            this.btn_delete_category.Click += new System.EventHandler(this.btn_delete_category_Click);
            // 
            // btn_edit_category
            // 
            this.btn_edit_category.Location = new System.Drawing.Point(167, 180);
            this.btn_edit_category.Name = "btn_edit_category";
            this.btn_edit_category.Size = new System.Drawing.Size(75, 23);
            this.btn_edit_category.TabIndex = 9;
            this.btn_edit_category.Text = "Editar...";
            this.btn_edit_category.UseVisualStyleBackColor = true;
            this.btn_edit_category.Click += new System.EventHandler(this.btn_edit_category_Click);
            // 
            // txt_excluded_categories
            // 
            this.txt_excluded_categories.Enabled = false;
            this.txt_excluded_categories.Location = new System.Drawing.Point(6, 42);
            this.txt_excluded_categories.Name = "txt_excluded_categories";
            this.txt_excluded_categories.Size = new System.Drawing.Size(402, 20);
            this.txt_excluded_categories.TabIndex = 3;
            // 
            // btn_add_category
            // 
            this.btn_add_category.Location = new System.Drawing.Point(42, 180);
            this.btn_add_category.Name = "btn_add_category";
            this.btn_add_category.Size = new System.Drawing.Size(75, 23);
            this.btn_add_category.TabIndex = 8;
            this.btn_add_category.Text = "Agregar...";
            this.btn_add_category.UseVisualStyleBackColor = true;
            this.btn_add_category.Click += new System.EventHandler(this.btn_add_category_Click);
            // 
            // dataGridViewCategories
            // 
            this.dataGridViewCategories.AllowUserToAddRows = false;
            this.dataGridViewCategories.AllowUserToDeleteRows = false;
            this.dataGridViewCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCategories.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewCategories.Name = "dataGridViewCategories";
            this.dataGridViewCategories.ReadOnly = true;
            this.dataGridViewCategories.Size = new System.Drawing.Size(402, 135);
            this.dataGridViewCategories.TabIndex = 7;
            // 
            // btn_excluded_categories
            // 
            this.btn_excluded_categories.Location = new System.Drawing.Point(167, 68);
            this.btn_excluded_categories.Name = "btn_excluded_categories";
            this.btn_excluded_categories.Size = new System.Drawing.Size(75, 23);
            this.btn_excluded_categories.TabIndex = 4;
            this.btn_excluded_categories.Text = "Editar";
            this.btn_excluded_categories.UseVisualStyleBackColor = true;
            this.btn_excluded_categories.Click += new System.EventHandler(this.btn_excluded_categories_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_excluded_categories);
            this.groupBox2.Controls.Add(this.txt_excluded_categories);
            this.groupBox2.Location = new System.Drawing.Point(12, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 112);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rubros excluidos";
            // 
            // btn_edit_location
            // 
            this.btn_edit_location.Location = new System.Drawing.Point(167, 53);
            this.btn_edit_location.Name = "btn_edit_location";
            this.btn_edit_location.Size = new System.Drawing.Size(75, 23);
            this.btn_edit_location.TabIndex = 1;
            this.btn_edit_location.Text = "Cambiar...";
            this.btn_edit_location.UseVisualStyleBackColor = true;
            this.btn_edit_location.Click += new System.EventHandler(this.btn_edit_location_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_edit_location);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_report_save);
            this.groupBox1.Controls.Add(this.txt_report_location);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 121);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reportes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ubicación de guardado de reportes:";
            // 
            // cb_report_save
            // 
            this.cb_report_save.AutoSize = true;
            this.cb_report_save.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_report_save.Location = new System.Drawing.Point(9, 89);
            this.cb_report_save.Name = "cb_report_save";
            this.cb_report_save.Size = new System.Drawing.Size(231, 17);
            this.cb_report_save.TabIndex = 2;
            this.cb_report_save.Text = "Siempre preguntar dónde guardar el reporte";
            this.cb_report_save.UseVisualStyleBackColor = true;
            // 
            // txt_report_location
            // 
            this.txt_report_location.Location = new System.Drawing.Point(189, 27);
            this.txt_report_location.Name = "txt_report_location";
            this.txt_report_location.ReadOnly = true;
            this.txt_report_location.Size = new System.Drawing.Size(219, 20);
            this.txt_report_location.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_delete_category);
            this.groupBox3.Controls.Add(this.btn_edit_category);
            this.groupBox3.Controls.Add(this.btn_add_category);
            this.groupBox3.Controls.Add(this.dataGridViewCategories);
            this.groupBox3.Location = new System.Drawing.Point(12, 415);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(414, 225);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rubros y productos";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 713);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btn_config_cancel);
            this.Controls.Add(this.btn_save_config);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfig";
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCategories)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_excluded_products;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_excluded_products;
        private System.Windows.Forms.Button btn_config_cancel;
        private System.Windows.Forms.Button btn_save_config;
        private System.Windows.Forms.Button btn_delete_category;
        private System.Windows.Forms.Button btn_edit_category;
        private System.Windows.Forms.TextBox txt_excluded_categories;
        private System.Windows.Forms.Button btn_add_category;
        private System.Windows.Forms.DataGridView dataGridViewCategories;
        private System.Windows.Forms.Button btn_excluded_categories;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_edit_location;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cb_report_save;
        private System.Windows.Forms.TextBox txt_report_location;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}