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
    public class DepartmentsController : ApiController
    {
        private static departmentsBL bl = new departmentsBL();

        // GET: api/Departments
        public IEnumerable<departments> Get()
        {
            return bl.getAllDepartments();
        }

        // GET: api/Departments/5
        public departments Get(int id)
        {
            return bl.getDepartments(id);
        }

        // POST: api/Departments
        public string Post(departments dep)
        {
            bl.postDepartments(dep);
            return "DEP DEPARTMENT CREATED";
        }

        // PUT: api/Departments/5
        public string Put(int id, departments dep)
        {
            bl.putDepartments(id, dep);
            return "DEP DEPARTMENT EDITED";
        }

        // DELETE: api/Departments/5
        public string Delete(int id)
        {
            bl.deleteDepartments(id);
            return "DEP DEPARTMENT DELETED";
        }
    }
}
