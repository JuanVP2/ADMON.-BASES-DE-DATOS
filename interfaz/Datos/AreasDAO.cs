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
    public class AreasDAO
    {
        public static List<Areas> obtenerAreas()
        {
            List<Areas> lista = new List<Areas>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia select
                    String select = "select id, nombre, ubicacion from areas;";
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

                        Areas area = new Areas(
                             Convert.ToInt32(fila["id"]),
                             fila["nombre"].ToString(),
                             fila["ubicacion"].ToString()
                            );
                        lista.Add(area);
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
    }
}