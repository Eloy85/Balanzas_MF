﻿namespace Balanzas_MF
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btn_load_sales = new System.Windows.Forms.Button();
            this.btn_load_errors = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_load_bal5 = new System.Windows.Forms.Button();
            this.btn_load_bal4 = new System.Windows.Forms.Button();
            this.btn_load_bal3 = new System.Windows.Forms.Button();
            this.btn_load_bal2 = new System.Windows.Forms.Button();
            this.btn_load_bal1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_process_data = new System.Windows.Forms.Button();
            this.btn_clean_fields = new System.Windows.Forms.Button();
            this.btn_print_report = new System.Windows.Forms.Button();
            this.label_sales = new System.Windows.Forms.Label();
            this.label_errors = new System.Windows.Forms.Label();
            this.label_bal1 = new System.Windows.Forms.Label();
            this.label_bal2 = new System.Windows.Forms.Label();
            this.label_bal3 = new System.Windows.Forms.Label();
            this.label_bal4 = new System.Windows.Forms.Label();
            this.label_bal5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Control balanzas";
            // 
            // btn_load_sales
            // 
            this.btn_load_sales.Location = new System.Drawing.Point(114, 88);
            this.btn_load_sales.Name = "btn_load_sales";
            this.btn_load_sales.Size = new System.Drawing.Size(127, 23);
            this.btn_load_sales.TabIndex = 1;
            this.btn_load_sales.Text = "Cargar ventas UltraNet";
            this.btn_load_sales.UseVisualStyleBackColor = true;
            this.btn_load_sales.Click += new System.EventHandler(this.btn_load_sales_Click);
            // 
            // btn_load_errors
            // 
            this.btn_load_errors.Location = new System.Drawing.Point(569, 88);
            this.btn_load_errors.Name = "btn_load_errors";
            this.btn_load_errors.Size = new System.Drawing.Size(101, 23);
            this.btn_load_errors.TabIndex = 2;
            this.btn_load_errors.Text = "Cargar errores";
            this.btn_load_errors.UseVisualStyleBackColor = true;
            this.btn_load_errors.Click += new System.EventHandler(this.btn_load_errors_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_bal5);
            this.groupBox1.Controls.Add(this.label_bal4);
            this.groupBox1.Controls.Add(this.label_bal3);
            this.groupBox1.Controls.Add(this.label_bal2);
            this.groupBox1.Controls.Add(this.label_bal1);
            this.groupBox1.Controls.Add(this.btn_load_bal5);
            this.groupBox1.Controls.Add(this.btn_load_bal4);
            this.groupBox1.Controls.Add(this.btn_load_bal3);
            this.groupBox1.Controls.Add(this.btn_load_bal2);
            this.groupBox1.Controls.Add(this.btn_load_bal1);
            this.groupBox1.Location = new System.Drawing.Point(114, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cargar ventas Qendra";
            // 
            // btn_load_bal5
            // 
            this.btn_load_bal5.Location = new System.Drawing.Point(476, 30);
            this.btn_load_bal5.Name = "btn_load_bal5";
            this.btn_load_bal5.Size = new System.Drawing.Size(75, 23);
            this.btn_load_bal5.TabIndex = 4;
            this.btn_load_bal5.Text = "Balanza 5";
            this.btn_load_bal5.UseVisualStyleBackColor = true;
            this.btn_load_bal5.Click += new System.EventHandler(this.btn_load_bal5_Click);
            // 
            // btn_load_bal4
            // 
            this.btn_load_bal4.Location = new System.Drawing.Point(360, 30);
            this.btn_load_bal4.Name = "btn_load_bal4";
            this.btn_load_bal4.Size = new System.Drawing.Size(75, 23);
            this.btn_load_bal4.TabIndex = 3;
            this.btn_load_bal4.Text = "Balanza 4";
            this.btn_load_bal4.UseVisualStyleBackColor = true;
            this.btn_load_bal4.Click += new System.EventHandler(this.btn_load_bal4_Click);
            // 
            // btn_load_bal3
            // 
            this.btn_load_bal3.Location = new System.Drawing.Point(244, 30);
            this.btn_load_bal3.Name = "btn_load_bal3";
            this.btn_load_bal3.Size = new System.Drawing.Size(75, 23);
            this.btn_load_bal3.TabIndex = 2;
            this.btn_load_bal3.Text = "Balanza 3";
            this.btn_load_bal3.UseVisualStyleBackColor = true;
            this.btn_load_bal3.Click += new System.EventHandler(this.btn_load_bal3_Click);
            // 
            // btn_load_bal2
            // 
            this.btn_load_bal2.Location = new System.Drawing.Point(125, 30);
            this.btn_load_bal2.Name = "btn_load_bal2";
            this.btn_load_bal2.Size = new System.Drawing.Size(75, 23);
            this.btn_load_bal2.TabIndex = 1;
            this.btn_load_bal2.Text = "Balanza 2";
            this.btn_load_bal2.UseVisualStyleBackColor = true;
            this.btn_load_bal2.Click += new System.EventHandler(this.btn_load_bal2_Click);
            // 
            // btn_load_bal1
            // 
            this.btn_load_bal1.Location = new System.Drawing.Point(7, 30);
            this.btn_load_bal1.Name = "btn_load_bal1";
            this.btn_load_bal1.Size = new System.Drawing.Size(75, 23);
            this.btn_load_bal1.TabIndex = 0;
            this.btn_load_bal1.Text = "Balanza 1";
            this.btn_load_bal1.UseVisualStyleBackColor = true;
            this.btn_load_bal1.Click += new System.EventHandler(this.btn_load_bal1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(114, 323);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(556, 164);
            this.dataGridView1.TabIndex = 4;
            // 
            // btn_process_data
            // 
            this.btn_process_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_process_data.Location = new System.Drawing.Point(358, 279);
            this.btn_process_data.Name = "btn_process_data";
            this.btn_process_data.Size = new System.Drawing.Size(75, 23);
            this.btn_process_data.TabIndex = 5;
            this.btn_process_data.Text = "Procesar";
            this.btn_process_data.UseVisualStyleBackColor = true;
            this.btn_process_data.Click += new System.EventHandler(this.btn_process_data_Click);
            // 
            // btn_clean_fields
            // 
            this.btn_clean_fields.Location = new System.Drawing.Point(239, 522);
            this.btn_clean_fields.Name = "btn_clean_fields";
            this.btn_clean_fields.Size = new System.Drawing.Size(90, 23);
            this.btn_clean_fields.TabIndex = 6;
            this.btn_clean_fields.Text = "Limpiar campos";
            this.btn_clean_fields.UseVisualStyleBackColor = true;
            this.btn_clean_fields.Click += new System.EventHandler(this.btn_clean_fields_Click);
            // 
            // btn_print_report
            // 
            this.btn_print_report.Location = new System.Drawing.Point(461, 522);
            this.btn_print_report.Name = "btn_print_report";
            this.btn_print_report.Size = new System.Drawing.Size(88, 23);
            this.btn_print_report.TabIndex = 7;
            this.btn_print_report.Text = "Imprimir reporte";
            this.btn_print_report.UseVisualStyleBackColor = true;
            this.btn_print_report.Click += new System.EventHandler(this.btn_print_report_Click);
            // 
            // label_sales
            // 
            this.label_sales.AutoSize = true;
            this.label_sales.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_sales.Location = new System.Drawing.Point(153, 114);
            this.label_sales.Name = "label_sales";
            this.label_sales.Size = new System.Drawing.Size(47, 13);
            this.label_sales.TabIndex = 8;
            this.label_sales.Text = "Cargado";
            // 
            // label_errors
            // 
            this.label_errors.AutoSize = true;
            this.label_errors.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_errors.Location = new System.Drawing.Point(598, 114);
            this.label_errors.Name = "label_errors";
            this.label_errors.Size = new System.Drawing.Size(47, 13);
            this.label_errors.TabIndex = 9;
            this.label_errors.Text = "Cargado";
            // 
            // label_bal1
            // 
            this.label_bal1.AutoSize = true;
            this.label_bal1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_bal1.Location = new System.Drawing.Point(22, 56);
            this.label_bal1.Name = "label_bal1";
            this.label_bal1.Size = new System.Drawing.Size(47, 13);
            this.label_bal1.TabIndex = 10;
            this.label_bal1.Text = "Cargado";
            // 
            // label_bal2
            // 
            this.label_bal2.AutoSize = true;
            this.label_bal2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_bal2.Location = new System.Drawing.Point(140, 56);
            this.label_bal2.Name = "label_bal2";
            this.label_bal2.Size = new System.Drawing.Size(47, 13);
            this.label_bal2.TabIndex = 11;
            this.label_bal2.Text = "Cargado";
            // 
            // label_bal3
            // 
            this.label_bal3.AutoSize = true;
            this.label_bal3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_bal3.Location = new System.Drawing.Point(259, 56);
            this.label_bal3.Name = "label_bal3";
            this.label_bal3.Size = new System.Drawing.Size(47, 13);
            this.label_bal3.TabIndex = 12;
            this.label_bal3.Text = "Cargado";
            // 
            // label_bal4
            // 
            this.label_bal4.AutoSize = true;
            this.label_bal4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_bal4.Location = new System.Drawing.Point(375, 56);
            this.label_bal4.Name = "label_bal4";
            this.label_bal4.Size = new System.Drawing.Size(47, 13);
            this.label_bal4.TabIndex = 13;
            this.label_bal4.Text = "Cargado";
            // 
            // label_bal5
            // 
            this.label_bal5.AutoSize = true;
            this.label_bal5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_bal5.Location = new System.Drawing.Point(490, 56);
            this.label_bal5.Name = "label_bal5";
            this.label_bal5.Size = new System.Drawing.Size(47, 13);
            this.label_bal5.TabIndex = 14;
            this.label_bal5.Text = "Cargado";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 571);
            this.Controls.Add(this.label_errors);
            this.Controls.Add(this.label_sales);
            this.Controls.Add(this.btn_print_report);
            this.Controls.Add(this.btn_clean_fields);
            this.Controls.Add(this.btn_process_data);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_load_errors);
            this.Controls.Add(this.btn_load_sales);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_load_sales;
        private System.Windows.Forms.Button btn_load_errors;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_load_bal5;
        private System.Windows.Forms.Button btn_load_bal4;
        private System.Windows.Forms.Button btn_load_bal3;
        private System.Windows.Forms.Button btn_load_bal2;
        private System.Windows.Forms.Button btn_load_bal1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_process_data;
        private System.Windows.Forms.Button btn_clean_fields;
        private System.Windows.Forms.Button btn_print_report;
        private System.Windows.Forms.Label label_bal5;
        private System.Windows.Forms.Label label_bal4;
        private System.Windows.Forms.Label label_bal3;
        private System.Windows.Forms.Label label_bal2;
        private System.Windows.Forms.Label label_bal1;
        private System.Windows.Forms.Label label_sales;
        private System.Windows.Forms.Label label_errors;
    }
}
