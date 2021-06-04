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
        public IEnumerable<shiftsWithEmployees> Get()
        {
            return bl.getAllShifts();
        }

        // GET: api/Shifts/5
        public shiftsWithEmployees Get(int id)
        {
            return bl.getShifts(id);
        }

        // POST: api/Shifts
        public string Post(shifts shift)
        {
            bl.postShifts(shift);
            return "SHF SHIFT ADDED";
        }

        // PUT: api/Shifts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Shifts/5
        public void Delete(int id)
        {
        }
    }
}
