using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
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
    public partial class empleados : Form
    {
        public empleados()
        {
            InitializeComponent();
        }

        private void cargarEmpleados()
        {
            using (var db = new empleadosEntities())
            {
                var empleados = db.Empleados
                    .Select(e => new
                    {
                        e.EmpleadoId,
                        e.Nombre,
                        Departamento = e.Departamentos.Nombre,
                        Cargo = e.Cargos.Nombre,
                        e.FechaInicio,
                        e.Salario,
                        e.Estado,
                        Tiempo = SqlFunctions.DateDiff("year", e.FechaInicio, DateTime.Now) + " años, " +
                                 SqlFunctions.DateDiff("month", e.FechaInicio, DateTime.Now) % 12 + " meses",
                        AFP = e.Salario * 0.0287m,
                        ARS = e.Salario * 0.0304m,
                        ISR = (e.Salario > 34000 ? e.Salario * 0.15m : 0)
                    })
                    .ToList();

                dgvEmpleados.DataSource = empleados;
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            cmbDepartamentos.SelectedIndex = -1;
            cmbCargos.SelectedIndex = -1;
            numSalario.Value = 0;
            dtpFechaInicio.Value = DateTime.Now;
            cmbEstado.SelectedIndex = -1;
        }



        private void empleados_Load(object sender, EventArgs e)
        {
            cargarEmpleados();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbDepartamentos.SelectedIndex == -1 ||
                    cmbCargos.SelectedIndex == -1 ||
                    cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                using (var db = new empleadosEntities())
                {
                    var nuevo = new Empleados
                    {
                        Nombre = txtNombre.Text,
                        DepartamentoId = (int)cmbDepartamentos.SelectedValue,
                        CargoId = (int)cmbCargos.SelectedValue,
                        FechaInicio = dtpFechaInicio.Value,
                        Salario = numSalario.Value,
                        Estado = cmbEstado.Text
                    };

                    db.Empleados.Add(nuevo);
                    db.SaveChanges();
                }

                MessageBox.Show("Empleado agregado correctamente.");
                cargarEmpleados();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar empleado: " + ex.Message);
            }


        }

        private void dgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmpleados.CurrentRow != null)
            {
                txtNombre.Text = dgvEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
                cmbDepartamentos.Text = dgvEmpleados.CurrentRow.Cells["Departamento"].Value.ToString();
                cmbCargos.Text = dgvEmpleados.CurrentRow.Cells["Cargo"].Value.ToString();
                dtpFechaInicio.Value = Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["FechaInicio"].Value);
                numSalario.Value = Convert.ToDecimal(dgvEmpleados.CurrentRow.Cells["Salario"].Value);
                cmbEstado.Text = dgvEmpleados.CurrentRow.Cells["Estado"].Value.ToString();
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmpleados.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un empleado de la lista.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbDepartamentos.SelectedIndex == -1 ||
                    cmbCargos.SelectedIndex == -1 ||
                    cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                int id = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["EmpleadoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var empleado = db.Empleados.FirstOrDefault(u => u.EmpleadoId == id);

                    if (empleado != null)
                    {
                        empleado.Nombre = txtNombre.Text;
                        empleado.DepartamentoId = (int)cmbDepartamentos.SelectedValue;
                        empleado.CargoId = (int)cmbCargos.SelectedValue;
                        empleado.FechaInicio = dtpFechaInicio.Value;
                        empleado.Salario = numSalario.Value;
                        empleado.Estado = cmbEstado.Text;

                        db.SaveChanges();
                        MessageBox.Show("Empleado actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado.");
                    }
                }

                cargarEmpleados();   
                limpiarCampos();     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmpleados.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un empleado de la lista.");
                    return;
                }

                int id = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["EmpleadoId"].Value);

                using (var db = new empleadosEntities())
                {
                    var empleado = db.Empleados.FirstOrDefault(u => u.EmpleadoId == id);

                    if (empleado != null)
                    {
                        db.Empleados.Remove(empleado);
                        db.SaveChanges();
                        MessageBox.Show("Empleado eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado.");
                    }
                }

                cargarEmpleados();   // refresca el DataGridView
                limpiarCampos();     // limpia los controles
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar empleado: " + ex.Message);
            }


        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar empleados en PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    Document doc = new Document(PageSize.A4.Rotate()); // horizontal para más columnas
                    PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                    doc.Open();

                    doc.Add(new Paragraph("Lista de Empleados"));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(11); // 11 columnas
                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Departamento");
                    tabla.AddCell("Cargo");
                    tabla.AddCell("Fecha Inicio");
                    tabla.AddCell("Salario");
                    tabla.AddCell("Estado");
                    tabla.AddCell("Tiempo");
                    tabla.AddCell("AFP");
                    tabla.AddCell("ARS");
                    tabla.AddCell("ISR");

                    foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                    {
                        if (fila.Cells["EmpleadoId"].Value != null)
                        {
                            tabla.AddCell(fila.Cells["EmpleadoId"].Value.ToString());
                            tabla.AddCell(fila.Cells["Nombre"].Value.ToString());
                            tabla.AddCell(fila.Cells["Departamento"].Value.ToString());
                            tabla.AddCell(fila.Cells["Cargo"].Value.ToString());
                            tabla.AddCell(fila.Cells["FechaInicio"].Value.ToString());
                            tabla.AddCell(fila.Cells["Salario"].Value.ToString());
                            tabla.AddCell(fila.Cells["Estado"].Value.ToString());
                            tabla.AddCell(fila.Cells["Tiempo"].Value.ToString());
                            tabla.AddCell(fila.Cells["AFP"].Value.ToString());
                            tabla.AddCell(fila.Cells["ARS"].Value.ToString());
                            tabla.AddCell(fila.Cells["ISR"].Value.ToString());
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
                saveFileDialog.Title = "Guardar empleados en CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        sw.WriteLine("EmpleadoId,Nombre,Departamento,Cargo,FechaInicio,Salario,Estado,Tiempo,AFP,ARS,ISR");

                        foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                        {
                            if (fila.Cells["EmpleadoId"].Value != null)
                            {
                                string linea =
                                    fila.Cells["EmpleadoId"].Value.ToString() + "," +
                                    fila.Cells["Nombre"].Value.ToString() + "," +
                                    fila.Cells["Departamento"].Value.ToString() + "," +
                                    fila.Cells["Cargo"].Value.ToString() + "," +
                                    fila.Cells["FechaInicio"].Value.ToString() + "," +
                                    fila.Cells["Salario"].Value.ToString() + "," +
                                    fila.Cells["Estado"].Value.ToString() + "," +
                                    fila.Cells["Tiempo"].Value.ToString() + "," +
                                    fila.Cells["AFP"].Value.ToString() + "," +
                                    fila.Cells["ARS"].Value.ToString() + "," +
                                    fila.Cells["ISR"].Value.ToString();

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
