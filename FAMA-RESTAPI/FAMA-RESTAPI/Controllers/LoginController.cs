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
        
        // GET: api/Login/5
        [HttpGet]
        public bool Get(int id)
        {
            return bl.loginCheck(id);
        }
        
        // POST: api/Login
        [HttpPost]
        public users Post(users user)
        {
            return bl.isUserExist(user);
        }
    }
}
