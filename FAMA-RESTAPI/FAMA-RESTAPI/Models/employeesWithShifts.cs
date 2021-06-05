using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class employeesWithShifts
    {
        public int ID { get; set; }
        public string fullname { get; set; }
        public int startWorkYear { get; set; }
        public int departmentID { get; set; }
        public string departmentName { get; set; }
        public bool isManager { get; set; }
        public List<shifts> empShifts { get; set; }
    }
}