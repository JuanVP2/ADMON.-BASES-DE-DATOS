using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Inventario
    {
        public int id { get; set; }
        public string nombreCorto { get; set; }
        public string descripcion { get; set; }
        public string serie { get; set; }
        public string color { get; set; }
        public string fechaAdquision { get; set; }
        public string tipoAdquision { get; set; }
        public string observaciones { get; set; }
        public int areasId { get; set; }

        public Inventario(int id, string nombreCorto, string descripcion, string serie,
            string color, string fechaAdquision, string tipoAdquision, string observaciones,
            int areasId)
        {
            this.id = id;
            this.nombreCorto = nombreCorto;
            this.descripcion = descripcion;
            this.serie = serie;
            this.color = color;
            this.fechaAdquision = fechaAdquision;
            this.tipoAdquision = tipoAdquision;
            this.observaciones = observaciones;
            this.areasId = areasId;
        }



        public Inventario() { }
    }
}
