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
        public IEnumerable<employeesWithShifts> Get()
        {
            return bl.geAllEmployees();
        }

        // GET: api/Employees/5
        public employeesWithShifts Get(int id)
        {
            return bl.getEmployees(id);
        }

        // POST: api/Employees
        public string Post(employeesShifts shift)
        {
            bl.addEmployeesShift(shift);
            return "EMP SHIFT ADDED";
        }

        // PUT: api/Employees/5
        public string Put(int id, employees emp)
        {
            bl.putEmployees(id, emp);
            return "EMP PUT COMPLETED";
        }

        // DELETE: api/Employees/5
        public string Delete(int id)
        {
            bl.deleteEmployees(id);
            return "ALL EMP DATA DELETED";
        }
    }
}
