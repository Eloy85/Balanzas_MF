namespace Balanzas_MF
{
    partial class FormErrores
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
            this.label1 = new System.Windows.Forms.Label();
            this.num_code = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.num_amount = new System.Windows.Forms.NumericUpDown();
            this.btn_add_error = new System.Windows.Forms.Button();
            this.dataGridViewErrors = new System.Windows.Forms.DataGridView();
            this.btn_errors_ok = new System.Windows.Forms.Button();
            this.btn_errors_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código:";
            // 
            // num_code
            // 
            this.num_code.Location = new System.Drawing.Point(123, 44);
            this.num_code.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_code.Name = "num_code";
            this.num_code.Size = new System.Drawing.Size(120, 20);
            this.num_code.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto:";
            // 
            // num_amount
            // 
            this.num_amount.Location = new System.Drawing.Point(446, 44);
            this.num_amount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_amount.Name = "num_amount";
            this.num_amount.Size = new System.Drawing.Size(120, 20);
            this.num_amount.TabIndex = 3;
            // 
            // btn_add_error
            // 
            this.btn_add_error.Location = new System.Drawing.Point(283, 93);
            this.btn_add_error.Name = "btn_add_error";
            this.btn_add_error.Size = new System.Drawing.Size(75, 23);
            this.btn_add_error.TabIndex = 4;
            this.btn_add_error.Text = "Agregar";
            this.btn_add_error.UseVisualStyleBackColor = true;
            this.btn_add_error.Click += new System.EventHandler(this.btn_add_error_Click);
            // 
            // dataGridViewErrors
            // 
            this.dataGridViewErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewErrors.Location = new System.Drawing.Point(77, 145);
            this.dataGridViewErrors.Name = "dataGridViewErrors";
            this.dataGridViewErrors.Size = new System.Drawing.Size(489, 126);
            this.dataGridViewErrors.TabIndex = 5;
            // 
            // btn_errors_ok
            // 
            this.btn_errors_ok.Location = new System.Drawing.Point(123, 310);
            this.btn_errors_ok.Name = "btn_errors_ok";
            this.btn_errors_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_errors_ok.TabIndex = 6;
            this.btn_errors_ok.Text = "Aceptar";
            this.btn_errors_ok.UseVisualStyleBackColor = true;
            this.btn_errors_ok.Click += new System.EventHandler(this.btn_errors_ok_Click);
            // 
            // btn_errors_cancel
            // 
            this.btn_errors_cancel.Location = new System.Drawing.Point(446, 310);
            this.btn_errors_cancel.Name = "btn_errors_cancel";
            this.btn_errors_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_errors_cancel.TabIndex = 7;
            this.btn_errors_cancel.Text = "Cancelar";
            this.btn_errors_cancel.UseVisualStyleBackColor = true;
            this.btn_errors_cancel.Click += new System.EventHandler(this.btn_errors_cancel_Click);
            // 
            // FormErrores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 381);
            this.Controls.Add(this.btn_errors_cancel);
            this.Controls.Add(this.btn_errors_ok);
            this.Controls.Add(this.dataGridViewErrors);
            this.Controls.Add(this.btn_add_error);
            this.Controls.Add(this.num_amount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.num_code);
            this.Controls.Add(this.label1);
            this.Name = "FormErrores";
            this.Text = "FormErrores";
            this.Load += new System.EventHandler(this.FormErrores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_amount;
        private System.Windows.Forms.Button btn_add_error;
        private System.Windows.Forms.DataGridView dataGridViewErrors;
        private System.Windows.Forms.Button btn_errors_ok;
        private System.Windows.Forms.Button btn_errors_cancel;
    }
}