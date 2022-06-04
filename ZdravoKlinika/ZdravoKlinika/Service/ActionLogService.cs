using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.Service
{
    internal class ActionLogService
    {
        private ActionLogRepository actionLogRepository;

        public ActionLogService()
        {
            actionLogRepository = new ActionLogRepository();    
        }

        internal ActionLogRepository ActionLogRepository { get => actionLogRepository; set => actionLogRepository = value; }

        public void AddLog(DateTime time, String action, RegisteredPatient user)
        {
            ActionLogRepository.Add(new ActionLog(GetUniqueId(), time, action, user));
        }
        
        public void RemoveLog(String id)
        {
            ActionLog log = ActionLogRepository.GetById(id);
            actionLogRepository.Remove(log);
        }

        public void RemoveAll()
        {
            actionLogRepository.RemoveAll();
        }

        public List<ActionLog> GetAll()
        {
            return actionLogRepository.GetAll();
        }

        public ActionLog GetById(String id)
        {
            return ActionLogRepository.GetById(id);
        }

        public List<ActionLog> GetByUserId(string id)
        {
            return actionLogRepository.GetByUserId(id);
        }

        public bool IsUserBannable(String id)
        {
            bool returnVal = false;
            int count = 0;
            foreach(ActionLog log in actionLogRepository.GetByUserId(id))
            {
                if(log.Action == "Remove Appointment" || log.Action == "Edit Appointment")
                {
                    count++;
                }
            }
            if(count >= 4)
            {
                returnVal = true;
            }
            return returnVal;
        }
        public String GetUniqueId()
        {
            bool unique = false;
            String newId = "";
            while (!unique)
            {
                newId = IdGenerator.Generate();
                unique = IsUnique(newId);
            }
            return newId;
        }
        public bool IsUnique(String newId)
        {
            bool returnVal = true;
            List<ActionLog> logs = actionLogRepository.GetAll();
            foreach (ActionLog log in logs)
            {
                if (log.Id.Equals(newId))
                {
                    returnVal = false;
                    break;
                }
            }
            return returnVal;
        }
    }
}
