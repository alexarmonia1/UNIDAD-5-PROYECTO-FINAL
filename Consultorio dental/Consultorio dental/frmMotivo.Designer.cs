namespace Consultorio_dental
{
    partial class frmMotivo
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
            txtDescripcion = new TextBox();
            dgvMotivos = new DataGridView();
            btnInsertar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnExportarPDF = new Button();
            btnExportarCSV = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMotivos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(108, 25);
            label1.TabIndex = 0;
            label1.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(122, 49);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(150, 31);
            txtDescripcion.TabIndex = 1;
            // 
            // dgvMotivos
            // 
            dgvMotivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMotivos.Location = new Point(39, 158);
            dgvMotivos.Name = "dgvMotivos";
            dgvMotivos.RowHeadersWidth = 62;
            dgvMotivos.Size = new Size(568, 280);
            dgvMotivos.TabIndex = 2;
            dgvMotivos.SelectionChanged += dgvMotivos_SelectionChanged;
            // 
            // btnInsertar
            // 
            btnInsertar.Location = new Point(39, 109);
            btnInsertar.Name = "btnInsertar";
            btnInsertar.Size = new Size(112, 34);
            btnInsertar.TabIndex = 3;
            btnInsertar.Text = "Insertar";
            btnInsertar.UseVisualStyleBackColor = true;
            btnInsertar.Click += btnInsertar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(160, 109);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(112, 34);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(278, 109);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 34);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Location = new Point(396, 109);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(140, 34);
            btnExportarPDF.TabIndex = 3;
            btnExportarPDF.Text = "Exportar Pdf";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.Location = new Point(542, 109);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new Size(150, 34);
            btnExportarCSV.TabIndex = 3;
            btnExportarCSV.Text = "Exportar csv";
            btnExportarCSV.UseVisualStyleBackColor = true;
            btnExportarCSV.Click += btnExportarCSV_Click;
            // 
            // frmMotivo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExportarCSV);
            Controls.Add(btnExportarPDF);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnInsertar);
            Controls.Add(dgvMotivos);
            Controls.Add(txtDescripcion);
            Controls.Add(label1);
            Name = "frmMotivo";
            Text = "frmMotivo";
            Load += frmMotivo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMotivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtDescripcion;
        private DataGridView dgvMotivos;
        private Button btnInsertar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnExportarPDF;
        private Button btnExportarCSV;
    }
}