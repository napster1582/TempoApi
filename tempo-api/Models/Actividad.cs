using System;
using System.Collections.Generic;

namespace tempo_api.Models
{
    public partial class Actividad
    {
        public Actividad()
        {
            DetalleActividad = new HashSet<DetalleActividad>();
        }

        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Empleados IdEmpleadoNavigation { get; set; }
        public virtual ICollection<DetalleActividad> DetalleActividad { get; set; }
    }
}
