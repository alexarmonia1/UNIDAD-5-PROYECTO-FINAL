namespace Consultorio_dental
{
    partial class frmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            citaToolStripMenuItem = new ToolStripMenuItem();
            pacienteToolStripMenuItem = new ToolStripMenuItem();
            motivoToolStripMenuItem = new ToolStripMenuItem();
            dentistaToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { citaToolStripMenuItem, pacienteToolStripMenuItem, motivoToolStripMenuItem, dentistaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // citaToolStripMenuItem
            // 
            citaToolStripMenuItem.Name = "citaToolStripMenuItem";
            citaToolStripMenuItem.Size = new Size(58, 29);
            citaToolStripMenuItem.Text = "Cita";
            citaToolStripMenuItem.Click += citaToolStripMenuItem_Click;
            // 
            // pacienteToolStripMenuItem
            // 
            pacienteToolStripMenuItem.Name = "pacienteToolStripMenuItem";
            pacienteToolStripMenuItem.Size = new Size(92, 29);
            pacienteToolStripMenuItem.Text = "Paciente";
            pacienteToolStripMenuItem.Click += pacienteToolStripMenuItem_Click;
            // 
            // motivoToolStripMenuItem
            // 
            motivoToolStripMenuItem.Name = "motivoToolStripMenuItem";
            motivoToolStripMenuItem.Size = new Size(85, 29);
            motivoToolStripMenuItem.Text = "Motivo";
            motivoToolStripMenuItem.Click += motivoToolStripMenuItem_Click;
            // 
            // dentistaToolStripMenuItem
            // 
            dentistaToolStripMenuItem.Name = "dentistaToolStripMenuItem";
            dentistaToolStripMenuItem.Size = new Size(93, 29);
            dentistaToolStripMenuItem.Text = "Dentista";
            dentistaToolStripMenuItem.Click += dentistaToolStripMenuItem_Click;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmPrincipal";
            Text = "dentista";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem citaToolStripMenuItem;
        private ToolStripMenuItem pacienteToolStripMenuItem;
        private ToolStripMenuItem motivoToolStripMenuItem;
        private ToolStripMenuItem dentistaToolStripMenuItem;
    }
}
