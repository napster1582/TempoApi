using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tempo_api.Models.Account
{
    public class RegisterUser
    {

    
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }


    }
}
