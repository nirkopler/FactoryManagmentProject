using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class departmentsBL
    {
        famaDBEntities db = new famaDBEntities();

        public IEnumerable<departments> getAllDepartments()
        {
            return db.departments;
        }

        public departments getDepartments(int id)
        {
            return db.departments.Where(d => d.ID == id).First();
        }

        public void postDepartments(departments dep)
        {
            db.departments.Add(dep);
            db.SaveChanges();
        }

        public void putDepartments(int id, departments dep)
        {
            var currentDepartment = db.departments.Where(d => d.ID == id).First();
            currentDepartment.name = dep.name;
            currentDepartment.managerID = dep.managerID;
            db.SaveChanges();
        }

        public void deleteDepartments(int id)
        {
            var currentDepartment = db.departments.Where(d => d.ID == id).First();
            db.departments.Remove(currentDepartment);
            db.SaveChanges();
        }
    }
}