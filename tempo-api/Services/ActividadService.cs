using Microsoft.Extensions.Logging;
using Repository.Helpers;
using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Interfaces.Repositories;
using tempo_api.Interfaces.Services;
using tempo_api.Models;

namespace tempo_api.Services
{
    public class ActividadService : IActividadService
    {
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        private readonly IActividadRepository _repository;

        public ActividadService(ILoggerFactory loggerFactory, IActividadRepository repository)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<ActividadService>();

            _repository = repository;
        }

        public Response GetAllActividad()
        {
            _logger.LogDebug("Solicitud para listar todos los Actividad");
            var response = _repository.GetAll(
            //includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response GetActividadById(int id)
        {
            _logger.LogDebug("Solicitud para obtener un solo registro por condición");
            var response = _repository.GetObjectByCondition(
            predicate: source => source.IdActividad == id
            //,includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response CreateActividad(Actividad model)
        {
            _logger.LogDebug($"Solicitud para crear {model}");
            return _repository.Create(model);
        }

        public Response UpdateActividad(Actividad model)
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
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<Actividad>(filter),
            includes
            );
            return response;
        }

        public Response GetOneByAdvanceQuery(string[] filter, params string[] includes)
        {
            var response = _repository.FindOneByConditionInclude(
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<Actividad>(filter),
            includes
            );
            return response;
        }




    }
}
