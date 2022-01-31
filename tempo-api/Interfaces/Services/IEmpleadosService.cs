using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Models;

namespace tempo_api.Interfaces.Services
{
    public interface IEmpleadosService
    {
        Response GetAllEmpleados();
        Response GetEmpleadosById(int id);
        Response CreateEmpleados(Empleados model);
        Response UpdateEmpleados(Empleados model);
        Task<Response> DeleteEmpleados(int id);
        Response GetListByAdvanceQuery(string[] filter, params string[] includes);
        Response GetOneByAdvanceQuery(string[] filter, params string[] includes);




    }
}
