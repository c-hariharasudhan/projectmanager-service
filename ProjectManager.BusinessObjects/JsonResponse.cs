using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessObjects
{
    public class JsonResponse
    {
        public object Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public JsonResponse(string status = Constants.STATUS_SUCCESS)
        {
            Status = status;
        }
    }
}
