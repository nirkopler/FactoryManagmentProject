using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class employeesBL
    {
        famaDBEntities db = new famaDBEntities();
        private static logsCheck log = new logsCheck();

        public IEnumerable<employeesWithShifts> geAllEmployees(int userID)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) {
                List<employeesWithShifts> empList = new List<employeesWithShifts>();
                foreach(var emp in db.employees)
                {
                    employeesWithShifts employee = new employeesWithShifts();
                    employee.ID = emp.ID;
                    employee.fullname = emp.fullname;
                    employee.startWorkYear = emp.startWorkYear;
                    employee.departmentID = emp.departmentID;
                    employee.departmentName = db.departments.Where(d => d.ID == emp.departmentID).First().name;
                    employee.isManager = db.departments.Where(d => d.ID == emp.departmentID).First().managerID == emp.ID;
                    employee.empShifts = new List<shifts>();
                    foreach (var shift in db.employeesShifts.Where(s => s.employeeID == emp.ID))
                    {
                        var currentShift = db.shifts.Where(s => s.ID == shift.shiftID).First();
                        employee.empShifts.Add(currentShift);
                    }
                    empList.Add(employee);
                };

            return empList;
            } else { return null; }
        }

        public IEnumerable<employeesWithShifts> empSearchResult(string inp, string by, int userID)
        {
            List<employeesWithShifts> result = new List<employeesWithShifts>();
            switch (by)
            {
            case "fname":
                result = geAllEmployees(userID).Where(emp => emp.fullname.Split(' ')[0] == inp).ToList();
                break;
            case "lname":
                result = geAllEmployees(userID).Where(emp => emp.fullname.Split(' ')[1] == inp).ToList();
                break;
            case "dep":
                result = geAllEmployees(userID).Where(emp => emp.departmentName == inp).ToList();
                break;
            }
            return result;
        }

        public void addEmployeesShift(employeesShifts shift, int userID)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                db.employeesShifts.Add(shift);
                db.SaveChanges();
            }
        }

        public void putEmployees(int id, int userID, employees emp)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID))
            {
                var currentEmp = db.employees.Where(e => e.ID == id).First();
                currentEmp.fullname = emp.fullname;
                currentEmp.startWorkYear = emp.startWorkYear;
                currentEmp.departmentID = emp.departmentID;
                db.SaveChanges();
            }
        }

        public void deleteEmployees(int id, int userID)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                //remove employee
                var currentEmp = db.employees.Where(e => e.ID == id).First();
                db.employees.Remove(currentEmp);
                //remove employee's shifts
                var empShifts = db.employeesShifts.Where(s => s.employeeID == id);
                foreach (var s in empShifts) {db.employeesShifts.Remove(s);};
                //save to DB
                db.SaveChanges();
            }
        }
    }
}