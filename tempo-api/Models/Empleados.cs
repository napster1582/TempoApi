using System;
using System.Collections.Generic;

namespace tempo_api.Models
{
    public partial class Empleados
    {
        public Empleados()
        {
            Actividad = new HashSet<Actividad>();
        }

        public int Id { get; set; }
        public string IdUser { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public virtual ICollection<Actividad> Actividad { get; set; }
    }
}
