using AutoMapper;
using NUnit.Framework;
using ProjectManager.Logic.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Tests.Mappings
{
    [TestFixture]
    public class ProjectMapsTests
    {
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            _mapper = UnityUlitity.SetupAutoMapper();
        }
        [Test]
        public void ProjectMaps_EntityToBusinessObject()
        {
            var entity = new DataAccess.Entity.Project
            {
                Project_Id = 11,
                Project_Name = "Project",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 5
            };
            var result = _mapper.Map<BusinessObjects.Project>(entity);
            Assert.That(result.ProjectId == 11);
        }

        [Test]
        public void ProjectMaps_BusinessObjectToEntity()
        {
            var bo = new BusinessObjects.Project
            {
                ProjectName = "Test",
                Priority = 10
            };
            var result = _mapper.Map<DataAccess.Entity.Project>(bo);
            Assert.That(result.Priority == 10);
        }

        [Test]
        public void ProjectMaps_MapsTasksCount()
        {
            var entity = new DataAccess.Entity.Project
            {
                Project_Id = 11,
                Project_Name = "Project",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 5,
                Tasks = new List<DataAccess.Entity.Task> {
                    new DataAccess.Entity.Task { Task_Id = 2, Task_Name = "Task 2", Project_Id = 11, Status = false},
                    new DataAccess.Entity.Task { Task_Id = 3, Task_Name = "Task 3", Project_Id = 11, Status = true},
                    new DataAccess.Entity.Task { Task_Id = 4, Task_Name = "Task 4", Project_Id = 11, Status = true},
                }
            };
            var result = _mapper.Map<BusinessObjects.Project>(entity);
            Assert.That(result.ProjectId == 11);
            Assert.That(result.NoOfTasks == 3);
            Assert.That(result.NoOfCompletedTasks == 1);

        }

    }
}
