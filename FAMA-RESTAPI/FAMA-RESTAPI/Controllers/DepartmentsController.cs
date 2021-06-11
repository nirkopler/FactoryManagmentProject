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
        [HttpGet]
        [Route("api/Departments")]
        public IEnumerable<departments> Get()
        {
            return bl.getAllDepartments();
        }

        // GET: api/Departments/all/userID
        [HttpGet]
        [Route("api/Departments/all/{userID}")]
        public IEnumerable<departmentsExt> GetAll(int userID)
        {
            return bl.getAllDepartmentsAction(userID);
        }

        // GET: api/Departments/5
        [HttpGet]
        [Route("api/Departments/{id}")]
        public departments Get(int id)
        {
            return bl.getDepartments(id);
        }

        // POST: api/Departments/{userID}
        [HttpPost]
        [Route("api/Departments/{userID}")]
        public string Post(int userID, departments dep)
        {
            bl.postDepartments(userID, dep);
            return "DEP DEPARTMENT CREATED";
        }

        // PUT: api/Departments/{id}/{userID}
        [HttpPut]
        [Route("api/Departments/{id}/{userID}")]
        public string Put(int id, int userID, departments dep)
        {
            bl.putDepartments(id, userID, dep);
            return "DEP DEPARTMENT EDITED";
        }

        // DELETE: api/Departments/{id}/{userID}
        [HttpDelete]
        [Route("api/Departments/{id}/{userID}")]
        public string Delete(int id, int userID)
        {
            bl.deleteDepartments(id, userID);
            return "DEP DEPARTMENT DELETED";
        }
    }
}
