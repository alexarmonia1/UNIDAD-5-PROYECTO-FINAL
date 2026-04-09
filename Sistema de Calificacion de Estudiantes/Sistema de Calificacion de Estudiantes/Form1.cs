using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Calificacion_de_Estudiantes
{
    public partial class frmPrincipal : Form
    {
        frmEstudiantes varEstudiantes = new frmEstudiantes();
        frmMaterias varMaterias = new frmMaterias();
        frmCalificaciones varCalificaciones = new frmCalificaciones();
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void estudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           varEstudiantes.ShowDialog();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varMaterias.ShowDialog();
        }

        private void calificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            varCalificaciones.ShowDialog();
        }
    }
}
