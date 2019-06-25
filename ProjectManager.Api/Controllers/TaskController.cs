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
    public class TaskController : ApiController
    {
        private readonly ITaskManager _taskManager;
        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        [HttpGet]
        [Route("api/task")]
        public JsonResponse GetTasksByProjectId(int projectId)
        {
            return new JsonResponse { Data = _taskManager.GetTasksByProject(projectId) };
        }

        [HttpGet]
        [Route("api/task/parent")]
        public JsonResponse GetParentTasks()
        {
            return new JsonResponse { Data = _taskManager.GetParentTasks() };
        }

        [HttpPost]
        [Route("api/task/save")]
        public JsonResponse Save(Task task)
        {
            return new JsonResponse { Data = _taskManager.SaveTask(task) };
        }

        [HttpPost]
        [Route("api/task/delete")]
        public JsonResponse Delete(int taskId)
        {
            return new JsonResponse { Data = _taskManager.DeleteTask(taskId) };
        }
    }
}