using tempo_api.Interfaces.Repositories;
using tempo_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace tempo_api.Repositories
{
	public class ActividadRepository : Repository<Actividad>, IActividadRepository
    	{
        	public ActividadRepository(TEMPOContext tEMPOContext)
            	:base(tEMPOContext)
        	{}
		
    	}
}
