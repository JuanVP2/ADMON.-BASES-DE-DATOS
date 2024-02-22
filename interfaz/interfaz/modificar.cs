using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Datos;
using Modelos;
using MySql.Data.MySqlClient;

namespace interfaz
{
    /// <summary>
    /// Description of modificar.
    /// </summary>
    /// 


    public partial class modificar : Form
    {
        List<Inventario> inventarios = new inventarioDAO().obtenerInventario();
        private MySqlConnection conexion;

        public modificar()
        {

            InitializeComponent();
            conexion = new MySqlConnection("");
            // MessageBox.Show("Datos crgdos " + inventarios.Count);

            dgvelementos.DataSource = inventarios;
            dgvelementos.Columns["id"].Visible = false;
            dgvelementos.Columns["areasid"].Visible = false;

        }
        private void FormAdministracion_Load(object sender, EventArgs e)
        {
            //CargarElementos();
            try
            {
                conexion.Open();
                string consulta = "SELECT id, nombreCorto, Descripcion,Serie ,Color , FechaAdquision, TipoAdquision, Observaciones FROM inventario";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataTable tablaElementos = new DataTable();
                adaptador.Fill(tablaElementos);

                dgvelementos.DataSource = tablaElementos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los elementos del inventario: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void CargarElementos()
        {
            try
            {
                conexion.Open();
                string consulta = "SELECT * FROM inventario";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataTable tablaElementos = new DataTable();
                adaptador.Fill(tablaElementos);

                dgvelementos.DataSource = tablaElementos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los elementos del inventaro" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }



        private void btnagregar_Click(object sender, EventArgs e)
        {
            FormInventario storeAddEdit = new FormInventario(false, null);
            storeAddEdit.FormClosed += FormAddEditClosed; // Suscribir al evento FormClosed
            storeAddEdit.ShowDialog();
        }

        private void FormAddEditClosed(object sender, FormClosedEventArgs e)
        {
            inventarios = new inventarioDAO().obtenerInventario();
            dgvelementos.DataSource = null; // Limpiar el origen de datos actual
            dgvelementos.DataSource = inventarios; // Establecer el nuevo origen de datos
        }

        private void actualizarTabla(List<Inventario> inventarios)
        {
            dgvelementos.Rows.Clear(); // Limpiar las filas existentes

            if (inventarios != null)
            {
                foreach (Inventario ti in inventarios)
                {
                    dgvelementos.Rows.Add(ti.id, ti.nombreCorto, ti.descripcion, ti.observaciones, ti.serie, ti.tipoAdquision, ti.fechaAdquision, ti.color);
                }
            }

        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay una fila seleccionada
                if (dgvelementos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, selecciona un inventario para editar.", "Editar Inventario",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                Inventario inventario = new Inventario(
                    Convert.ToInt32(dgvelementos.SelectedCells[0].Value),
                    dgvelementos.SelectedCells[1].Value.ToString(),
                    dgvelementos.SelectedCells[2].Value.ToString(),
                    dgvelementos.SelectedCells[3].Value.ToString(),
                    dgvelementos.SelectedCells[4].Value.ToString(),
                    dgvelementos.SelectedCells[5].Value.ToString(),
                    dgvelementos.SelectedCells[6].Value.ToString(),
                    dgvelementos.SelectedCells[7].Value.ToString(),
                    Convert.ToInt32(dgvelementos.SelectedCells[8].Value)
                );
                new inventarioDAO().actualizar(inventario);
                FormInventario frmInventario = new FormInventario(true, inventario);
                frmInventario.ShowDialog();

                // Actualizar el DataGridView después de modificar el inventario
                inventarios = new inventarioDAO().obtenerInventario();
                dgvelementos.DataSource = null;
                dgvelementos.DataSource = inventarios;
            }
            catch
            {
                MessageBox.Show("Error al editar inventario. No se ha seleccionado ningún inventario.", "Editar Inventario",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void btneliminar_Click(object sender, EventArgs e)
        {

            if (dgvelementos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un inventario para eliminar.", "Eliminar Inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int idInventarioEliminar = Convert.ToInt32(dgvelementos.SelectedRows[0].Cells[0].Value);

            DialogResult resultado = MessageBox.Show($"¿Estás seguro de eliminar el inventario con ID {idInventarioEliminar}?", "Eliminar Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                bool eliminado = new inventarioDAO().eliminar(idInventarioEliminar);

                if (eliminado)
                {
                    MessageBox.Show("Inventario eliminado exitosamente.", "Eliminar Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Actualizar el DataGridView después de eliminar
                    inventarios = new inventarioDAO().obtenerInventario();
                    dgvelementos.DataSource = null;
                    dgvelementos.DataSource = inventarios;
                }
                else
                {
                    MessageBox.Show("Error al eliminar el inventario.", "Eliminar Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}
