using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using System.Threading.Tasks;
using tempo_api.Interfaces.Services;
using tempo_api.Models;

namespace tempo_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {

        private readonly IEmpleadosService _service;

        public EmpleadosController(IEmpleadosService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<Response> GetAllEmpleados()
        {
            return _service.GetAllEmpleados();
        }

        [HttpGet("{id}", Name = "GetEmpleadosById")]
        public ActionResult<Response> GetEmpleadosById(int id)
        {
            return _service.GetEmpleadosById(id);
        }

        [HttpPost]
        public ActionResult<Response> CreateEmpleados([FromBody]Empleados model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.CreateEmpleados(model);
        }

        [HttpPut]
        public ActionResult<Response> UpdateEmpleados([FromBody]Empleados model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.UpdateEmpleados(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteEmpleados(int id)
        {
            return await _service.DeleteEmpleados(id);
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
