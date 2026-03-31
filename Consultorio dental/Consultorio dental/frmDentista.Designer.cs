namespace Consultorio_dental
{
    partial class frmDentista
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
            label1 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtEspecialidad = new TextBox();
            dgvDentistas = new DataGridView();
            btnActualizar = new Button();
            btnInsertar = new Button();
            btnEliminar = new Button();
            btnExportarPDF = new Button();
            btnExportarCSV = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDentistas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 24);
            label1.Name = "label1";
            label1.Size = new Size(82, 25);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(97, 24);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(150, 31);
            txtNombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 91);
            label2.Name = "label2";
            label2.Size = new Size(113, 25);
            label2.TabIndex = 0;
            label2.Text = "Especialidad:";
            // 
            // txtEspecialidad
            // 
            txtEspecialidad.Location = new Point(107, 88);
            txtEspecialidad.Name = "txtEspecialidad";
            txtEspecialidad.Size = new Size(150, 31);
            txtEspecialidad.TabIndex = 1;
            // 
            // dgvDentistas
            // 
            dgvDentistas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDentistas.Location = new Point(276, 12);
            dgvDentistas.Name = "dgvDentistas";
            dgvDentistas.RowHeadersWidth = 62;
            dgvDentistas.Size = new Size(512, 369);
            dgvDentistas.TabIndex = 2;
            dgvDentistas.SelectionChanged += dgvDentistas_SelectionChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(12, 200);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(112, 34);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnInsertar
            // 
            btnInsertar.Location = new Point(12, 160);
            btnInsertar.Name = "btnInsertar";
            btnInsertar.Size = new Size(112, 34);
            btnInsertar.TabIndex = 3;
            btnInsertar.Text = "Insertar";
            btnInsertar.UseVisualStyleBackColor = true;
            btnInsertar.Click += btnInsertar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(12, 240);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 34);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Location = new Point(130, 160);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(127, 34);
            btnExportarPDF.TabIndex = 3;
            btnExportarPDF.Text = "ExportarPDF";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.Location = new Point(132, 200);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new Size(125, 34);
            btnExportarCSV.TabIndex = 3;
            btnExportarCSV.Text = "ExportarCSV";
            btnExportarCSV.UseVisualStyleBackColor = true;
            btnExportarCSV.Click += btnExportarCSV_Click;
            // 
            // frmDentista
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnInsertar);
            Controls.Add(btnExportarCSV);
            Controls.Add(btnExportarPDF);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(dgvDentistas);
            Controls.Add(txtEspecialidad);
            Controls.Add(label2);
            Controls.Add(txtNombre);
            Controls.Add(label1);
            Name = "frmDentista";
            Text = "frmDentista";
            Load += frmDentista_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDentistas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNombre;
        private Label label2;
        private TextBox txtEspecialidad;
        private DataGridView dgvDentistas;
        private Button btnActualizar;
        private Button btnInsertar;
        private Button btnEliminar;
        private Button btnExportarPDF;
        private Button btnExportarCSV;
    }
}