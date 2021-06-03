using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAMA_RESTAPI.Models
{
    public class loginBL
    {
        famaDBEntities db = new famaDBEntities();
        logsBL logsBL = new logsBL();
        public users isUserExist(users user)
        {
            //check if user exists
            var result = db.users.Where(u => u.username == user.username && u.password == user.password);
            if (result.Count() == 1) {
                //get time
                DateTime localDate = DateTime.Now;
                //check if user already log
                var logcheck = db.logs.Where(l => l.userID == result.FirstOrDefault().ID);
                if (logcheck.Count() == 1)
                {
                    //check if user already log TODAY
                    if (logcheck.FirstOrDefault().date.ToShortDateString() != localDate.ToShortDateString())
                    {
                        logcheck.FirstOrDefault().actions = 0;
                        db.SaveChanges();
                    }
                }
                //if user is totally new to DB
                else { 
                    //add log to logs table
                    logs newLog = new logs();
                    newLog.userID = result.FirstOrDefault().ID;
                    newLog.date = localDate;
                    db.logs.Add(newLog);
                    db.SaveChanges();
                }
                //return current user
                return result.First();
            } else { return null; };
        }
    }
}