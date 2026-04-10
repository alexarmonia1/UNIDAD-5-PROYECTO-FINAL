using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_de_manejo_de_empleados.modelos;

namespace sistema_de_manejo_de_empleados
{
    public partial class Form1 : Form
    {
        departamento d = new departamento();
        cargos c = new cargos();
        empleados emplo = new empleados();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            d.ShowDialog();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            c.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           emplo.ShowDialog();
        }
    }
}
