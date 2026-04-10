namespace sistema_de_manejo_de_empleados
{
    partial class empleados
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
            this.btnInsertar = new System.Windows.Forms.Button();
            this.btnExportarCSV = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCargos = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.numSalario = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDepartamentos = new System.Windows.Forms.ComboBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numSalario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsertar
            // 
            this.btnInsertar.Location = new System.Drawing.Point(12, 317);
            this.btnInsertar.Name = "btnInsertar";
            this.btnInsertar.Size = new System.Drawing.Size(75, 39);
            this.btnInsertar.TabIndex = 16;
            this.btnInsertar.Text = "Insertar";
            this.btnInsertar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsertar.UseVisualStyleBackColor = true;
            this.btnInsertar.Click += new System.EventHandler(this.btnInsertar_Click);
            // 
            // btnExportarCSV
            // 
            this.btnExportarCSV.Location = new System.Drawing.Point(171, 362);
            this.btnExportarCSV.Name = "btnExportarCSV";
            this.btnExportarCSV.Size = new System.Drawing.Size(146, 39);
            this.btnExportarCSV.TabIndex = 12;
            this.btnExportarCSV.Text = "ExportarCSV";
            this.btnExportarCSV.UseVisualStyleBackColor = true;
            this.btnExportarCSV.Click += new System.EventHandler(this.btnExportarCSV_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(197, 317);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(108, 39);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Location = new System.Drawing.Point(5, 362);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(139, 39);
            this.btnExportarPDF.TabIndex = 14;
            this.btnExportarPDF.Text = "ExportarPDF";
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(93, 317);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(98, 39);
            this.btnActualizar.TabIndex = 15;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(135, 75);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 26);
            this.txtNombre.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Cargo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Estado";
            // 
            // cmbCargos
            // 
            this.cmbCargos.FormattingEnabled = true;
            this.cmbCargos.Location = new System.Drawing.Point(135, 151);
            this.cmbCargos.Name = "cmbCargos";
            this.cmbCargos.Size = new System.Drawing.Size(121, 28);
            this.cmbCargos.TabIndex = 19;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(135, 193);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 28);
            this.cmbEstado.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Fecha inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(135, 233);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 26);
            this.dtpFechaInicio.TabIndex = 20;
            // 
            // numSalario
            // 
            this.numSalario.Location = new System.Drawing.Point(135, 270);
            this.numSalario.Name = "numSalario";
            this.numSalario.Size = new System.Drawing.Size(120, 26);
            this.numSalario.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Salario";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Departamentos";
            // 
            // cmbDepartamentos
            // 
            this.cmbDepartamentos.FormattingEnabled = true;
            this.cmbDepartamentos.Location = new System.Drawing.Point(135, 110);
            this.cmbDepartamentos.Name = "cmbDepartamentos";
            this.cmbDepartamentos.Size = new System.Drawing.Size(121, 28);
            this.cmbDepartamentos.TabIndex = 19;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Location = new System.Drawing.Point(353, 24);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.RowHeadersWidth = 62;
            this.dgvEmpleados.RowTemplate.Height = 28;
            this.dgvEmpleados.Size = new System.Drawing.Size(857, 467);
            this.dgvEmpleados.TabIndex = 22;
            this.dgvEmpleados.SelectionChanged += new System.EventHandler(this.dgvEmpleados_SelectionChanged);
            // 
            // empleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 513);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.numSalario);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.cmbDepartamentos);
            this.Controls.Add(this.cmbCargos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsertar);
            this.Controls.Add(this.btnExportarCSV);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.btnActualizar);
            this.Name = "empleados";
            this.Text = "empleados";
            this.Load += new System.EventHandler(this.empleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSalario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsertar;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCargos;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.NumericUpDown numSalario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDepartamentos;
        private System.Windows.Forms.DataGridView dgvEmpleados;
    }
}