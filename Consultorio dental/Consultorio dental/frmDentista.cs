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

namespace Consultorio_dental
{
    public partial class frmDentista : Form
    {
        public frmDentista()
        {
            InitializeComponent();
        }

        private void CargarDentistas()
        {
            using var db = new ConsultorioContext();

            dgvDentistas.DataSource = db.Dentista
                .Select(d => new
                {
                    d.DentistaId,
                    d.Nombre,
                    d.Especialidad
                })
                .ToList();
        }

        private void frmDentista_Load(object sender, EventArgs e)
        {
            CargarDentistas();
            txtNombre.Clear();
            txtEspecialidad.Clear();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del dentista.");
                    txtNombre.Focus();
                    return;

                }

                if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
                {
                    MessageBox.Show("Debe ingresar la especialidad del dentista.");
                    txtEspecialidad.Focus();
                    return;
                }

                using var db = new ConsultorioContext();

                var nuevoDentista = new Dentistum
                {
                    Nombre = txtNombre.Text.Trim(),
                    Especialidad = txtEspecialidad.Text.Trim()
                };

                db.Dentista.Add(nuevoDentista);
                db.SaveChanges();

                MessageBox.Show("Dentista agregado correctamente");
                CargarDentistas();


                txtNombre.Clear();
                txtEspecialidad.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar dentista: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvDentistas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un dentista en la tabla.");
                    return;
                }


                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del dentista.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
                {
                    MessageBox.Show("Debe ingresar la especialidad del dentista.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvDentistas.CurrentRow.Cells["DentistaId"].Value;
                var dentista = db.Dentista.Find(id);

                if (dentista != null)
                {
                    dentista.Nombre = txtNombre.Text.Trim();
                    dentista.Especialidad = txtEspecialidad.Text.Trim();

                    db.SaveChanges();

                    MessageBox.Show("Dentista actualizado correctamente");
                    CargarDentistas();


                    txtNombre.Clear();
                    txtEspecialidad.Clear();
                }
                else
                {
                    MessageBox.Show("No se encontró el dentista seleccionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar dentista: " + ex.Message);
            }


        }

        private void dgvDentistas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDentistas.CurrentRow != null)
            {
                if (dgvDentistas.CurrentRow.Cells["Nombre"].Value != null)
                    txtNombre.Text = dgvDentistas.CurrentRow.Cells["Nombre"].Value.ToString();

                if (dgvDentistas.CurrentRow.Cells["Especialidad"].Value != null)
                    txtEspecialidad.Text = dgvDentistas.CurrentRow.Cells["Especialidad"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvDentistas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un dentista en la tabla.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvDentistas.CurrentRow.Cells["DentistaId"].Value;
                var dentista = db.Dentista.Find(id);

                if (dentista != null)
                {

                    var confirmacion = MessageBox.Show(
                        "¿Está seguro que desea eliminar este dentista?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmacion == DialogResult.Yes)
                    {
                        db.Dentista.Remove(dentista);
                        db.SaveChanges();

                        MessageBox.Show("Dentista eliminado correctamente");
                        CargarDentistas();


                        txtNombre.Clear();
                        txtEspecialidad.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el dentista seleccionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar dentista: " + ex.Message);
            }


        }
        private void ExportarDentistasCSV(string rutaArchivo)
        {
            using (var sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                // Encabezados
                var encabezados = string.Join(",", dgvDentistas.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => $"\"{c.HeaderText}\""));
                sw.WriteLine(encabezados);

                // Filas
                foreach (DataGridViewRow fila in dgvDentistas.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        var valores = string.Join(",", fila.Cells
                            .Cast<DataGridViewCell>()
                            .Select(c => $"\"{c.Value?.ToString()}\""));
                        sw.WriteLine(valores);
                    }
                }
            }

            MessageBox.Show("Dentistas exportados a CSV correctamente");
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Guardar listado de dentistas";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarDentistasCSV(saveFileDialog.FileName);
            }


        }

        private void ExportarDentistasPDF(string rutaArchivo)
        {
            using (var fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var doc = new iTextSharp.text.Document())
            using (var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs))
            {
                doc.Open();

                // Título
                doc.Add(new iTextSharp.text.Paragraph("Listado de Dentistas"));
                doc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                // Tabla PDF con las columnas como el DataGridView
                var tabla = new iTextSharp.text.pdf.PdfPTable(dgvDentistas.Columns.Count);

                // Encabezados
                foreach (DataGridViewColumn col in dgvDentistas.Columns)
                {
                    tabla.AddCell(new iTextSharp.text.Phrase(col.HeaderText));
                }

                // Filas
                foreach (DataGridViewRow fila in dgvDentistas.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            tabla.AddCell(new iTextSharp.text.Phrase(celda.Value?.ToString()));
                        }
                    }
                }

                doc.Add(tabla);
                doc.Close();
            }

            MessageBox.Show("Dentistas exportados a PDF correctamente");
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = "Guardar listado de dentistas";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarDentistasPDF(saveFileDialog.FileName);
            }


        }
    }
}
