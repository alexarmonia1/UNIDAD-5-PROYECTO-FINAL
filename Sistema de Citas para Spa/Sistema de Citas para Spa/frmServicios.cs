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
    public partial class frmServicios : Form
    {
        public frmServicios()
        {
            InitializeComponent();
        }

        private void cargarServicios()
        {
            using (var db = new spaEntities())
            {
                var servicios = db.Servicios
                    .Select(s => new
                    {
                        s.ServicioId,
                        s.Nombre,
                        s.DuracionMinutos,
                        s.Precio
                    })
                    .ToList();

                dgvServicios.DataSource = servicios;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            
        }

        private void frmServicios_Load(object sender, EventArgs e)
        {
            cargarServicios();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    limpiarCampos();
                    return;
                }

                // Validar que Precio sea decimal
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }

                using (var db = new spaEntities())
                {
                    var nuevo = new Servicios
                    {
                        Nombre = txtNombre.Text,
                        DuracionMinutos = (int)numDuracion.Value, 
                        Precio = precio
                    };

                    db.Servicios.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Servicio agregado correctamente.");
                cargarServicios();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar servicio: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }

                
                if (dgvServicios.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un servicio de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvServicios.CurrentRow.Cells["ServicioId"].Value);

                using (var db = new spaEntities())
                {
                    var servicio = db.Servicios.FirstOrDefault(s => s.ServicioId == id);

                    if (servicio != null)
                    {
                        servicio.Nombre = txtNombre.Text;
                        servicio.DuracionMinutos = (int)numDuracion.Value;
                        servicio.Precio = precio;

                        db.SaveChanges();
                        MessageBox.Show("Servicio actualizado correctamente.");
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Servicio no encontrado.");
                    }
                }

                cargarServicios(); // refresca el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar servicio: " + ex.Message);
            }


        }

        private void dgvServicios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServicios.CurrentRow != null)
            {
                // Nombre
                txtNombre.Text = dgvServicios.CurrentRow.Cells["Nombre"].Value.ToString();

                // Duración (NumericUpDown)
                if (int.TryParse(dgvServicios.CurrentRow.Cells["DuracionMinutos"].Value.ToString(), out int duracion))
                {
                    numDuracion.Value = duracion;
                }

                // Precio
                txtPrecio.Text = dgvServicios.CurrentRow.Cells["Precio"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dgvServicios.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un servicio de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvServicios.CurrentRow.Cells["ServicioId"].Value);

                using (var db = new spaEntities())
                {
                    var servicio = db.Servicios.FirstOrDefault(s => s.ServicioId == id);

                    if (servicio != null)
                    {
                        db.Servicios.Remove(servicio);
                        db.SaveChanges();
                        MessageBox.Show("Servicio eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Servicio no encontrado.");
                    }
                }

                cargarServicios(); 
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar servicio: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar servicios en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Servicios"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(4);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Duración (min)");
                    tabla.AddCell("Precio");

                    foreach (DataGridViewRow fila in dgvServicios.Rows)
                    {
                        if (fila.Cells["ServicioId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["ServicioId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["DuracionMinutos"].Value.ToString());
                            tabla.AddCell(fila.Cells["Precio"].Value.ToString());
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("PDF de servicios generado correctamente en: " + ruta);
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
                saveFileDialog.Title = "Guardar servicios en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("ServicioId,Nombre,DuracionMinutos,Precio");

                        foreach (DataGridViewRow fila in dgvServicios.Rows)
                        {
                            if (fila.Cells["ServicioId"].Value != null)
                            {
                                string linea = fila.Cells["ServicioId"].Value.ToString() + "," +
                                               fila.Cells["Nombre"].Value.ToString() + "," +
                                               fila.Cells["DuracionMinutos"].Value.ToString() + "," +
                                               fila.Cells["Precio"].Value.ToString();

                                sw.WriteLine(linea);
                            }
                        }
                    }

                    MessageBox.Show("CSV de servicios generado correctamente en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }


        }
    }
}
