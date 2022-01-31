using Microsoft.Extensions.Logging;
using Repository.Helpers;
using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Interfaces.Repositories;
using tempo_api.Interfaces.Services;
using tempo_api.Models;

namespace tempo_api.Services
{
    public class EmpleadosService : IEmpleadosService
    {
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        private readonly IEmpleadosRepository _repository;

        public EmpleadosService(ILoggerFactory loggerFactory, IEmpleadosRepository repository)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<EmpleadosService>();

            _repository = repository;
        }

        public Response GetAllEmpleados()
        {
            _logger.LogDebug("Solicitud para listar todos los Empleados");
            var response = _repository.GetAll(
            //includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response GetEmpleadosById(int id)
        {
            _logger.LogDebug("Solicitud para obtener un solo registro por condición");
            var response = _repository.GetObjectByCondition(
            predicate: source => source.Id == id
            //,includes: source => source.AsQueryable().Include(x => x.IdNavigation));
            );
            return response;
        }

        public Response CreateEmpleados(Empleados model)
        {
            _logger.LogDebug($"Solicitud para crear {model}");
            return _repository.Create(model);
        }

        public Response UpdateEmpleados(Empleados model)
        {
            _logger.LogDebug($"Solicitud para actualizar {model}");
            return _repository.Update(model);
        }

        public async Task<Response> DeleteEmpleados(int id)
        {
            return await _repository.DeleteById(id);
        }

        public Response GetListByAdvanceQuery(string[] filter, params string[] includes)
        {
            var response = _repository.FindAllByConditionInclude(
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<Empleados>(filter),
            includes
            );
            return response;
        }

        public Response GetOneByAdvanceQuery(string[] filter, params string[] includes)
        {
            var response = _repository.FindOneByConditionInclude(
            predicate: AdvanceQueryUtils.ConvertParamArgsToExpression<Empleados>(filter),
            includes
            );
            return response;
        }




    }
}
