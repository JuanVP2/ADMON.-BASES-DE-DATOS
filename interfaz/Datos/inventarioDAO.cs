using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Modelos;
using System.Data;


namespace Datos
{
    public class inventarioDAO
    {
        public List<Inventario> obtenerInventario()
        {
            List<Inventario> lista = new List<Inventario>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia select
                    String select = "select id, nombreCorto,descripcion," +
                        "serie,color, fechaAdquision,tipoAdquision,observaciones,areas_id from inventario;";
                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = Conexion.conexion;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;

                    //Llenar el datatable
                    da.Fill(dt);
                    //Crear un objeto categoría por cada fila de la tabla y añadirlo a la lista
                    foreach (DataRow fila in dt.Rows)
                    {
                       /* int reportsTo = 0; // Valor predeterminado en caso de DBNull
                        if (!Convert.IsDBNull(fila["ReportsTo"]))
                        {
                            reportsTo = Convert.ToInt32(fila["ReportsTo"]);
                        }*/
                        Inventario inventario = new Inventario(
                             Convert.ToInt32(fila["id"]),
                             fila["nombreCorto"].ToString(),
                             fila["descripcion"].ToString(),
                             fila["serie"].ToString(),
                             fila["color"].ToString(),
                             fila["fechaAdquision"].ToString(),
                             fila["tipoAdquision"].ToString(),
                             fila["observaciones"].ToString(),
                             Convert.ToInt32(fila["areas_id"])
                            );
                        lista.Add(inventario);
                    }
                    return lista;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return null;
            }
        }
        public bool agregar(Inventario inv)
        {
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia insert
                    string insert = "INSERT INTO inventario (id, nombreCorto, descripcion," +
                        "serie,color,fechaAdquision,tipoAdquision,observaciones," +
                        "areas_id) VALUES (@id, @nombreCorto,@descripcion,@serie,@color,@fechaAdquision," +
                        "@tipoAdquision,@observaciones,@areas_id);";

                    MySqlCommand sentencia = new MySqlCommand(insert, Conexion.conexion);

                    //Parametros
                    sentencia.Parameters.AddWithValue("@id", inv.id);
                    sentencia.Parameters.AddWithValue("@nombreCorto", inv.nombreCorto);
                    sentencia.Parameters.AddWithValue("@descripcion", inv.descripcion);
                    sentencia.Parameters.AddWithValue("@serie", inv.serie);
                    sentencia.Parameters.AddWithValue("@color", inv.color);
                    sentencia.Parameters.AddWithValue("@fechaAdquision", inv.fechaAdquision);
                    sentencia.Parameters.AddWithValue("@tipoAdquision", inv.tipoAdquision);
                    sentencia.Parameters.AddWithValue("@observaciones", inv.observaciones);
                    sentencia.Parameters.AddWithValue("@areas_id", inv.areasId);

                    //Ejecutar la sentencia
                    int filasAfectadas = sentencia.ExecuteNonQuery();

                    //Verificar si se insertaron filas
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Datos insertados correctamente.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No se insertaron datos.");
                        return false;
                    }
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return false;
            }
        }

        public bool actualizar(Inventario inv)
        {
            if (Conexion.Conectar())
            {
                try
                {
                    // Crear la sentencia UPDATE
                    string update = "UPDATE inventario SET " +
                                    "nombreCorto = @nombreCorto, " +
                                    "descripcion = @descripcion, " +
                                    "serie = @serie, " +
                                    "color = @color, " +
                                    "fechaAdquision = @fechaAdquision, " +
                                    "tipoAdquision = @tipoAdquision, " +
                                    "observaciones = @observaciones, " +
                                    "areas_id = @areas_id " +
                                    "WHERE id = @id;";

                    MySqlCommand sentencia = new MySqlCommand(update, Conexion.conexion);

                    // Parámetros
                    sentencia.Parameters.AddWithValue("@nombreCorto", inv.nombreCorto);
                    sentencia.Parameters.AddWithValue("@descripcion", inv.descripcion);
                    sentencia.Parameters.AddWithValue("@serie", inv.serie);
                    sentencia.Parameters.AddWithValue("@color", inv.color);
                    sentencia.Parameters.AddWithValue("@fechaAdquision", inv.fechaAdquision);
                    sentencia.Parameters.AddWithValue("@tipoAdquision", inv.tipoAdquision);
                    sentencia.Parameters.AddWithValue("@observaciones", inv.observaciones);
                    sentencia.Parameters.AddWithValue("@areas_id", inv.areasId);
                    sentencia.Parameters.AddWithValue("@id", inv.id);

                    // Ejecutar la sentencia
                    int filasAfectadas = sentencia.ExecuteNonQuery();

                    // Verificar si se actualizaron filas
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Datos actualizados correctamente.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No se actualizaron datos. Puede ser que el ID no exista.");
                        return false;
                    }
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return false;
            }
        }
        private List<int> ObtenerIdsAreasDisponibles()
        {
            // Crear una lista para almacenar los IDs de áreas disponibles
            List<int> idsAreasDisponibles = new List<int>();

            // Crear una instancia de AreasDAO
            AreasDAO areasDAO = new AreasDAO();

            // Llamar al método obtenerAreas() en la instancia de AreasDAO
            List<Areas> areasDisponibles = AreasDAO.obtenerAreas();

            // Agregar los IDs de áreas a la lista
            foreach (var area in areasDisponibles)
            {
                idsAreasDisponibles.Add(area.Id);
            }

            return idsAreasDisponibles;
        }

        public bool eliminar(int id)
        {
            if (Conexion.Conectar())
            {
                try
                {
                    // Crear la sentencia DELETE
                    string delete = "DELETE FROM inventario WHERE id = @id;";

                    MySqlCommand sentencia = new MySqlCommand(delete, Conexion.conexion);

                    // Parámetro
                    sentencia.Parameters.AddWithValue("@id", id);

                    // Ejecutar la sentencia
                    int filasAfectadas = sentencia.ExecuteNonQuery();

                    // Verificar si se eliminó alguna fila
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Datos eliminados correctamente.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No se eliminó ningún dato.");
                        return false;
                    }
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return false;
            }
        }


    }

}
