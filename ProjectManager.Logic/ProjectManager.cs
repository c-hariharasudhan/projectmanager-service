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
    public class ProjectManager : IProjectManager
    {
        private readonly IRepository<DataAccess.Entity.Project> _projectRepository;
        private readonly IRepository<DataAccess.Entity.User> _userRepository;
        private readonly IRepository<DataAccess.Entity.Task> _taskRepository;
        private readonly IMapper _mapper;
        public ProjectManager(IRepository<DataAccess.Entity.Project> projectRepository, IMapper mapper, IRepository<DataAccess.Entity.User> userRepository, IRepository<DataAccess.Entity.Task> taskRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public int DeleteProject(int projectId)
        {
            var taskIds = _taskRepository.Get().Where(t => t.Project_Id == projectId).Select(t => t.Task_Id).ToList();
            if (taskIds.Any())
            {
                _userRepository.UpdateAll(u => taskIds.Contains(u.Task_Id ?? 0), "Task_Id", null);
                _taskRepository.Delete(t => t.Project_Id == projectId);
            }
            _userRepository.UpdateAll(u => u.Project_Id == projectId, "Project_Id", null);
            return _projectRepository.Delete(projectId);
        }

        public Project GetProjectById(int projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _mapper.Map<IEnumerable<Project>>(_projectRepository.Get());
        }

        public Project SaveProject(Project project)
        {
            var request = _mapper.Map<DataAccess.Entity.Project>(project);
            var response = request.Project_Id == 0 ? _projectRepository.Create(request) : _projectRepository.Update(request);
            if (request.Project_Id > 0)
            {
                _userRepository.Update(manager => manager.Project_Id == project.ProjectId, "Project_Id", null);                
            }
            _userRepository.Update(manager => manager.User_Id == project.User.UserId, "Project_Id", response.Project_Id);
            return _mapper.Map<Project>(response);
        }
    }
}
