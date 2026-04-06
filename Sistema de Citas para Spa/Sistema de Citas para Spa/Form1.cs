using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Citas_para_Spa
{
    public partial class frmPrincipal : Form
    {
        frmCitas varCitas = new frmCitas();
        frmPacientes varPacientes = new frmPacientes();
        frmServicios varServicios = new frmServicios();
        frmTerapeutas varTerapeutas = new frmTerapeutas();
        public frmPrincipal()
        {
            
            InitializeComponent();
        }

        private void citToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varCitas.ShowDialog();
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varPacientes.ShowDialog();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varServicios.ShowDialog();
        }

        private void terapeutasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varTerapeutas.ShowDialog();
        }
    }
}
