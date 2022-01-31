using tempo_api.Interfaces.Repositories;
using tempo_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace tempo_api.Repositories
{
	public class EmpleadosRepository : Repository<Empleados>, IEmpleadosRepository
    	{
        	public EmpleadosRepository(TEMPOContext tEMPOContext)
            	:base(tEMPOContext)
        	{}
		
    	}
}
