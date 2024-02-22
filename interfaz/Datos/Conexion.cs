using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Datos
{
    internal class Conexion
    {
        //clase para establecer Conexion con el servidor
        public static MySqlConnection conexion;

        public static bool Conectar()
        {
            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open) return true;

                conexion = new MySqlConnection();
                //Conexion.ConnectionString = "server=proyecto-tbd-ecm-jvp.mysql.database.azure.com;uid=tbd;pwd=Adminbd.;database=northwind";
                //Conexion.ConnectionString = "server=t-baseusuarios.mysql.database.azure.com;uid=RAdmin;pwd=GyD-2023;database=nwind";
                conexion.ConnectionString = "server=172.172.240.142;uid=juanulises;pwd=#123Usuario;database=inventario_tienda";
                //Conexion.ConnectionString = $"server={Settings.Default.Host};uid={Settings.Default.Usuario};pwd={Settings.Default.Password};database=northwind";
                //Conexion.ConnectionString = "server=" + ip + ";uid=" + usuario + ";pwd=" + usuario + ";database=northwind";
                conexion.Open();

                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void Desconectar()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }
    }
}
