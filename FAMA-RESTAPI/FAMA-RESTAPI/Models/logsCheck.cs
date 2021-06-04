using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class logsCheck
    {
        famaDBEntities db = new famaDBEntities();

        
        public string addAction(int id)
        {
            var currentUser = db.logs.Where(l => l.userID == id).First();
            currentUser.actions += 1;
            db.SaveChanges();
            return "user id: " + currentUser.userID + " +1 action";
        }

        public bool isMaxActions(int id)
        {
            var currentUserUsers = db.users.Where(l => l.ID == id).First();
            var currentUserLogs = db.logs.Where(l => l.userID == id).First();
            return currentUserUsers.numOfActions > currentUserLogs.actions ? true : false;
        }
    }
}