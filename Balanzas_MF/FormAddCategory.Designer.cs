namespace Balanzas_MF
{
    partial class FormAddCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddCategory));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_category_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_category_cancel = new System.Windows.Forms.Button();
            this.btn_category_ok = new System.Windows.Forms.Button();
            this.txt_products = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.num_category = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_category)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(76, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(274, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "(Separar los códigos de los productos con una coma \",\")";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(201, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "(Escribir la descripción tal cual figura en el sistema)";
            // 
            // txt_category_name
            // 
            this.txt_category_name.Location = new System.Drawing.Point(241, 18);
            this.txt_category_name.Name = "txt_category_name";
            this.txt_category_name.Size = new System.Drawing.Size(205, 20);
            this.txt_category_name.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Descripción";
            // 
            // btn_category_cancel
            // 
            this.btn_category_cancel.Location = new System.Drawing.Point(288, 131);
            this.btn_category_cancel.Name = "btn_category_cancel";
            this.btn_category_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_category_cancel.TabIndex = 4;
            this.btn_category_cancel.Text = "Cancelar";
            this.btn_category_cancel.UseVisualStyleBackColor = true;
            this.btn_category_cancel.Click += new System.EventHandler(this.btn_category_cancel_Click);
            // 
            // btn_category_ok
            // 
            this.btn_category_ok.Location = new System.Drawing.Point(130, 131);
            this.btn_category_ok.Name = "btn_category_ok";
            this.btn_category_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_category_ok.TabIndex = 3;
            this.btn_category_ok.Text = "Aceptar";
            this.btn_category_ok.UseVisualStyleBackColor = true;
            this.btn_category_ok.Click += new System.EventHandler(this.btn_category_ok_Click);
            // 
            // txt_products
            // 
            this.txt_products.Location = new System.Drawing.Point(76, 70);
            this.txt_products.Name = "txt_products";
            this.txt_products.Size = new System.Drawing.Size(370, 20);
            this.txt_products.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Productos";
            // 
            // num_category
            // 
            this.num_category.Location = new System.Drawing.Point(57, 19);
            this.num_category.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_category.Name = "num_category";
            this.num_category.Size = new System.Drawing.Size(96, 20);
            this.num_category.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Rubro";
            // 
            // FormAddCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 168);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_category_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_category_cancel);
            this.Controls.Add(this.btn_category_ok);
            this.Controls.Add(this.txt_products);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.num_category);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddCategory";
            this.Text = "Agregar rubro";
            this.Load += new System.EventHandler(this.FormAddCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_category)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_category_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_category_cancel;
        private System.Windows.Forms.Button btn_category_ok;
        private System.Windows.Forms.TextBox txt_products;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_category;
        private System.Windows.Forms.Label label1;
    }
}