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
    public partial class frmTours : Form
    {
        public frmTours()
        {
            InitializeComponent();
        }

        private void cargarTours()
        {
            using (var db = new toursEntities())
            {
                var tours = db.Tours
                    .Select(t => new
                    {
                        t.TourId,
                        t.Nombre,
                        Pais = t.Paises.Nombre,
                        Destino = t.Destinos.Nombre,
                        t.Fecha,
                        t.Hora,
                        t.Precio,
                        t.ITBIS,
                        t.DuracionDias,
                        t.DuracionHoras,
                        t.FechaHoraFin,
                        t.Estado
                    })
                    .ToList();

                dgvTours.DataSource = tours;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmTours_Load(object sender, EventArgs e)
        {
            // Configuración del NumericUpDown para precios
            numPrecio.Minimum = 0;
            numPrecio.Maximum = 1000000;   // rango amplio para precios altos
            numPrecio.DecimalPlaces = 2;   // dos decimales
            numPrecio.Increment = 100;     // paso de incremento

            using (var db = new toursEntities())
            {
                cmbPais.DataSource = db.Paises.ToList();
                cmbPais.DisplayMember = "Nombre";
                cmbPais.ValueMember = "PaisId";
            }

            cargarTours();

        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            cmbPais.SelectedIndex = -1;
            cmbDestino.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;   
            dtpHora.Value = DateTime.Now;    
            numPrecio.Value = 0;             
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPais.SelectedValue != null && cmbPais.SelectedValue is int)
            {
                int paisId = (int)cmbPais.SelectedValue;

                using (var db = new toursEntities())
                {
                    cmbDestino.DataSource = db.Destinos.Where(d => d.PaisId == paisId).ToList();
                    cmbDestino.DisplayMember = "Nombre";
                    cmbDestino.ValueMember = "DestinoId";
                }
            }






        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbPais.SelectedIndex == -1 || cmbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            using (var db = new toursEntities())
            {
                var destino = db.Destinos.FirstOrDefault(d => d.DestinoId == (int)cmbDestino.SelectedValue);

                var nuevo = new Tours
                {
                    Nombre = txtNombre.Text,
                    PaisId = (int)cmbPais.SelectedValue,
                    DestinoId = (int)cmbDestino.SelectedValue,
                    Fecha = dtpFecha.Value.Date,
                    Hora = dtpHora.Value.TimeOfDay,
                    Precio = numPrecio.Value,
                    DuracionDias = destino.DuracionDias,
                    DuracionHoras = destino.DuracionHoras,
                    Estado = (dtpFecha.Value.Date + dtpHora.Value.TimeOfDay > DateTime.Now) ? "Vigente" : "No Vigente"
                };

                db.Tours.Add(nuevo);
                db.SaveChanges();
            }

            MessageBox.Show("Tour agregado correctamente.");
            cargarTours();
            limpiarCampos();


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvTours.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un tour de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvTours.CurrentRow.Cells["TourId"].Value);

            using (var db = new toursEntities())
            {
                var tour = db.Tours.FirstOrDefault(t => t.TourId == id);
                var destino = db.Destinos.FirstOrDefault(d => d.DestinoId == (int)cmbDestino.SelectedValue);

                if (tour != null)
                {
                    tour.Nombre = txtNombre.Text;
                    tour.PaisId = (int)cmbPais.SelectedValue;
                    tour.DestinoId = (int)cmbDestino.SelectedValue;
                    tour.Fecha = dtpFecha.Value.Date;
                    tour.Hora = dtpHora.Value.TimeOfDay;
                    tour.Precio = numPrecio.Value;
                    tour.DuracionDias = destino.DuracionDias;
                    tour.DuracionHoras = destino.DuracionHoras;
                    tour.Estado = (dtpFecha.Value.Date + dtpHora.Value.TimeOfDay > DateTime.Now) ? "Vigente" : "No Vigente";

                    db.SaveChanges();
                    MessageBox.Show("Tour actualizado correctamente.");
                }
            }

            cargarTours();
            limpiarCampos();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTours.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un tour de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvTours.CurrentRow.Cells["TourId"].Value);

            using (var db = new toursEntities())
            {
                var tour = db.Tours.FirstOrDefault(t => t.TourId == id);

                if (tour != null)
                {
                    db.Tours.Remove(tour);
                    db.SaveChanges();
                    MessageBox.Show("Tour eliminado correctamente.");
                }
            }

            cargarTours();
            limpiarCampos();


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar tours en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4.Rotate()); // horizontal
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Tours"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(11);
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("País");
                    tabla.AddCell("Destino");
                    tabla.AddCell("Fecha");
                    tabla.AddCell("Hora");
                    tabla.AddCell("Precio");
                    tabla.AddCell("ITBIS");
                    tabla.AddCell("Duración");
                    tabla.AddCell("Fecha Fin");
                    tabla.AddCell("Estado");

                    foreach (DataGridViewRow fila in dgvTours.Rows)
                    {
                        if (fila.Cells["TourId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["TourId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Pais"].Value.ToString());
                            tabla.AddCell(fila.Cells["Destino"].Value.ToString());
                            tabla.AddCell(fila.Cells["Fecha"].Value.ToString());
                            tabla.AddCell(fila.Cells["Hora"].Value.ToString());
                            tabla.AddCell(fila.Cells["Precio"].Value.ToString());
                            tabla.AddCell(fila.Cells["ITBIS"].Value.ToString());
                            tabla.AddCell(fila.Cells["DuracionDias"].Value.ToString() + " días / " +
                                          fila.Cells["DuracionHoras"].Value.ToString() + " horas");
                            tabla.AddCell(fila.Cells["FechaHoraFin"].Value.ToString());
                            tabla.AddCell(fila.Cells["Estado"].Value.ToString());
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
                saveFileDialog.Title = "Guardar tours en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("TourId,Nombre,Pais,Destino,Fecha,Hora,Precio,ITBIS,DuracionDias,DuracionHoras,FechaHoraFin,Estado");

                        foreach (DataGridViewRow fila in dgvTours.Rows)
                        {
                            if (fila.Cells["TourId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["TourId"].Value.ToString() + "," +
                                    fila.Cells["Nombre"].Value.ToString() + "," +
                                    fila.Cells["Pais"].Value.ToString() + "," +
                                    fila.Cells["Destino"].Value.ToString() + "," +
                                    fila.Cells["Fecha"].Value.ToString() + "," +
                                    fila.Cells["Hora"].Value.ToString() + "," +
                                    fila.Cells["Precio"].Value.ToString() + "," +
                                    fila.Cells["ITBIS"].Value.ToString() + "," +
                                    fila.Cells["DuracionDias"].Value.ToString() + "," +
                                    fila.Cells["DuracionHoras"].Value.ToString() + "," +
                                    fila.Cells["FechaHoraFin"].Value.ToString() + "," +
                                    fila.Cells["Estado"].Value.ToString();

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

        private void dgvTours_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTours.CurrentRow != null)
            {
                txtNombre.Text = dgvTours.CurrentRow.Cells["Nombre"].Value.ToString();

                cmbPais.Text = dgvTours.CurrentRow.Cells["Pais"].Value.ToString();
                cmbDestino.Text = dgvTours.CurrentRow.Cells["Destino"].Value.ToString();

                dtpFecha.Value = Convert.ToDateTime(dgvTours.CurrentRow.Cells["Fecha"].Value);
                dtpHora.Value = DateTime.Parse(dgvTours.CurrentRow.Cells["Hora"].Value.ToString());

                
                numPrecio.Value = Convert.ToDecimal(dgvTours.CurrentRow.Cells["Precio"].Value);

                string estado = dgvTours.CurrentRow.Cells["Estado"].Value.ToString();
                
            }






        }
    }
}
