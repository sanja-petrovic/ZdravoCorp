using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class TimeOffRequestRepository
    {
        private TimeOffRequestDataHandler dataHandler;
        private List<TimeOffRequest> requests;

        public TimeOffRequestRepository()
        {
            this.dataHandler = new TimeOffRequestDataHandler();
            this.requests = new List<TimeOffRequest>();
        }

        public List<TimeOffRequest> GetAll()
        {
            return this.dataHandler.Read();
        }

        public TimeOffRequest GetById(int id)
        {
            TimeOffRequest request = null;
            foreach(TimeOffRequest r in this.requests)
            {
                if(r.Id == id)
                {
                    request = r;
                }
            }

            return request;
        }

        public void CreateRequest(TimeOffRequest request)
        {
            this.requests.Add(request);
        }

        public void ChangeState(TimeOffRequest request, RequestState state)
        {
            request.State = state;
        }
    }
}
