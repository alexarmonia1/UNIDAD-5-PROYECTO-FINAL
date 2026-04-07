using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_de_Citas_para_Spa.modelos;
using System.Data.Entity;


namespace Sistema_de_Citas_para_Spa
{
    public partial class frmCitas : Form
    {
        public frmCitas()
        {
            InitializeComponent();
        }

        private void CargarCitas() 
        {
            using (var db = new spaEntities()) 
            {
                var citas = db.Citas
                    .Select(c => new
                    {
                        c.CitaId,
                        c.PacienteId,
                        c.ServicioId,
                        c.TerapeutaId,
                        c.Fecha,
                        c.Hora
                    })
                    .ToList();
                dgvCitas.DataSource = citas;

            }
        }

       

        private void frmCitas_Load(object sender, EventArgs e)
        {
            CargarCitas();
           
        }

        private void cmbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
           

        }
    } 
}

