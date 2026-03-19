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
    public partial class frmCita : Form
    {
        public frmCita()
        {
            InitializeComponent();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbPaciente.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un paciente.");
                    return;
                }
                if (cmbDentista.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un dentista.");
                    return;
                }
                if (cmbMotivo.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un motivo.");
                    return;
                }

                using var db = new ConsultorioContext();

                var nuevaCita = new Citum
                {
                    PacienteId = (int)cmbPaciente.SelectedValue,
                    DentistaId = (int)cmbDentista.SelectedValue,
                    MotivoId = (int)cmbMotivo.SelectedValue,
                    Fecha = DateOnly.FromDateTime(dtpFecha.Value)
                };

                db.Cita.Add(nuevaCita);
                db.SaveChanges();

                MessageBox.Show("Cita agregada correctamente");
                CargarCitas();

                // para Limpiar controles
                cmbPaciente.SelectedIndex = -1;
                cmbDentista.SelectedIndex = -1;
                cmbMotivo.SelectedIndex = -1;
                dtpFecha.Value = DateTime.Now;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar cita: " + ex.Message);
            }



        }


        private void CargarCitas()
        {
            using var db = new ConsultorioContext();

            dgvCitas.DataSource = db.Cita
                .Select(c => new
                {
                    c.CitaId,
                    c.PacienteId,
                    c.DentistaId,
                    c.MotivoId,
                    c.Fecha,
                    Estado = c.Fecha.ToDateTime(TimeOnly.MinValue) > DateTime.Now ? "Pendiente" : "Vencida",
                    TiempoRestante = c.Fecha.ToDateTime(TimeOnly.MinValue) > DateTime.Now
                        ? (c.Fecha.ToDateTime(TimeOnly.MinValue) - DateTime.Now).Days + " días"
                        : "0"
                })
                .ToList();
        }

        private void frmCita_Load(object sender, EventArgs e)
        {
            CargarCitas();
            using var db = new ConsultorioContext();

            // Pacientes
            cmbPaciente.DataSource = db.Pacientes.ToList();
            cmbPaciente.DisplayMember = "Nombre";      // lo que se ve en la lista
            cmbPaciente.ValueMember = "PacienteId";    // el ID que se usa internamente
            cmbPaciente.SelectedIndex = -1;            // para que arranque vacío

            // Dentistas
            cmbDentista.DataSource = db.Dentista.ToList();
            cmbDentista.DisplayMember = "Nombre";
            cmbDentista.ValueMember = "DentistaId";
            cmbDentista.SelectedIndex = -1;

            // Motivos
            cmbMotivo.DataSource = db.Motivos.ToList();
            cmbMotivo.DisplayMember = "Descripcion";   // o el campo que tengas
            cmbMotivo.ValueMember = "MotivoId";
            cmbMotivo.SelectedIndex = -1;


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya una fila seleccionada
                if (dgvCitas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una cita en la tabla.");
                    return;
                }

                // Validar que los combos tengan valores
                if (cmbPaciente.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un paciente.");
                    return;
                }
                if (cmbDentista.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un dentista.");
                    return;
                }
                if (cmbMotivo.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un motivo.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvCitas.CurrentRow.Cells["CitaId"].Value;
                var cita = db.Cita.Find(id);

                if (cita != null)
                {
                    cita.PacienteId = (int)cmbPaciente.SelectedValue;
                    cita.DentistaId = (int)cmbDentista.SelectedValue;
                    cita.MotivoId = (int)cmbMotivo.SelectedValue;
                    cita.Fecha = DateOnly.FromDateTime(dtpFecha.Value);

                    db.SaveChanges();
                    MessageBox.Show("Cita actualizada correctamente");
                    CargarCitas();

                    // Limpiar controles después de actualizar
                    cmbPaciente.SelectedIndex = -1;
                    cmbDentista.SelectedIndex = -1;
                    cmbMotivo.SelectedIndex = -1;
                    dtpFecha.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("No se encontró la cita seleccionada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar cita: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya una fila seleccionada
                if (dgvCitas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una cita en la tabla.");
                    return;
                }

                using var db = new ConsultorioContext();

                int id = (int)dgvCitas.CurrentRow.Cells["CitaId"].Value;
                var cita = db.Cita.Find(id);

                if (cita != null)
                {
                    // Confirmación antes de eliminar
                    var confirmacion = MessageBox.Show(
                        "¿Está seguro que desea eliminar esta cita?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmacion == DialogResult.Yes)
                    {
                        db.Cita.Remove(cita);
                        db.SaveChanges();
                        MessageBox.Show("Cita eliminada correctamente");
                        CargarCitas();

                        // 🔹 Limpiar controles después de eliminar
                        cmbPaciente.SelectedIndex = -1;
                        cmbDentista.SelectedIndex = -1;
                        cmbMotivo.SelectedIndex = -1;
                        dtpFecha.Value = DateTime.Now;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró la cita seleccionada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cita: " + ex.Message);
            }


        }

        private void ExportarCitasCSV(string rutaArchivo)
        {
            using (var sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                // Encabezados
                var encabezados = string.Join(",", dgvCitas.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => $"\"{c.HeaderText}\""));
                sw.WriteLine(encabezados);

                // Filas
                foreach (DataGridViewRow fila in dgvCitas.Rows)
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

            MessageBox.Show("Citas exportadas a CSV correctamente");
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Guardar listado de citas";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarCitasCSV(saveFileDialog.FileName);
            }


        }
        private void ExportarCitasPDF(string rutaArchivo)
        {
            using (var fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var doc = new iTextSharp.text.Document())
            using (var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs))
            {
                doc.Open();

                // Título
                doc.Add(new iTextSharp.text.Paragraph("Listado de Citas"));
                doc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                // Tabla PDF
                var tabla = new iTextSharp.text.pdf.PdfPTable(dgvCitas.Columns.Count);

                // Encabezados
                foreach (DataGridViewColumn col in dgvCitas.Columns)
                {
                    tabla.AddCell(new iTextSharp.text.Phrase(col.HeaderText));
                }

                // Filas
                foreach (DataGridViewRow fila in dgvCitas.Rows)
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

            MessageBox.Show("Citas exportadas a PDF correctamente");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = "Guardar listado de citas";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarCitasPDF(saveFileDialog.FileName);
            }


        }
    }


}
