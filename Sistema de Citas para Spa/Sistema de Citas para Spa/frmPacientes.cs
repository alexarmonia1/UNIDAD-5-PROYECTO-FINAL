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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;



namespace Sistema_de_Citas_para_Spa
{
    public partial class frmPacientes : Form
    {
        public frmPacientes()
        {
            InitializeComponent();
        }

        private void cargarPacientes()
        {
            using (var db = new spaEntities())
            {
                var pacientes = db.Pacientes
                    .Select(p => new
                    {
                        p.PacienteId,
                        p.Nombre,
                        p.Telefono,
                        p.Email
                    })
                    .ToList();

                dgvPacientes.DataSource = pacientes;
            }


        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmPacientes_Load(object sender, EventArgs e)
        {
            cargarPacientes();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    limpiarCampos();
                    return; 
                }


                using (var db = new spaEntities())
                {
                    var nuevo = new Pacientes
                    {
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Email = txtEmail.Text
                    };

                    db.Pacientes.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Paciente agregado correctamente.");
                cargarPacientes(); 
                limpiarCampos() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar paciente: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                if (dgvPacientes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un paciente de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvPacientes.CurrentRow.Cells["PacienteId"].Value);

                using (var db = new spaEntities())
                {
                    var paciente = db.Pacientes.FirstOrDefault(p => p.PacienteId == id);

                    if (paciente != null)
                    {
                        paciente.Nombre = txtNombre.Text;
                        paciente.Telefono = txtTelefono.Text;
                        paciente.Email = txtEmail.Text;

                        db.SaveChanges();
                        MessageBox.Show("Paciente actualizado correctamente.");
                        limpiarCampos();
                        
                    }
                    else
                    {
                        MessageBox.Show("Paciente no encontrado.");
                    }
                }

                cargarPacientes(); 
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar paciente: " + ex.Message);
            }


        }

        private void dgvPacientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow != null)
            {
                txtNombre.Text = dgvPacientes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = dgvPacientes.CurrentRow.Cells["Telefono"].Value.ToString();
                txtEmail.Text = dgvPacientes.CurrentRow.Cells["Email"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPacientes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un paciente de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvPacientes.CurrentRow.Cells["PacienteId"].Value);

                using (var db = new spaEntities())
                {
                    var paciente = db.Pacientes.FirstOrDefault(p => p.PacienteId == id);

                    if (paciente != null)
                    {
                        db.Pacientes.Remove(paciente);
                        db.SaveChanges();
                        MessageBox.Show("Paciente eliminado correctamente.");
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Paciente no encontrado.");
                    }
                }

                cargarPacientes(); 
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar paciente: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar pacientes en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    
                    doc.Add(new Paragraph("Lista de Pacientes"));
                    doc.Add(new Paragraph(" "));

                    
                    PdfPTable tabla = new PdfPTable(4);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Teléfono");
                    tabla.AddCell("Email");

                    
                    foreach (DataGridViewRow fila in dgvPacientes.Rows)
                    {
                        if (fila.Cells["PacienteId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["PacienteId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Telefono"].Value.ToString());
                            tabla.AddCell(fila.Cells["Email"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message);
            }




        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Ventana para elegir ruta y nombre
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Guardar pacientes en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        // Encabezados
                        sw.WriteLine("PacienteId,Nombre,Telefono,Email");

                        // recorro mi dgvPacientes
                        foreach (DataGridViewRow fila in dgvPacientes.Rows)
                        {
                            if (fila.Cells["PacienteId"].Value != null)
                            {
                                string linea = fila.Cells["PacienteId"].Value.ToString() + "," +
                                               fila.Cells["Nombre"].Value.ToString() + "," +
                                               fila.Cells["Telefono"].Value.ToString() + "," +
                                               fila.Cells["Email"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }
    }
}
