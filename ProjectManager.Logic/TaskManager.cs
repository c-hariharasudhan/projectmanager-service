using ProjectManager.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BusinessObjects;
using ProjectManager.DataAccess.Repository;
using AutoMapper;

namespace ProjectManager.Logic
{
    public class TaskManager : ITaskManager
    {
        private readonly IRepository<DataAccess.Entity.Task> _taskRepository;
        private readonly IRepository<DataAccess.Entity.ParentTask> _parentTaskRepository;
        private readonly IMapper _mapper;
        public TaskManager(IRepository<DataAccess.Entity.Task> taskRepository, IRepository<DataAccess.Entity.ParentTask> parentTaskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _parentTaskRepository = parentTaskRepository;
            _mapper = mapper;
        }
        public int DeleteTask(int taskId)
        {
            return _taskRepository.Delete(taskId);
        }

        public IEnumerable<ParentTask> GetParentTasks()
        {
            var result = _parentTaskRepository.Get();
            return _mapper.Map<IEnumerable<ParentTask>>(result);
        }

        public IEnumerable<BusinessObjects.Task> GetTasksByProject(int projectId)
        {
            var result = _taskRepository.Get().Where(t => t.Project_Id == projectId);
            return _mapper.Map<IEnumerable<BusinessObjects.Task>>(result);

        }

        public ParentTask InsertParentTask(ParentTask parentTask)
        {
            var entity = _mapper.Map<DataAccess.Entity.ParentTask>(parentTask);
            var result  =_parentTaskRepository.Create(entity);
            return _mapper.Map<ParentTask>(result);

        }

        public BusinessObjects.Task SaveTask(BusinessObjects.Task task)
        {
            var entity = _mapper.Map<DataAccess.Entity.Task>(task);
            if(task.ParentTaskId == 0 && !string.IsNullOrEmpty(task.ParentTaskName))
            {
                var parent = _parentTaskRepository.Create(new DataAccess.Entity.ParentTask { Parent_Task = task.ParentTaskName });
                entity.Parent_Id = parent.Parent_Id;
            }
            var result = task.TaskId == 0 ? _taskRepository.Create(entity) : _taskRepository.Update(entity);
            return _mapper.Map<BusinessObjects.Task>(result);
        }
    }
}
