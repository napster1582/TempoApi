using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tempo_api.Models.Account
{
    public class LoginResult
    {

        public ApplicationUser User { get; set; }
        public Empleados Empleados { get; set; }
        public List<IdentityUserRole<string>> Roles { get; set; }
        public string Token { get; set; }
    }
}
