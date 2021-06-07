using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class departmentsExt
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int managerID { get; set; }
        public bool areAnyEmployees { get; set; }
    }
}