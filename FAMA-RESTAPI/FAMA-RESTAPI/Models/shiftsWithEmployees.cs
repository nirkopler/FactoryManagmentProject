using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class shiftsWithEmployees
    {
        public int ID { get; set; }
        public System.DateTime date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

        public List<employees> empList { get; set; }

    }
}