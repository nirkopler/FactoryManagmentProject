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
    public class EmployeesController : ApiController
    {
        private static employeesBL bl = new employeesBL();

        // GET: api/Employees
        [HttpGet]
        [Route("api/Employees/{userID}")]
        public IEnumerable<employeesWithShifts> Get(int userID)
        {
            return bl.geAllEmployees(userID);
        }

        // GET: api/Employees/5
        [HttpGet]
        [Route("api/Employees/search/{inp}/{by}/{userID}")]
        public IEnumerable<employeesWithShifts> Get(string inp, string by, int userID)
        {
            return bl.empSearchResult(inp, by, userID);
        }

        // POST: api/Employees
        [HttpPost]
        [Route("api/Employees/{userID}")]
        public string Post(employeesShifts shift, int userID)
        {
            bl.addEmployeesShift(shift, userID);
            return "EMP SHIFT ADDED";
        }

        // PUT: api/Employees/5
        [HttpPut]
        [Route("api/Employees/{id}/{userID}")]
        public string Put(int id, int userID, employees emp)
        {
            bl.putEmployees(id, userID, emp);
            return "EMP PUT COMPLETED";
        }

        // DELETE: api/Employees/5
        [HttpDelete]
        [Route("api/Employees/{id}/{userID}")]
        public string Delete(int id, int userID)
        {
            bl.deleteEmployees(id, userID);
            return "ALL EMP DATA DELETED";
        }
    }
}
