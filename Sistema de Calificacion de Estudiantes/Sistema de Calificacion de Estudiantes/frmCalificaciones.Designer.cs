namespace Sistema_de_Calificacion_de_Estudiantes
{
    partial class frmCalificaciones
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEstudiantes = new System.Windows.Forms.ComboBox();
            this.cmbMaterias = new System.Windows.Forms.ComboBox();
            this.numC1 = new System.Windows.Forms.NumericUpDown();
            this.numC2 = new System.Windows.Forms.NumericUpDown();
            this.numC3 = new System.Windows.Forms.NumericUpDown();
            this.numC4 = new System.Windows.Forms.NumericUpDown();
            this.numExamen = new System.Windows.Forms.NumericUpDown();
            this.btnInsertar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.btnExportarCSV = new System.Windows.Forms.Button();
            this.dgvCalificaciones = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numC1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExamen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalificaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estudiante";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Calificación 1";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Calificación 2";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Materia";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Calificación 3";
            this.label5.Click += new System.EventHandler(this.label3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Calificación 4";
            this.label6.Click += new System.EventHandler(this.label3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Examen";
            this.label7.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmbEstudiantes
            // 
            this.cmbEstudiantes.FormattingEnabled = true;
            this.cmbEstudiantes.Location = new System.Drawing.Point(139, 37);
            this.cmbEstudiantes.Name = "cmbEstudiantes";
            this.cmbEstudiantes.Size = new System.Drawing.Size(121, 28);
            this.cmbEstudiantes.TabIndex = 1;
            // 
            // cmbMaterias
            // 
            this.cmbMaterias.FormattingEnabled = true;
            this.cmbMaterias.Location = new System.Drawing.Point(139, 92);
            this.cmbMaterias.Name = "cmbMaterias";
            this.cmbMaterias.Size = new System.Drawing.Size(121, 28);
            this.cmbMaterias.TabIndex = 1;
            // 
            // numC1
            // 
            this.numC1.Location = new System.Drawing.Point(139, 140);
            this.numC1.Name = "numC1";
            this.numC1.Size = new System.Drawing.Size(120, 26);
            this.numC1.TabIndex = 2;
            // 
            // numC2
            // 
            this.numC2.Location = new System.Drawing.Point(139, 184);
            this.numC2.Name = "numC2";
            this.numC2.Size = new System.Drawing.Size(120, 26);
            this.numC2.TabIndex = 2;
            // 
            // numC3
            // 
            this.numC3.Location = new System.Drawing.Point(139, 233);
            this.numC3.Name = "numC3";
            this.numC3.Size = new System.Drawing.Size(120, 26);
            this.numC3.TabIndex = 2;
            // 
            // numC4
            // 
            this.numC4.Location = new System.Drawing.Point(139, 273);
            this.numC4.Name = "numC4";
            this.numC4.Size = new System.Drawing.Size(120, 26);
            this.numC4.TabIndex = 2;
            // 
            // numExamen
            // 
            this.numExamen.Location = new System.Drawing.Point(139, 314);
            this.numExamen.Name = "numExamen";
            this.numExamen.Size = new System.Drawing.Size(120, 26);
            this.numExamen.TabIndex = 2;
            // 
            // btnInsertar
            // 
            this.btnInsertar.Location = new System.Drawing.Point(54, 357);
            this.btnInsertar.Name = "btnInsertar";
            this.btnInsertar.Size = new System.Drawing.Size(75, 34);
            this.btnInsertar.TabIndex = 3;
            this.btnInsertar.Text = "Insertar";
            this.btnInsertar.UseVisualStyleBackColor = true;
            this.btnInsertar.Click += new System.EventHandler(this.btnInsertar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(139, 357);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 34);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(54, 397);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(79, 34);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Location = new System.Drawing.Point(54, 437);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(126, 34);
            this.btnExportarPDF.TabIndex = 3;
            this.btnExportarPDF.Text = "ExportarPDF";
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnExportarCSV
            // 
            this.btnExportarCSV.Location = new System.Drawing.Point(54, 477);
            this.btnExportarCSV.Name = "btnExportarCSV";
            this.btnExportarCSV.Size = new System.Drawing.Size(126, 34);
            this.btnExportarCSV.TabIndex = 3;
            this.btnExportarCSV.Text = "ExportarCSV";
            this.btnExportarCSV.UseVisualStyleBackColor = true;
            this.btnExportarCSV.Click += new System.EventHandler(this.btnExportarCSV_Click);
            // 
            // dgvCalificaciones
            // 
            this.dgvCalificaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalificaciones.Location = new System.Drawing.Point(319, 23);
            this.dgvCalificaciones.Name = "dgvCalificaciones";
            this.dgvCalificaciones.RowHeadersWidth = 62;
            this.dgvCalificaciones.RowTemplate.Height = 28;
            this.dgvCalificaciones.Size = new System.Drawing.Size(954, 511);
            this.dgvCalificaciones.TabIndex = 4;
            this.dgvCalificaciones.SelectionChanged += new System.EventHandler(this.dgvCalificaciones_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "limpiar campos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCalificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 546);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvCalificaciones);
            this.Controls.Add(this.btnExportarCSV);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnInsertar);
            this.Controls.Add(this.numExamen);
            this.Controls.Add(this.numC4);
            this.Controls.Add(this.numC3);
            this.Controls.Add(this.numC2);
            this.Controls.Add(this.numC1);
            this.Controls.Add(this.cmbMaterias);
            this.Controls.Add(this.cmbEstudiantes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCalificaciones";
            this.Text = "frmCalificaciones";
            this.Load += new System.EventHandler(this.frmCalificaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numC1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExamen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalificaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEstudiantes;
        private System.Windows.Forms.ComboBox cmbMaterias;
        private System.Windows.Forms.NumericUpDown numC1;
        private System.Windows.Forms.NumericUpDown numC2;
        private System.Windows.Forms.NumericUpDown numC3;
        private System.Windows.Forms.NumericUpDown numC4;
        private System.Windows.Forms.NumericUpDown numExamen;
        private System.Windows.Forms.Button btnInsertar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.DataGridView dgvCalificaciones;
        private System.Windows.Forms.Button button1;
    }
}