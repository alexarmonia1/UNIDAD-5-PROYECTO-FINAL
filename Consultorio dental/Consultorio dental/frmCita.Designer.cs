namespace Consultorio_dental
{
    partial class frmCita
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
            btnInsertar = new Button();
            btnAtualizar = new Button();
            btnEliminar = new Button();
            btnExportar = new Button();
            dgvCitas = new DataGridView();
            btnExportarCsv = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbPaciente = new ComboBox();
            cmbDentista = new ComboBox();
            cmbMotivo = new ComboBox();
            dtpFecha = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).BeginInit();
            SuspendLayout();
            // 
            // btnInsertar
            // 
            btnInsertar.Location = new Point(359, 77);
            btnInsertar.Name = "btnInsertar";
            btnInsertar.Size = new Size(112, 34);
            btnInsertar.TabIndex = 0;
            btnInsertar.Text = "Insertar";
            btnInsertar.UseVisualStyleBackColor = true;
            btnInsertar.Click += btnInsertar_Click;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Location = new Point(359, 117);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(112, 34);
            btnAtualizar.TabIndex = 1;
            btnAtualizar.Text = "actualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(497, 77);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 34);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(497, 117);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(112, 34);
            btnExportar.TabIndex = 3;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // dgvCitas
            // 
            dgvCitas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitas.Location = new Point(12, 183);
            dgvCitas.Name = "dgvCitas";
            dgvCitas.RowHeadersWidth = 62;
            dgvCitas.Size = new Size(925, 325);
            dgvCitas.TabIndex = 4;
            // 
            // btnExportarCsv
            // 
            btnExportarCsv.Location = new Point(615, 100);
            btnExportarCsv.Name = "btnExportarCsv";
            btnExportarCsv.Size = new Size(140, 34);
            btnExportarCsv.TabIndex = 3;
            btnExportarCsv.Text = "Exportar csv";
            btnExportarCsv.UseVisualStyleBackColor = true;
            btnExportarCsv.Click += btnExportarCsv_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 22);
            label1.Name = "label1";
            label1.Size = new Size(76, 25);
            label1.TabIndex = 5;
            label1.Text = "Paciente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 61);
            label2.Name = "label2";
            label2.Size = new Size(77, 25);
            label2.TabIndex = 5;
            label2.Text = "Dentista";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 109);
            label3.Name = "label3";
            label3.Size = new Size(69, 25);
            label3.TabIndex = 5;
            label3.Text = "Motivo";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(359, 22);
            label4.Name = "label4";
            label4.Size = new Size(57, 25);
            label4.TabIndex = 5;
            label4.Text = "Fecha";
            // 
            // cmbPaciente
            // 
            cmbPaciente.FormattingEnabled = true;
            cmbPaciente.Location = new Point(110, 19);
            cmbPaciente.Name = "cmbPaciente";
            cmbPaciente.Size = new Size(182, 33);
            cmbPaciente.TabIndex = 6;
            // 
            // cmbDentista
            // 
            cmbDentista.FormattingEnabled = true;
            cmbDentista.Location = new Point(110, 61);
            cmbDentista.Name = "cmbDentista";
            cmbDentista.Size = new Size(182, 33);
            cmbDentista.TabIndex = 6;
            // 
            // cmbMotivo
            // 
            cmbMotivo.FormattingEnabled = true;
            cmbMotivo.Location = new Point(110, 109);
            cmbMotivo.Name = "cmbMotivo";
            cmbMotivo.Size = new Size(182, 33);
            cmbMotivo.TabIndex = 6;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(422, 22);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(156, 31);
            dtpFecha.TabIndex = 7;
            // 
            // frmCita
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 520);
            Controls.Add(dtpFecha);
            Controls.Add(cmbMotivo);
            Controls.Add(cmbDentista);
            Controls.Add(cmbPaciente);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvCitas);
            Controls.Add(btnExportarCsv);
            Controls.Add(btnExportar);
            Controls.Add(btnEliminar);
            Controls.Add(btnAtualizar);
            Controls.Add(btnInsertar);
            Name = "frmCita";
            Text = "frmCita";
            Load += frmCita_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCitas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnInsertar;
        private Button btnAtualizar;
        private Button btnEliminar;
        private Button btnExportar;
        private DataGridView dgvCitas;
        private Button btnExportarCsv;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cmbPaciente;
        private ComboBox cmbDentista;
        private ComboBox cmbMotivo;
        private DateTimePicker dtpFecha;
    }
}