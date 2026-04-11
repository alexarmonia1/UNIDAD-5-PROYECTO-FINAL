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
using Agencia_de_Tours.modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Agencia_de_Tours
{
    public partial class frmDestinos : Form
    {
        public frmDestinos()
        {
            InitializeComponent();
        }

        private void cargarDestinos()
        {
            using (var db = new toursEntities())
            {
                var destinos = db.Destinos
                    .Select(d => new
                    {
                        d.DestinoId,
                        d.Nombre,
                        Pais = d.Paises.Nombre,
                        d.DuracionDias,
                        d.DuracionHoras
                    })
                    .ToList();

                dgvDestinos.DataSource = destinos;
            }
        }

        private void frmDestinos_Load(object sender, EventArgs e)
        {
            using (var db = new toursEntities())
            {
                cmbPais.DataSource = db.Paises.ToList();
                cmbPais.DisplayMember = "Nombre";
                cmbPais.ValueMember = "PaisId";
            }

            cargarDestinos();


        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            cmbPais.SelectedIndex = -1;
            numDias.Value = 0;
            numHoras.Value = 0;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbPais.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            using (var db = new toursEntities())
            {
                var nuevo = new Destinos
                {
                    Nombre = txtNombre.Text,
                    PaisId = (int)cmbPais.SelectedValue,
                    DuracionDias = (int)numDias.Value,
                    DuracionHoras = (int)numHoras.Value
                };

                db.Destinos.Add(nuevo);
                db.SaveChanges();
            }

            MessageBox.Show("Destino agregado correctamente.");
            cargarDestinos();
            limpiarCampos();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvDestinos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un destino de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvDestinos.CurrentRow.Cells["DestinoId"].Value);

            using (var db = new toursEntities())
            {
                var destino = db.Destinos.FirstOrDefault(d => d.DestinoId == id);

                if (destino != null)
                {
                    destino.Nombre = txtNombre.Text;
                    destino.PaisId = (int)cmbPais.SelectedValue;
                    destino.DuracionDias = (int)numDias.Value;
                    destino.DuracionHoras = (int)numHoras.Value;

                    db.SaveChanges();
                    MessageBox.Show("Destino actualizado correctamente.");
                }
            }

            cargarDestinos();
            limpiarCampos();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDestinos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un destino de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvDestinos.CurrentRow.Cells["DestinoId"].Value);

            using (var db = new toursEntities())
            {
                var destino = db.Destinos.FirstOrDefault(d => d.DestinoId == id);

                if (destino != null)
                {
                    db.Destinos.Remove(destino);
                    db.SaveChanges();
                    MessageBox.Show("Destino eliminado correctamente.");
                }
            }

            cargarDestinos();
            limpiarCampos();


        }

        private void dgvDestinos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDestinos.CurrentRow != null)
            {
                txtNombre.Text = dgvDestinos.CurrentRow.Cells["Nombre"].Value.ToString();
                cmbPais.Text = dgvDestinos.CurrentRow.Cells["Pais"].Value.ToString();
                numDias.Value = Convert.ToInt32(dgvDestinos.CurrentRow.Cells["DuracionDias"].Value);
                numHoras.Value = Convert.ToInt32(dgvDestinos.CurrentRow.Cells["DuracionHoras"].Value);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar destinos en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4.Rotate()); // horizontal
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Destinos"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(4); // ID, Nombre, País, Duración
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("País");
                    tabla.AddCell("Duración (Días/Horas)");

                    foreach (DataGridViewRow fila in dgvDestinos.Rows)
                    {
                        if (fila.Cells["DestinoId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["DestinoId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Pais"].Value.ToString());
                            tabla.AddCell(fila.Cells["DuracionDias"].Value.ToString() + " días / " +
                                          fila.Cells["DuracionHoras"].Value.ToString() + " horas");
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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Guardar destinos en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("DestinoId,Nombre,Pais,DuracionDias,DuracionHoras");

                        foreach (DataGridViewRow fila in dgvDestinos.Rows)
                        {
                            if (fila.Cells["DestinoId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["DestinoId"].Value.ToString() + "," +
                                    fila.Cells["Nombre"].Value.ToString() + "," +
                                    fila.Cells["Pais"].Value.ToString() + "," +
                                    fila.Cells["DuracionDias"].Value.ToString() + "," +
                                    fila.Cells["DuracionHoras"].Value.ToString();

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
