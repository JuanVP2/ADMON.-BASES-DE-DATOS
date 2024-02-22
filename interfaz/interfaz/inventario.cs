using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Datos;
using Modelos;
using System.Collections.Generic;

namespace interfaz
{

	public partial class FormInventario : Form
	{
		private MySqlConnection conexion;

        private Boolean flag;
        Inventario inventario;
        public FormInventario(Boolean flag, Inventario inventario)
        {
			
			InitializeComponent();
            //conexion = new MySqlConnection("");
            txtnombre.MaxLength = 45; // Máximo 45 caracteres
            txtdescripcion.MaxLength = 45; // Máximo 45 caracteres
            txtserie.MaxLength = 45; // Máximo 45 caracteres
            txtcolor.MaxLength = 45; // Máximo 45 caracteres
            txtdquisicion.MaxLength = 45; // Máximo 45 caracteres
            txtobservaciones.MaxLength = 45; // Máximo 45 caracteres

            this.flag = flag;
            this.inventario = inventario;
            if (flag == false)
            {
                this.Text = "Agregar Inventario";
            }
            else
            {
                this.Text = "Editar Inventario";
                txtnombre.Text = inventario.nombreCorto;
                txtserie.Text = inventario.serie;
                txtdescripcion.Text = inventario.descripcion;
                txtobservaciones.Text = inventario.observaciones;
                txtdquisicion.Text = inventario.tipoAdquision;
                txtcolor.Text = inventario.color;
                dateTimePicker1.Text = inventario.fechaAdquision;
               
            }
            // Llenar el combo con las áreas
            List<Areas> listaAreas = AreasDAO.obtenerAreas();

            cmbArea.DataSource = listaAreas;
            cmbArea.DisplayMember = "id";
            cmbArea.DisplayMember = "Nombre";
            cmbArea.Refresh();

            // Seleccionar el área del inventario (opcional)
            if (flag == true)
            {
                int indexArea = listaAreas.FindIndex(x => x.Id == inventario.areasId);
                cmbArea.SelectedIndex = indexArea;
            }
        }
		 
		private void FormInventario_Load(object sender, EventArgs e)
        {
            //CargarAreas();
            inventarioDAO inventario = new inventarioDAO();
            List<Inventario> inv = inventario.obtenerInventario();

            // txtid.Text = inv[0].nombreCorto.ToString();
            //txtid.Text = "holaa";

            //inventario.agregar(new Inventario(4, "Producto4", "Descripción del Producto 4", "Serie789", "Verde", 
            //"2022-02-18", "Compra", "Observaciones Producto 4", 3));

            // inventario.actualizar(new Inventario(4, "sjfjsj", "Descripción del Producto 4", "Serie789", "Verde", 
            // "2022-02-18", "Compra", "Observaciones Producto 4", 3));

            //inventario.eliminar(4);


        }



        private List<int> ObtenerIdsAreasDisponibles()
        {
            // Crear una lista para almacenar los IDs de áreas disponibles
            List<int> idsAreasDisponibles = new List<int>();

            // Obtener la lista de áreas disponibles utilizando el método estático obtenerAreas de la clase AreasDAO
            List<Areas> areasDisponibles = AreasDAO.obtenerAreas();

            // Agregar los IDs de áreas a la lista
            foreach (var area in areasDisponibles)
            {
                idsAreasDisponibles.Add(area.Id);
            }

            return idsAreasDisponibles;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            int idInventario = Convert.ToInt32(this.Tag); // Obtiene el ID del inventario a modificar

            string nombreCorto = txtnombre.Text;
            string descripcion = txtdescripcion.Text;
            string serie = txtserie.Text;
            string color = txtcolor.Text;
            string fechaAdquisicion = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string tipoAdquisicion = txtdquisicion.Text;
            string observaciones = txtobservaciones.Text;
            Areas a = (Areas)(cmbArea.SelectedValue);
            int areasId = (a.Id);
            // Crear un objeto Inventario con los datos actualizados
            Inventario nuevoInventario = new Inventario(idInventario, nombreCorto, descripcion, serie, color, fechaAdquisicion, tipoAdquisicion, observaciones, areasId);
            
            // Actualizar el inventario en la base de datos utilizando el método actualizar de inventarioDAO
            inventarioDAO inventarioDao = new inventarioDAO();
            
            bool actualizado;
            
            if (flag == false)
            {
                inventarioDao.agregar(nuevoInventario);
                actualizado = inventarioDao.actualizar(nuevoInventario);
            }
            else
            {
                actualizado = inventarioDao.actualizar(nuevoInventario);
            }


            

            if (actualizado)
            {
                MessageBox.Show("Inventario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cierra el formulario actual
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

