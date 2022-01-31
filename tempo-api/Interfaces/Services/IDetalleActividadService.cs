using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Models;

namespace tempo_api.Interfaces.Services
{
    public interface IDetalleActividadService
    {
        Response GetAllActividad();
        Response GetActividadById(int id);
        Response CreateActividad(DetalleActividad model);
        Response UpdateActividad(DetalleActividad model);
        Task<Response> DeleteActividad(int id);
        Response GetListByAdvanceQuery(string[] filter, params string[] includes);
        Response GetOneByAdvanceQuery(string[] filter, params string[] includes);

    }
}
