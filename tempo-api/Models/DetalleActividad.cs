using System;
using System.Collections.Generic;

namespace tempo_api.Models
{
    public partial class DetalleActividad
    {
        public int IdDetalleActividad { get; set; }
        public int IdActividad { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Horas { get; set; }

        public virtual Actividad IdActividadNavigation { get; set; }
    }
}
