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
    public partial class frmPaises : Form
    {
        public frmPaises()
        {
            InitializeComponent();
        }

        private void cargarPaises()
        {
            using (var db = new toursEntities())
            {
                var paises = db.Paises
                    .Select(p => new
                    {
                        p.PaisId,
                        p.Nombre
                    })
                    .ToList();

                dgvPaises.DataSource = paises;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            cargarPaises();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            using (var db = new toursEntities())
            {
                var nuevo = new Paises
                {
                    Nombre = txtNombre.Text
                };

                db.Paises.Add(nuevo);
                db.SaveChanges();
            }

            MessageBox.Show("País agregado correctamente.");
            cargarPaises();
            limpiarCampos();


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un país de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvPaises.CurrentRow.Cells["PaisId"].Value);

            using (var db = new toursEntities())
            {
                var pais = db.Paises.FirstOrDefault(p => p.PaisId == id);

                if (pais != null)
                {
                    pais.Nombre = txtNombre.Text;
                    db.SaveChanges();
                    MessageBox.Show("País actualizado correctamente.");
                }
            }

            cargarPaises();
            limpiarCampos();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un país de la lista.");
                return;
            }

            int id = Convert.ToInt32(dgvPaises.CurrentRow.Cells["PaisId"].Value);

            using (var db = new toursEntities())
            {
                var pais = db.Paises.FirstOrDefault(p => p.PaisId == id);

                if (pais != null)
                {
                    db.Paises.Remove(pais);
                    db.SaveChanges();
                    MessageBox.Show("País eliminado correctamente.");
                }
            }

            cargarPaises();
            limpiarCampos();


        }

        private void dgvPaises_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow != null)
            {
                txtNombre.Text = dgvPaises.CurrentRow.Cells["Nombre"].Value.ToString();
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar países en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Países"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(2); // ID y Nombre
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");

                    foreach (DataGridViewRow fila in dgvPaises.Rows)
                    {
                        if (fila.Cells["PaisId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["PaisId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
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
                saveFileDialog.Title = "Guardar países en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("PaisId,Nombre");

                        foreach (DataGridViewRow fila in dgvPaises.Rows)
                        {
                            if (fila.Cells["PaisId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["PaisId"].Value.ToString() + "," +
                                    fila.Cells["Nombre"].Value.ToString();

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
