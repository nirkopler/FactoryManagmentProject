using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class departmentsBL
    {
        famaDBEntities db = new famaDBEntities();
        private static logsCheck log = new logsCheck();

        public IEnumerable<departments> getAllDepartments()
        {
            return db.departments;
        }
        public IEnumerable<departmentsExt> getAllDepartmentsAction(int userID)
        {
            famaDBEntities db = new famaDBEntities();
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID))
            {
                List<departmentsExt> depList = new List<departmentsExt>();
                foreach (var dep in db.departments)
                {
                    departmentsExt department = new departmentsExt();
                    department.ID = dep.ID;
                    department.name = dep.name;
                    department.managerID = dep.managerID;
                    department.areAnyEmployees = false;
                    foreach (var emp in db.employees)
                    {
                        if (emp.departmentID == dep.ID)
                        {
                            department.areAnyEmployees = true;
                        }
                    }
                    depList.Add(department);
                }
                return depList;
            } else { return null; }
        }

        public departments getDepartments(int id)
        {
            return db.departments.Where(d => d.ID == id).First();
        }

        public void postDepartments(int userID, departments dep)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                db.departments.Add(dep);
                db.SaveChanges();
            }
        }

        public void putDepartments(int id, int userID, departments dep)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                var currentDepartment = db.departments.Where(d => d.ID == id).First();
                currentDepartment.name = dep.name;
                currentDepartment.managerID = dep.managerID;
                db.SaveChanges();
            }
        }

        public void deleteDepartments(int id, int userID)
        {
            //add user action
            log.addActionLog(userID);
            //Check user actions
            if (log.checkLogs(userID)) { 
                var currentDepartment = db.departments.Where(d => d.ID == id).First();
                db.departments.Remove(currentDepartment);
                db.SaveChanges();
            }
        }
    }
}