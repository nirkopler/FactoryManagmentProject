using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class shiftsBL
    {
        famaDBEntities db = new famaDBEntities();
        private static logsCheck log = new logsCheck();

        public IEnumerable<shifts> getAllShifts()
        {
            return db.shifts;
        }
        public IEnumerable<shiftsWithEmployees> getAllShiftsAction(int userID)
        {
            famaDBEntities db = new famaDBEntities();
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                List<shiftsWithEmployees> shiftList = new List<shiftsWithEmployees>();
                foreach (var shf in db.shifts)
                {
                    shiftsWithEmployees shift = new shiftsWithEmployees();
                    shift.ID = shf.ID;
                    shift.date = shf.date;
                    shift.startTime = shf.startTime;
                    shift.endTime = shf.endTime;
                    shift.empList = new List<employees>();
                    foreach (var emp in db.employeesShifts.Where(s => s.shiftID == shf.ID))
                    {
                        var currentEmp = db.employees.Where(e => e.ID == emp.employeeID).First();
                        shift.empList.Add(currentEmp);
                    }
                    shiftList.Add(shift);
                }
                return shiftList;
            } else { return null; }
        }

        public shifts getShifts(int id)
        {
            return getAllShifts().Where(s => s.ID == id).First();
        }

        public void postShifts(shifts shift, int userID)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                db.shifts.Add(shift);
                db.SaveChanges();
            }
        }
    }
}