using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agencia_de_Tours
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPaises frmPaises = new frmPaises();
            frmPaises.ShowDialog();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDestinos destinos = new frmDestinos();
            destinos.ShowDialog();
        }

        private void toursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTours tours = new frmTours();
            tours.ShowDialog();
        }
    }
}
