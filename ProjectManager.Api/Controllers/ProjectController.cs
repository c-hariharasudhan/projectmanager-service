using ProjectManager.BusinessObjects;
using ProjectManager.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManager.Api.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectManager _projectManager;
        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }
        [HttpGet]
        [Route("api/project")]
        public JsonResponse Get()
        {
            return new JsonResponse { Data = _projectManager.GetProjects() };
        }


        [HttpPost]
        [Route("api/project/save")]
        public JsonResponse Save(Project project)
        {
            return new JsonResponse { Data = _projectManager.SaveProject(project) };
        }

        [HttpPost]
        [Route("api/project/delete")]
        public JsonResponse Delete(int projectId)
        {
            return new JsonResponse { Data = _projectManager.DeleteProject(projectId) };
        }
    }
}