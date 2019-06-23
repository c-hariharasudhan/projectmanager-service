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
        private readonly IMapper _mapper;
        public ProjectManager(IRepository<DataAccess.Entity.Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public int DeleteProject(int projectId)
        {
            throw new NotImplementedException();
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
            return _mapper.Map<Project>(response);
        }
    }
}
