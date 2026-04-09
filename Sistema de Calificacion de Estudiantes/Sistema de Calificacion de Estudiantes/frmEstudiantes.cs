using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Sistema_de_Calificacion_de_Estudiantes.Modelos;

namespace Sistema_de_Calificacion_de_Estudiantes
{
    public partial class frmEstudiantes : Form
    {
        public frmEstudiantes()
        {
            InitializeComponent();
        }

        private void cargarEstudiantes()
        {
            using (var db = new estudiantesEntities())
            {
                var estudiantes = db.Estudiantes
                    .Select(e => new
                    {
                        e.EstudianteId,
                        e.Nombre,
                        e.Matricula
                    })
                    .ToList();

                dgvEstudiantes.DataSource = estudiantes;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            txtMatricula.Clear();
        }

        private void frmEstudiantes_Load(object sender, EventArgs e)
        {
            cargarEstudiantes();
            limpiarCampos();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtMatricula.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                using (var db = new estudiantesEntities())
                {
                    var nuevo = new Estudiantes
                    {
                        Nombre = txtNombre.Text,
                        Matricula = txtMatricula.Text
                    };

                    db.Estudiantes.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Estudiante agregado correctamente.");
                cargarEstudiantes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar estudiante: " + ex.Message);
            }


        }

        private void dgvEstudiantes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow != null)
            {
                txtNombre.Text = dgvEstudiantes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtMatricula.Text = dgvEstudiantes.CurrentRow.Cells["Matricula"].Value.ToString();
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtMatricula.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                if (dgvEstudiantes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un estudiante de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvEstudiantes.CurrentRow.Cells["EstudianteId"].Value);

                using (var db = new estudiantesEntities())
                {
                    var estudiante = db.Estudiantes.FirstOrDefault(u => u.EstudianteId == id);

                    if (estudiante != null)
                    {
                        estudiante.Nombre = txtNombre.Text;
                        estudiante.Matricula = txtMatricula.Text;

                        db.SaveChanges();
                        MessageBox.Show("Estudiante actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Estudiante no encontrado.");
                    }
                }

                cargarEstudiantes();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar estudiante: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEstudiantes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un estudiante de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvEstudiantes.CurrentRow.Cells["EstudianteId"].Value);

                using (var db = new estudiantesEntities())
                {
                    var estudiante = db.Estudiantes.FirstOrDefault(u => u.EstudianteId == id);

                    if (estudiante != null)
                    {
                        db.Estudiantes.Remove(estudiante);
                        db.SaveChanges();
                        MessageBox.Show("Estudiante eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Estudiante no encontrado.");
                    }
                }

                cargarEstudiantes();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar estudiante: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar estudiantes en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Estudiantes"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(3);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Matrícula");

                    foreach (DataGridViewRow fila in dgvEstudiantes.Rows)
                    {
                        if (fila.Cells["EstudianteId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["EstudianteId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Matricula"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF de estudiantes generado correctamente en: " + ruta);
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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Guardar estudiantes en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("EstudianteId,Nombre,Matrícula");

                        foreach (DataGridViewRow fila in dgvEstudiantes.Rows)
                        {
                            if (fila.Cells["EstudianteId"].Value != null)
                            {
                                string linea = fila.Cells["EstudianteId"].Value.ToString() + "," +
                                               fila.Cells["Nombre"].Value.ToString() + "," +
                                               fila.Cells["Matricula"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV de estudiantes generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }
    }
}

