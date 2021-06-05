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
        [Route("api/Employees/{userID}")]
        public IEnumerable<employeesWithShifts> Get(int userID)
        {
            return bl.geAllEmployees(userID);
        }

        // GET: api/Employees/5
        [Route("api/Employees/{id}/{userID}")]
        public employeesWithShifts Get(int id, int userID)
        {
            return bl.getEmployees(id, userID);
        }

        // POST: api/Employees
        [HttpPost]
        [Route("api/Employees/{id}/{userID}")]
        public string Post(employeesShifts shift)
        {
            bl.addEmployeesShift(shift);
            return "EMP SHIFT ADDED";
        }

        // PUT: api/Employees/5
        [HttpPut]
        [Route("api/Employees/{id}/{userID}")]
        public string Put(int id, employees emp)
        {
            bl.putEmployees(id, emp);
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
