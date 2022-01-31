using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Napster.BuildToken.Jwt;
using tempo_api.Interfaces.Services;
using tempo_api.Models;
using tempo_api.Models.Account;
using tempo_api.Models.Generic;

namespace tempo_api.Controllers.Account
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TEMPOContext _context;
        private readonly ITokenFactory _tokenFactory;
        private readonly IEmpleadosService _empleadosService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                TEMPOContext context,
                                ITokenFactory tokenFactory,
                                IEmpleadosService empleadosService)
        {
            _userManager = userManager;
            _context = context;
            _tokenFactory = tokenFactory;
            _empleadosService = empleadosService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<Response>> RegisterUser([FromBody] RegisterUser model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            model.Rol = "Empleado";
            var idRolClaim = "1";

            var identityUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = model.Password
            };


            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, model.Rol);

                // Se crea la instancia de empleado y se guarda el registro en la DB
                var empleado = new Empleados
                {
                   Documento = model.Documento,
                   Nombres = model.Nombres,
                   Apellidos = model.Apellidos,
                   IdUser = identityUser.Id
                };


                var  responseService = _empleadosService.CreateEmpleados(empleado);
                var  empleadoResult = (Empleados)responseService.Result;

                //Se le asigna el claim personalizado de idEmpleado en los Claims
                await _userManager.AddClaimAsync(identityUser, new Claim("IdEmpleado", empleadoResult.Id.ToString()));

                await _userManager.AddClaimAsync(identityUser, new Claim("IdRol", idRolClaim));
                await _userManager.AddClaimAsync(identityUser, new Claim("RolName", model.Rol));


                return new Response { IsSuccess = result.Succeeded, Message = "Usuario registrado correctamente", Result = identityUser };

            }
            else
                return new Response { IsSuccess = result.Succeeded, Message = "Error", Result = result.Errors.ToList() };


        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Response>> LoginUser([FromBody]LoginUser loginUser)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            var claims = await GetClaimsUser(loginUser.UserName, loginUser.Password);

            if (claims == null)
                return new Response { IsSuccess = false, Message = "Error de inicio de sesión", Result = "Usuario o contraseña incorrecto" };


            // Se obtiene el claim de IdEmpleado
            // Se consultar el empleado por Id para devolverlo en la respuesta del Login
            var idEmpleado = Convert.ToInt32(claims.SingleOrDefault(c => c.Type == "IdEmpleado").Value);
            var responseService = _empleadosService.GetEmpleadosById(idEmpleado);
            var empleado = (Empleados)responseService.Result;

            // Lista de roles del usuario
            List<IdentityUserRole<string>> userRolesList = _context.UserRoles
                                                                   .Where(r => r.UserId == user.Id)
                                                                   .Select(s => new IdentityUserRole<string>
                                                                   {
                                                                       RoleId = s.RoleId,
                                                                       UserId = s.UserId
                                                                   }).ToList();

            // El TokenFactory con el metodo BuildToken es un paquete nuget de mi autoría.
            var loginResult = new LoginResult()
            {
                User = user,
                Empleados = empleado,
                Roles = userRolesList,
                Token = _tokenFactory.BuildToken(user.UserName),
            };

            return new Response { IsSuccess = true, Message = $"Usuario autentificado como {loginUser.UserName}", Result = loginResult };

        }



        private async Task<IEnumerable<Claim>> GetClaimsUser(string userName, string password)
        {

            // El usuario o la contraseña esta vacia
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<IEnumerable<Claim>>(null);

            // Obtiene el usuario para verficarlo
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<IEnumerable<Claim>>(null);

            // Comprueba las credenciales
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var claimsUser = await _userManager.GetClaimsAsync(userToVerify);
                return await Task.FromResult(claimsUser);
            }

            // Credenciales no validas o el usuario no existe
            return await Task.FromResult<IEnumerable<Claim>>(null);

        }


    }
}