namespace Consultorio_dental
{
    public partial class frmPrincipal : Form
    {
        frmCita frmCita = new frmCita();
        frmDentista frmDentista = new frmDentista();
        frmMotivo frmMotivo = new frmMotivo();
        frmPaciente frmPaciente = new frmPaciente();
        public frmPrincipal()
        {



            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void citaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCita.ShowDialog();
        }

        private void pacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPaciente.ShowDialog();
        }

        private void motivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMotivo.ShowDialog();
        }

        private void dentistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDentista.ShowDialog();
        }
    }
}
