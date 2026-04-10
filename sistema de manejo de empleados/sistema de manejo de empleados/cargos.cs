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
using sistema_de_manejo_de_empleados.modelos;

namespace sistema_de_manejo_de_empleados
{
    public partial class cargos : Form
    {
        public cargos()
        {
            InitializeComponent();
        }

        private void cargarCargos()
        {
            using (var db = new empleadosEntities())
            {
                var cargos = db.Cargos
                    .Select(c => new
                    {
                        c.CargoId,
                        c.Nombre
                    })
                    .ToList();

                dgvCargos.DataSource = cargos;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
        }

        private void cargos_Load(object sender, EventArgs e)
        {
            cargarCargos();
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

                using (var db = new empleadosEntities())
                {
                    var nuevo = new Cargos
                    {
                        Nombre = txtNombre.Text
                    };

                    db.Cargos.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Cargo agregado correctamente.");
                cargarCargos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar cargo: " + ex.Message);
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

                if (dgvCargos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un cargo de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvCargos.CurrentRow.Cells["CargoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var cargo = db.Cargos.FirstOrDefault(c => c.CargoId == id);

                    if (cargo != null)
                    {
                        cargo.Nombre = txtNombre.Text;
                        db.SaveChanges();
                        MessageBox.Show("Cargo actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Cargo no encontrado.");
                    }
                }

                cargarCargos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar cargo: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCargos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un cargo de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvCargos.CurrentRow.Cells["CargoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var cargo = db.Cargos.FirstOrDefault(c => c.CargoId == id);

                    if (cargo != null)
                    {
                        db.Cargos.Remove(cargo);
                        db.SaveChanges();
                        MessageBox.Show("Cargo eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Cargo no encontrado.");
                    }
                }

                cargarCargos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cargo: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar cargos en PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ruta = saveFileDialog.FileName;

                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();

                doc.Add(new Paragraph("Lista de Cargos"));
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(2);
                tabla.AddCell("ID");
                tabla.AddCell("Nombre");

                foreach (DataGridViewRow fila in dgvCargos.Rows)
                {
                    if (fila.Cells["CargoId"].Value != null)
                    {
                        tabla.AddCell(fila.Cells["CargoId"].Value.ToString());
                        tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                    }
                }

                doc.Add(tabla);
                doc.Close();

                MessageBox.Show("PDF generado correctamente en: " + ruta);
            }


        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "Guardar cargos en CSV";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ruta = saveFileDialog.FileName;

                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    sw.WriteLine("CargoId,Nombre");

                    foreach (DataGridViewRow fila in dgvCargos.Rows)
                    {
                        if (fila.Cells["CargoId"].Value != null)
                        {
                            string linea =
                                fila.Cells["CargoId"].Value.ToString() + "," +
                                fila.Cells["Nombre"].Value.ToString();

                            sw.WriteLine(linea);
                        }
                    }
                }

                MessageBox.Show("CSV generado correctamente en: " + ruta);
            }


        }

        private void dgvCargos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCargos.CurrentRow != null)
            {
                
                txtNombre.Text = dgvCargos.CurrentRow.Cells["Nombre"].Value.ToString();
            }


        }
    }
}
