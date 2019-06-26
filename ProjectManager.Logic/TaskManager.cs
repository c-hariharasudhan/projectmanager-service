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
        private readonly IRepository<DataAccess.Entity.User> _userRepository;
        private readonly IMapper _mapper;
        public TaskManager(IRepository<DataAccess.Entity.Task> taskRepository, IRepository<DataAccess.Entity.ParentTask> parentTaskRepository, IMapper mapper, IRepository<DataAccess.Entity.User> userRepository)
        {
            _taskRepository = taskRepository;
            _parentTaskRepository = parentTaskRepository;
            _userRepository = userRepository;
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
            if(task.Priority == 0)
            {
                InsertParentTask(new ParentTask { ParentTaskName = task.TaskName });
                return new BusinessObjects.Task();
            }
            var entity = _mapper.Map<DataAccess.Entity.Task>(task);           
            var result = task.TaskId == 0 ? _taskRepository.Create(entity) : _taskRepository.Update(entity);
            _userRepository.Update(user => user.User_Id == task.User.UserId, "Task_Id", result.Task_Id);
            return _mapper.Map<BusinessObjects.Task>(result);
        }
    }
}
