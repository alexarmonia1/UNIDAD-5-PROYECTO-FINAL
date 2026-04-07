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
using Sistema_de_Citas_para_Spa.modelos;

namespace Sistema_de_Citas_para_Spa
{
    public partial class frmTerapeutas : Form
    {
        public frmTerapeutas()
        {
            InitializeComponent();
        }

        private void cargarTerapeutas()
        {
            using (var db = new spaEntities())
            {
                var terapeutas = db.Terapeutas
                    .Select(t => new
                    {
                        t.TerapeutaId,
                        t.Nombre,
                        t.Especialidad
                    })
                    .ToList();

                dgvTerapeutas.DataSource = terapeutas;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            txtEspecialidad.Clear();
        }

        

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtEspecialidad.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    txtNombre.Focus();
                    return;
                }

                using (var db = new spaEntities())
                {
                    var nuevo = new Terapeutas
                    {
                        Nombre = txtNombre.Text,
                        Especialidad = txtEspecialidad.Text
                    };

                    db.Terapeutas.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Terapeuta agregado correctamente.");
                cargarTerapeutas();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar terapeuta: " + ex.Message);
            }


        }

        private void frmTerapeutas_Load(object sender, EventArgs e)
        {
            cargarTerapeutas();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtEspecialidad.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                if (dgvTerapeutas.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un terapeuta de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvTerapeutas.CurrentRow.Cells["TerapeutaId"].Value);

                using (var db = new spaEntities())
                {
                    var terapeuta = db.Terapeutas.FirstOrDefault(t => t.TerapeutaId == id);

                    if (terapeuta != null)
                    {
                        terapeuta.Nombre = txtNombre.Text;
                        terapeuta.Especialidad = txtEspecialidad.Text;

                        db.SaveChanges();
                        MessageBox.Show("Terapeuta actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Terapeuta no encontrado.");
                    }
                }

                cargarTerapeutas();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar terapeuta: " + ex.Message);
            }


        }

        private void dgvTerapeutas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTerapeutas.CurrentRow != null)
            {
                txtNombre.Text = dgvTerapeutas.CurrentRow.Cells["Nombre"].Value.ToString();
                txtEspecialidad.Text = dgvTerapeutas.CurrentRow.Cells["Especialidad"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTerapeutas.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un terapeuta de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvTerapeutas.CurrentRow.Cells["TerapeutaId"].Value);

                using (var db = new spaEntities())
                {
                    var terapeuta = db.Terapeutas.FirstOrDefault(t => t.TerapeutaId == id);

                    if (terapeuta != null)
                    {
                        db.Terapeutas.Remove(terapeuta);
                        db.SaveChanges();
                        MessageBox.Show("Terapeuta eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Terapeuta no encontrado.");
                    }
                }

                cargarTerapeutas();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar terapeuta: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar terapeutas en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Terapeutas"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(3);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Especialidad");

                    foreach (DataGridViewRow fila in dgvTerapeutas.Rows)
                    {
                        if (fila.Cells["TerapeutaId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["TerapeutaId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Especialidad"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF de terapeutas generado correctamente en: " + ruta);
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
                saveFileDialog.Title = "Guardar terapeutas en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("TerapeutaId,Nombre,Especialidad");

                        foreach (DataGridViewRow fila in dgvTerapeutas.Rows)
                        {
                            if (fila.Cells["TerapeutaId"].Value != null)
                            {
                                string linea = fila.Cells["TerapeutaId"].Value.ToString() + "," +
                                               fila.Cells["Nombre"].Value.ToString() + "," +
                                               fila.Cells["Especialidad"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV de terapeutas generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }
    }
}
