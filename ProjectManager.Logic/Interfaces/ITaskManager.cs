using ProjectManager.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Interfaces
{
    public interface ITaskManager
    {
        IEnumerable<BusinessObjects.Task> GetTasksByProject(int projectId);
        BusinessObjects.Task SaveTask(BusinessObjects.Task task);
        int DeleteTask(int taskId);
        IEnumerable<ParentTask> GetParentTasks();
        ParentTask InsertParentTask(ParentTask parentTask);
    }
}
