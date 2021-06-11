using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FAMA_RESTAPI.Models;
using System.Web.Http.Cors;

namespace FAMA_RESTAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShiftsController : ApiController
    {
        private static shiftsBL bl = new shiftsBL();

        // GET: api/Shifts
        [HttpGet]
        [Route("api/Shifts")]
        public IEnumerable<shifts> Get()
        {
            return bl.getAllShifts();
        }

        // GET: api/Shifts//all/{userID}
        [HttpGet]
        [Route("api/Shifts/all/{userID}")]
        public IEnumerable<shiftsWithEmployees> GetAll(int userID)
        {
            return bl.getAllShiftsAction(userID);
        }

        // GET: api/Shifts/5
        [HttpGet]
        [Route("api/Shifts/{id}")]
        public shifts Get(int id)
        {
            return bl.getShifts(id);
        }

        // POST: api/Shifts/{userID}
        [HttpPost]
        [Route("api/Shifts/{userID}")]
        public string Post(shifts shift, int userID)
        {
            bl.postShifts(shift, userID);
            return "SHF SHIFT ADDED";
        }
    }
}
