using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Interfaces.Services;
using tempo_api.Models;

namespace tempo_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DetalleActividadController : ControllerBase
    {

        private readonly IDetalleActividadService _service;

        public DetalleActividadController(IDetalleActividadService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<Response> GetAllActividad()
        {
            return _service.GetAllActividad();
        }

        [HttpGet("{id}")]
        public ActionResult<Response> GetActividadById(int id)
        {
            return _service.GetActividadById(id);
        }

        [HttpPost]
        public ActionResult<Response> CreateActividad([FromBody]DetalleActividad model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.CreateActividad(model);
        }

        [HttpPut]
        public ActionResult<Response> UpdateActividad([FromBody]DetalleActividad model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.UpdateActividad(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteActividad(int id)
        {
            return await _service.DeleteActividad(id);
        }

        [HttpGet]
        [Route("GetListByAdvanceQuery")]
        public ActionResult<Response> GetListByAdvanceQuery([FromQuery] string[] filter, [FromQuery] string[] includes)
        {
            return _service.GetListByAdvanceQuery(filter, includes);
        }

        [HttpGet]
        [Route("GetOneByAdvanceQuery")]
        public ActionResult<Response> GetOneByAdvanceQuery([FromQuery] string[] filter, [FromQuery] string[] includes)
        {
            return _service.GetOneByAdvanceQuery(filter, includes);
        }
        





    }
}
