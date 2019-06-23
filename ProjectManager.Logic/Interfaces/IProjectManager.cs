using ProjectManager.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Interfaces
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetProjects();
        Project GetProjectById(int projectId);
        Project SaveProject(Project project);
        int DeleteProject(int projectId);
    }
}
