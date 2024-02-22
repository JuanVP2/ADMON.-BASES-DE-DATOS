using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Areas
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string ubicacion { get; set; }

        public Areas(int id, string nombre, string ubicacion)
        {
            this.Id = id;
            this.nombre = nombre;
            this.ubicacion = ubicacion;
        }
    }
}

