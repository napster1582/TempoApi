using Microsoft.Extensions.Logging;
using Repository.Helpers;
using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Interfaces.Repositories;
using tempo_api.Interfaces.Services;
using tempo_api.Models;

namespace tempo_api.Services
{
    public class DetalleActividadService : IDetalleActividadService
    {
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        private readonly IDetalleActividadRepository _repository;

        public DetalleActividadService(ILoggerFactory loggerFactory, IDetalleActividadRepository repository)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<DetalleActividadService>();

            _repository = repository;
        }

        public Response GetAllActividad()
        {
            _logger.LogDebug("Solicitud para listar todos los DetalleActividad");
            var response = _repository.GetAll(
            //includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response GetActividadById(int id)
        {
            _logger.LogDebug("Solicitud para obtener un solo registro por condición");
            var response = _repository.GetObjectByCondition(
            predicate: source => source.IdDetalleActividad == id
            //,includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response CreateActividad(DetalleActividad model)
        {
            _logger.LogDebug($"Solicitud para crear {model}");
            return _repository.Create(model);
        }

        public Response UpdateActividad(DetalleActividad model)
        {
            _logger.LogDebug($"Solicitud para actualizar {model}");
            return _repository.Update(model);
        }

        public async Task<Response> DeleteActividad(int id)
        {
            return await _repository.DeleteById(id);
        }

        public Response GetListByAdvanceQuery(string[] filter, params string[] includes)
        {
            var response = _repository.FindAllByConditionInclude(
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<DetalleActividad>(filter),
            includes
            );
            return response;
        }

        public Response GetOneByAdvanceQuery(string[] filter, params string[] includes)
        {
            var response = _repository.FindOneByConditionInclude(
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<DetalleActividad>(filter),
            includes
            );
            return response;
        }








    }
}
