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
    public partial class frmMotivo : Form
    {
        public frmMotivo()
        {
            InitializeComponent();
        }




        private void CargarMotivos()
        {
            using var db = new ConsultorioContext();

            dgvMotivos.DataSource = db.Motivos
                .Select(m => new
                {
                    m.MotivoId,
                    m.Descripcion
                })
                .ToList();
        }

        private void frmMotivo_Load(object sender, EventArgs e)
        {
            CargarMotivos();
            txtDescripcion.Clear();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el usuario haya escrito algo
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar una descripción para el motivo.");
                    return;
                }

                using var db = new ConsultorioContext();

                var nuevoMotivo = new Motivo
                {
                    Descripcion = txtDescripcion.Text.Trim()
                };

                db.Motivos.Add(nuevoMotivo);
                db.SaveChanges();

                MessageBox.Show("Motivo agregado correctamente");
                CargarMotivos(); // aqui refresco el dataGridView

                // limpio controles despues insertar
                txtDescripcion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar motivo: " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya una fila seleccionada en el DataGridView
                if (dgvMotivos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un motivo en la tabla.");
                    return;
                }

                // Validar que el TextBox no esté vacío
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar una descripción para el motivo.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvMotivos.CurrentRow.Cells["MotivoId"].Value;
                var motivo = db.Motivos.Find(id);

                if (motivo != null)
                {
                    motivo.Descripcion = txtDescripcion.Text.Trim();
                    db.SaveChanges();

                    MessageBox.Show("Motivo actualizado correctamente");
                    CargarMotivos();

                    // 🔹 Limpiar controles después de actualizar
                    txtDescripcion.Clear();
                }
                else
                {
                    MessageBox.Show("No se encontró el motivo seleccionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar motivo: " + ex.Message);
            }


        }

        private void dgvMotivos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMotivos.CurrentRow != null && dgvMotivos.CurrentRow.Cells["Descripcion"].Value != null)
            {
                txtDescripcion.Text = dgvMotivos.CurrentRow.Cells["Descripcion"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvMotivos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un motivo en la tabla.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvMotivos.CurrentRow.Cells["MotivoId"].Value;
                var motivo = db.Motivos.Find(id);

                if (motivo != null)
                {
                    // Confirmación antes de eliminar
                    var confirmacion = MessageBox.Show(
                        "¿Está seguro que desea eliminar este motivo?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmacion == DialogResult.Yes)
                    {
                        db.Motivos.Remove(motivo);
                        db.SaveChanges();

                        MessageBox.Show("Motivo eliminado correctamente");
                        CargarMotivos();

                        // Limpio controles después de eliminar
                        txtDescripcion.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el motivo seleccionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar motivo: " + ex.Message);
            }



        }

        private void ExportarMotivosPDF(string rutaArchivo)
        {
            using (var fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var doc = new iTextSharp.text.Document())
            using (var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs))
            {
                doc.Open();


                doc.Add(new iTextSharp.text.Paragraph("Listado de Motivos"));
                doc.Add(new iTextSharp.text.Paragraph(" ")); // espacio


                var tabla = new iTextSharp.text.pdf.PdfPTable(dgvMotivos.Columns.Count);


                foreach (DataGridViewColumn col in dgvMotivos.Columns)
                {
                    tabla.AddCell(new iTextSharp.text.Phrase(col.HeaderText));
                }


                foreach (DataGridViewRow fila in dgvMotivos.Rows)
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

            MessageBox.Show("Motivos exportados a PDF correctamente");
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = "Guardar listado de motivos";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarMotivosPDF(saveFileDialog.FileName);
            }


        }

        private void ExportarMotivosCSV(string rutaArchivo)
        {
            using (var sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                // Encabezados
                var encabezados = string.Join(",", dgvMotivos.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => $"\"{c.HeaderText}\""));
                sw.WriteLine(encabezados);

                // Filas
                foreach (DataGridViewRow fila in dgvMotivos.Rows)
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

            MessageBox.Show("Motivos exportados a CSV correctamente");
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Guardar listado de motivos";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarMotivosCSV(saveFileDialog.FileName);
            }


        }
    }





}
