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
    public partial class departamento : Form
    {
        public departamento()
        {
            InitializeComponent();
        }

        private void cargarDepartamentos()
        {
            using (var db = new empleadosEntities())
            {
                var departamentos = db.Departamentos
                    .Select(d => new
                    {
                        d.DepartamentoId,
                        d.Nombre
                    })
                    .ToList();

                dgvDepartamentos.DataSource = departamentos;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
        }

        private void departamento_Load(object sender, EventArgs e)
        {
            cargarDepartamentos();
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
                    var nuevo = new Departamentos
                    {
                        Nombre = txtNombre.Text
                    };

                    db.Departamentos.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Departamento agregado correctamente.");
                cargarDepartamentos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar departamento: " + ex.Message);
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

                if (dgvDepartamentos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un departamento de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvDepartamentos.CurrentRow.Cells["DepartamentoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var departamento = db.Departamentos.FirstOrDefault(d => d.DepartamentoId == id);

                    if (departamento != null)
                    {
                        departamento.Nombre = txtNombre.Text;
                        db.SaveChanges();
                        MessageBox.Show("Departamento actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Departamento no encontrado.");
                    }
                }

                cargarDepartamentos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar departamento: " + ex.Message);
            }


        }

        private void dgvDepartamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartamentos.CurrentRow != null)
            {
                txtNombre.Text = dgvDepartamentos.CurrentRow.Cells["Nombre"].Value.ToString();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDepartamentos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un departamento de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvDepartamentos.CurrentRow.Cells["DepartamentoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var departamento = db.Departamentos.FirstOrDefault(d => d.DepartamentoId == id);

                    if (departamento != null)
                    {
                        db.Departamentos.Remove(departamento);
                        db.SaveChanges();
                        MessageBox.Show("Departamento eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Departamento no encontrado.");
                    }
                }

                cargarDepartamentos();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar departamento: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar departamentos en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Departamentos"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(2); // ID y Nombre
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");

                    foreach (DataGridViewRow fila in dgvDepartamentos.Rows)
                    {
                        if (fila.Cells["DepartamentoId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["DepartamentoId"].Value.ToString());
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
                saveFileDialog.Title = "Guardar departamentos en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("DepartamentoId,Nombre");

                        foreach (DataGridViewRow fila in dgvDepartamentos.Rows)
                        {
                            if (fila.Cells["DepartamentoId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["DepartamentoId"].Value.ToString() + "," +
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
