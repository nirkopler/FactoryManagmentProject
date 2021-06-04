using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class employeesBL
    {
        famaDBEntities db = new famaDBEntities();

        public IEnumerable<employeesWithShifts> geAllEmployees()
        {
            List<employeesWithShifts> empList = new List<employeesWithShifts>();
            foreach(var emp in db.employees)
            {
                employeesWithShifts employee = new employeesWithShifts();
                employee.ID = emp.ID;
                employee.fullname = emp.fullname;
                employee.startWorkYear = emp.startWorkYear;
                employee.departmentID = emp.departmentID;
                employee.empShifts = new List<shifts>();
                foreach (var shift in db.employeesShifts.Where(s => s.employeeID == emp.ID))
                {
                    var currentShift = db.shifts.Where(s => s.ID == shift.shiftID).First();
                    employee.empShifts.Add(currentShift);
                }
                empList.Add(employee);
            };

            return empList;
        }

        public employeesWithShifts getEmployees(int id)
        {
            return geAllEmployees().Where(emp => emp.ID == id).FirstOrDefault();
        }

        public void addEmployeesShift(employeesShifts shift)
        {
            db.employeesShifts.Add(shift);
            db.SaveChanges();
        }

        public void putEmployees(int id, employees emp)
        {
            var currentEmp = db.employees.Where(e => e.ID == id).First();
            currentEmp.fullname = emp.fullname;
            currentEmp.startWorkYear = emp.startWorkYear;
            currentEmp.departmentID = emp.departmentID;
            db.SaveChanges();
        }
        public void deleteEmployees(int id)
        {
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