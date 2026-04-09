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
    public partial class frmCalificaciones : Form
    {
        public frmCalificaciones()
        {
            InitializeComponent();
        }

        private void cargarCalificaciones()
        {
            using (var db = new estudiantesEntities())
            {
                var calificaciones = db.Calificaciones
                    .Select(c => new
                    {
                        c.CalificacionId,
                        Estudiante = c.Estudiantes.Nombre,
                        Materia = c.Materias.Nombre,
                        c.Calificacion1,
                        c.Calificacion2,
                        c.Calificacion3,
                        c.Calificacion4,
                        c.Examen,
                        c.Total,
                        c.Clasificacion,
                        c.Estado
                    })
                    .ToList();

                dgvCalificaciones.DataSource = calificaciones;
            }
        }

        private void limpiarCampos()
        {
           
            cmbEstudiantes.SelectedIndex = -1;
            cmbMaterias.SelectedIndex = -1;

           
            numC1.Value = 0;
            numC2.Value = 0;
            numC3.Value = 0;
            numC4.Value = 0;
            numExamen.Value = 0;
        }

        private void calcularResultados(out decimal total, out string clasificacion, out string estado)
        {
            decimal promedio = (numC1.Value + numC2.Value + numC3.Value + numC4.Value) / 4;
            total = (promedio * 0.7m) + (numExamen.Value * 0.3m);

            if (total >= 90) clasificacion = "A";
            else if (total >= 80) clasificacion = "B";
            else if (total >= 70) clasificacion = "C";
            else clasificacion = "F";

            estado = (total >= 70) ? "Aprobado" : "Reprobado";
        }





        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmCalificaciones_Load(object sender, EventArgs e)
        {
            cargarCalificaciones();

            using (var db = new estudiantesEntities())
            {
                // Estudiantes
                cmbEstudiantes.DataSource = db.Estudiantes.ToList();
                cmbEstudiantes.DisplayMember = "Nombre";       
                cmbEstudiantes.ValueMember = "EstudianteId";   
                cmbEstudiantes.SelectedIndex = -1;             

                // Materias
                cmbMaterias.DataSource = db.Materias.ToList();
                cmbMaterias.DisplayMember = "Nombre";
                cmbMaterias.ValueMember = "MateriaId";
                cmbMaterias.SelectedIndex = -1;
            }

            cargarCalificaciones();


        }

        private void dgvCalificaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCalificaciones.CurrentRow != null)
            {
                
                cmbEstudiantes.Text = dgvCalificaciones.CurrentRow.Cells["Estudiante"].Value.ToString();
                cmbMaterias.Text = dgvCalificaciones.CurrentRow.Cells["Materia"].Value.ToString();

               
                numC1.Value = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["Calificacion1"].Value);
                numC2.Value = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["Calificacion2"].Value);
                numC3.Value = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["Calificacion3"].Value);
                numC4.Value = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["Calificacion4"].Value);
                numExamen.Value = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["Examen"].Value);
            }


        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEstudiantes.SelectedValue == null || cmbMaterias.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar estudiante y materia.");
                    return;
                }

                calcularResultados(out decimal total, out string clasificacion, out string estado);

                using (var db = new estudiantesEntities())
                {
                    var nueva = new Calificaciones
                    {
                        EstudianteId = (int)cmbEstudiantes.SelectedValue,
                        MateriaId = (int)cmbMaterias.SelectedValue,
                        Calificacion1 = (int)numC1.Value,
                        Calificacion2 = (int)numC2.Value,
                        Calificacion3 = (int)numC3.Value,
                        Calificacion4 = (int)numC4.Value,
                        Examen = (int)numExamen.Value,
                        Total = total,
                        Clasificacion = clasificacion,
                        Estado = estado
                    };

                    db.Calificaciones.Add(nueva);
                    db.SaveChanges();
                }

                MessageBox.Show("Calificación agregada correctamente.");
                cargarCalificaciones();
                limpiarCampos(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar calificación: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEstudiantes.SelectedValue == null || cmbMaterias.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar estudiante y materia.");
                    return;
                }

                if (dgvCalificaciones.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una calificación de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["CalificacionId"].Value);

                calcularResultados(out decimal total, out string clasificacion, out string estado);

                using (var db = new estudiantesEntities())
                {
                    var calificacion = db.Calificaciones.FirstOrDefault(c => c.CalificacionId == id);

                    if (calificacion != null)
                    {
                        calificacion.EstudianteId = (int)cmbEstudiantes.SelectedValue;
                        calificacion.MateriaId = (int)cmbMaterias.SelectedValue;
                        calificacion.Calificacion1 = (int)numC1.Value;
                        calificacion.Calificacion2 = (int)numC2.Value;
                        calificacion.Calificacion3 = (int)numC3.Value;
                        calificacion.Calificacion4 = (int)numC4.Value;
                        calificacion.Examen = (int)numExamen.Value;
                        calificacion.Total = total;
                        calificacion.Clasificacion = clasificacion;
                        calificacion.Estado = estado;

                        db.SaveChanges();
                        MessageBox.Show("Calificación actualizada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Calificación no encontrada.");
                    }
                }

                cargarCalificaciones();
                limpiarCampos(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar calificación: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCalificaciones.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una calificación de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvCalificaciones.CurrentRow.Cells["CalificacionId"].Value);

                using (var db = new estudiantesEntities())
                {
                    var calificacion = db.Calificaciones.FirstOrDefault(c => c.CalificacionId == id);

                    if (calificacion != null)
                    {
                        db.Calificaciones.Remove(calificacion);
                        db.SaveChanges();
                        MessageBox.Show("Calificación eliminada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Calificación no encontrada.");
                    }
                }

                cargarCalificaciones();
                limpiarCampos(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar calificación: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar calificaciones en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4.Rotate()); // horizontal para más columnas
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Calificaciones"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(11); // 11 columnas
                    tabla.AddCell("ID");
                    tabla.AddCell("Estudiante");
                    tabla.AddCell("Materia");
                    tabla.AddCell("C1");
                    tabla.AddCell("C2");
                    tabla.AddCell("C3");
                    tabla.AddCell("C4");
                    tabla.AddCell("Examen");
                    tabla.AddCell("Total");
                    tabla.AddCell("Clasificación");
                    tabla.AddCell("Estado");

                    foreach (DataGridViewRow fila in dgvCalificaciones.Rows)
                    {
                        if (fila.Cells["CalificacionId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["CalificacionId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Estudiante"].Value.ToString());
                            tabla.AddCell(fila.Cells["Materia"].Value.ToString());
                            tabla.AddCell(fila.Cells["Calificacion1"].Value.ToString());
                            tabla.AddCell(fila.Cells["Calificacion2"].Value.ToString());
                            tabla.AddCell(fila.Cells["Calificacion3"].Value.ToString());
                            tabla.AddCell(fila.Cells["Calificacion4"].Value.ToString());
                            tabla.AddCell(fila.Cells["Examen"].Value.ToString());
                            tabla.AddCell(fila.Cells["Total"].Value.ToString());
                            tabla.AddCell(fila.Cells["Clasificacion"].Value.ToString());
                            tabla.AddCell(fila.Cells["Estado"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF de calificaciones generado correctamente en: " + ruta);
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
                saveFileDialog.Title = "Guardar calificaciones en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("CalificacionId,Estudiante,Materia,C1,C2,C3,C4,Examen,Total,Clasificacion,Estado");

                        foreach (DataGridViewRow fila in dgvCalificaciones.Rows)
                        {
                            if (fila.Cells["CalificacionId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["CalificacionId"].Value.ToString() + "," +
                                    fila.Cells["Estudiante"].Value.ToString() + "," +
                                    fila.Cells["Materia"].Value.ToString() + "," +
                                    fila.Cells["Calificacion1"].Value.ToString() + "," +
                                    fila.Cells["Calificacion2"].Value.ToString() + "," +
                                    fila.Cells["Calificacion3"].Value.ToString() + "," +
                                    fila.Cells["Calificacion4"].Value.ToString() + "," +
                                    fila.Cells["Examen"].Value.ToString() + "," +
                                    fila.Cells["Total"].Value.ToString() + "," +
                                    fila.Cells["Clasificacion"].Value.ToString() + "," +
                                    fila.Cells["Estado"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV de calificaciones generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
    }
}
