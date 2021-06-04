using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class logsCheck
    {
        famaDBEntities db = new famaDBEntities();

        public bool checkLogs(int userID)
        {
            var currentUserLog = db.logs.Where(l => l.userID == userID).First();
            var currentUser = db.users.Where(u => u.ID == userID).First();
            if (currentUserLog.actions > currentUser.numOfActions)
            {
                return false;
            }
            return true;
        }

        public void addActionLog(int userID)
        {
            var currentUserLog = db.logs.Where(l => l.userID == userID).First();
            var currentUser = db.users.Where(u => u.ID == userID).First();
            currentUserLog.actions++;
            db.SaveChanges();
        }
    }
}