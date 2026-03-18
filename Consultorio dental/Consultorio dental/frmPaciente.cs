using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Consultorio_dental.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Consultorio_dental
{
    public partial class frmPaciente : Form
    {
        public frmPaciente()
        {
            InitializeComponent();
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {

        }

        private void CargarPacientes()
        {
            using var db = new ConsultorioContext();

            dgvPacientes.DataSource = db.Pacientes
                .Select(p => new
                {
                    p.PacienteId,
                    p.Nombre,
                    p.Apellido,
                    p.FechaNacimiento,
                    p.Telefono,
                    p.Correo

                })
                .ToList();
        }


        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPaciente_Load_1(object sender, EventArgs e)
        {
            CargarPacientes();
            LimpiarCampos();

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new ConsultorioContext();
                var nuevo = new Paciente
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    FechaNacimiento = DateOnly.FromDateTime(dtpFechaNacimiento.Value),
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text
                };
                db.Pacientes.Add(nuevo);
                db.SaveChanges();
                MessageBox.Show("Paciente agregado correctamente");
                dgvPacientes.DataSource = db.Pacientes.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            CargarPacientes();
            LimpiarCampos();




        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new ConsultorioContext();

                if (dgvPacientes.CurrentRow != null)
                {
                    int id = (int)dgvPacientes.CurrentRow.Cells["PacienteId"].Value;
                    var paciente = db.Pacientes.Find(id);

                    if (paciente != null)
                    {
                        paciente.Nombre = txtNombre.Text;
                        paciente.Apellido = txtApellido.Text;
                        paciente.FechaNacimiento = DateOnly.FromDateTime(dtpFechaNacimiento.Value);
                        paciente.Telefono = txtTelefono.Text;
                        paciente.Correo = txtCorreo.Text;

                        db.SaveChanges();
                        MessageBox.Show("Paciente actualizado correctamente");
                        dgvPacientes.DataSource = db.Pacientes.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Paciente no encontrado");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un paciente en la tabla");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            CargarPacientes();
            LimpiarCampos();




        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new ConsultorioContext();

                if (dgvPacientes.CurrentRow != null)
                {

                    int id = (int)dgvPacientes.CurrentRow.Cells["PacienteId"].Value;

                    var paciente = db.Pacientes.Find(id);

                    if (paciente != null)
                    {
                        db.Pacientes.Remove(paciente);
                        db.SaveChanges();
                        MessageBox.Show("Paciente eliminado correctamente");


                        dgvPacientes.DataSource = db.Pacientes.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Paciente no encontrado");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un paciente en la tabla");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            CargarPacientes();
            LimpiarCampos();

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = "Guardar listado de pacientes";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarPacientesPDF(saveFileDialog.FileName);
            }


        }

        private void ExportarPacientesPDF(string rutaArchivo)
        {
            var doc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            PdfPTable tabla = new PdfPTable(dgvPacientes.Columns.Count);

            // aqui agrego los encabezados
            foreach (DataGridViewColumn columna in dgvPacientes.Columns)
            {
                tabla.AddCell(new Phrase(columna.HeaderText));
            }

            // y aqui las filas
            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (!fila.IsNewRow)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        tabla.AddCell(new Phrase(celda.Value?.ToString() ?? ""));
                    }
                }
            }

            doc.Add(tabla);
            doc.Close();

            MessageBox.Show("Pacientes exportados a PDF correctamente");
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Guardar listado de pacientes";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarPacientesCSV(saveFileDialog.FileName);
            }


        }

        private void ExportarPacientesCSV(string rutaArchivo)
        {
            using (var sw = new StreamWriter(rutaArchivo))
            {
               
                var encabezados = string.Join(",", dgvPacientes.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => c.HeaderText));
                sw.WriteLine(encabezados);

               
                foreach (DataGridViewRow fila in dgvPacientes.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        var valores = string.Join(",", fila.Cells
                            .Cast<DataGridViewCell>()
                            .Select(c => c.Value?.ToString()?.Replace(",", " ")));
                        sw.WriteLine(valores);
                    }
                }
            }

            MessageBox.Show("Pacientes exportados a CSV correctamente");
        }
    }
}
