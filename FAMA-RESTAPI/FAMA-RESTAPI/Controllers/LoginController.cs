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
    public class LoginController : ApiController
    {
        private static loginBL bl = new loginBL();
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public users Post(users user)
        {
            return bl.isUserExist(user);
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }

        //[Route("api/Login/{num1}/{num2}")]
        //public string Get(int num1, int num2)
        //{
        //    int sum = num1 + num2;
        //    return "string: " + num1 + " + " + num2 + " = " + sum;
        //}
    }
}
