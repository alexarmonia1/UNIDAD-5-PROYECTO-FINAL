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
    public partial class frmMaterias : Form
    {
        public frmMaterias()
        {
            InitializeComponent();
        }
        private void cargarMaterias()
        {
            using (var db = new estudiantesEntities())
            {
                var materias = db.Materias
                    .Select(m => new
                    {
                        m.MateriaId,
                        m.Nombre,
                        m.Creditos
                    })
                    .ToList();

                dgvMaterias.DataSource = materias;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            numCreditos.Value = 0;
           
        }

     

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmMaterias_Load(object sender, EventArgs e)
        {
            cargarMaterias();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.");
                    return;
                }

                using (var db = new estudiantesEntities())
                {
                    var nueva = new Materias
                    {
                        Nombre = txtNombre.Text,
                        Creditos = (int)numCreditos.Value
                    };

                    db.Materias.Add(nueva);
                    db.SaveChanges();
                }

                MessageBox.Show("Materia agregada correctamente.");
                cargarMaterias();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar materia: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.");
                    return;
                }

                if (dgvMaterias.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una materia de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvMaterias.CurrentRow.Cells["MateriaId"].Value);

                using (var db = new estudiantesEntities())
                {
                    var materia = db.Materias.FirstOrDefault(m => m.MateriaId == id);

                    if (materia != null)
                    {
                        materia.Nombre = txtNombre.Text;
                        materia.Creditos = (int)numCreditos.Value;

                        db.SaveChanges();
                        MessageBox.Show("Materia actualizada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Materia no encontrada.");
                    }
                }

                cargarMaterias();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar materia: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaterias.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una materia de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvMaterias.CurrentRow.Cells["MateriaId"].Value);

                using (var db = new estudiantesEntities())
                {
                    var materia = db.Materias.FirstOrDefault(m => m.MateriaId == id);

                    if (materia != null)
                    {
                        db.Materias.Remove(materia);
                        db.SaveChanges();
                        MessageBox.Show("Materia eliminada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Materia no encontrada.");
                    }
                }

                cargarMaterias();
                limpiarCampos() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar materia: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar materias en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Materias"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(3);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Créditos");

                    foreach (DataGridViewRow fila in dgvMaterias.Rows)
                    {
                        if (fila.Cells["MateriaId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["MateriaId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Creditos"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF de materias generado correctamente en: " + ruta);
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
                saveFileDialog.Title = "Guardar materias en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("MateriaId,Nombre,Creditos");

                        foreach (DataGridViewRow fila in dgvMaterias.Rows)
                        {
                            if (fila.Cells["MateriaId"].Value != null)
                            {
                                string linea = fila.Cells["MateriaId"].Value.ToString() + "," +
                                               fila.Cells["Nombre"].Value.ToString() + "," +
                                               fila.Cells["Creditos"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV de materias generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }

        private void dgvMaterias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaterias.CurrentRow != null)
            {
                txtNombre.Text = dgvMaterias.CurrentRow.Cells["Nombre"].Value.ToString();
                numCreditos.Value = Convert.ToInt32(dgvMaterias.CurrentRow.Cells["Creditos"].Value);
            }


        }
    }
}
