using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessObjects
{
    public class ParentTask : BaseModel
    {
        public int ParentTaskId { get; set; }
        public string ParentTaskName { get; set; }
    }
}
