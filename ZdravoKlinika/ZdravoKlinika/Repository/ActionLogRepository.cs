using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    internal class ActionLogRepository
    {
        private ActionLogDataHandler actionLogDataHandler;
        private RegisteredPatientRepository registeredPatientRepository;

        public ActionLogRepository()
        {
            actionLogDataHandler = new ActionLogDataHandler();
            registeredPatientRepository = new RegisteredPatientRepository();
            ReadDataFromFile();
        }
        private void ReadDataFromFile()
        {
            actionLogs = actionLogDataHandler.Read();
            if (actionLogs == null) actionLogs = new List<ActionLog>();
        }

        public void UpdateReferences(ActionLog log)
        {
            if (log.RegisteredPatient != null)
                log.RegisteredPatient = registeredPatientRepository.GetById(log.RegisteredPatient.GetPatientId());
        }
        public void AddLog(Model.ActionLog log)
        {

            if (log == null)
                return;
            if (this.actionLogs == null)
                this.actionLogs = new List<ActionLog>();
            if (!this.actionLogs.Contains(log))
            {
                actionLogs.Add(log);
                actionLogDataHandler.Write(actionLogs);
            }
           
        }

        public void RemoveLog(Model.ActionLog log)
        {
            if (log == null)
                return;
            if(this.actionLogs != null)
            {
                if(actionLogs.Contains(log))
                   actionLogs.Remove(log);
            }
        }

        public void RemoveAll()
        {
            if(actionLogs != null)
                actionLogs.Clear();
        }

        public List<ActionLog> GetAll()
        {
            ReadDataFromFile();
            foreach (ActionLog log in actionLogs)
            {
                UpdateReferences(log);
            }
            return actionLogs;
        }

        public Model.ActionLog GetById(String id)
        {
            ActionLog returnValue = null;
            foreach(ActionLog log in actionLogs)
            {
                if(log.Id == id)
                {
                    UpdateReferences(log);
                    returnValue = log;
                    break;
                }
            }
            return returnValue;
        }

        public List<ActionLog> GetByUserId(string id)
        {
            List<ActionLog> returnValue = new List<ActionLog> ();
            foreach(ActionLog log in actionLogs)
            {
                if(log.RegisteredPatient.IsPatientById(id))
                {
                    UpdateReferences(log);
                    returnValue.Add(log);
                }
            }
            return returnValue;
        }

        public System.Collections.Generic.List<ActionLog> actionLogs;

        /// <summary>
        /// Property for collection of Model.ActionLog
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.Generic.List<ActionLog> ActionLog
        {
            get
            {
                if (actionLogs == null)
                    actionLogs = new System.Collections.Generic.List<ActionLog>();
                return actionLogs;
            }
            set
            {
                RemoveAll();
                if (value != null)
                {
                    foreach (Model.ActionLog oActionLog in value)
                        AddLog(oActionLog);
                }
            }
        }

        
    }
}
