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
    public class TaskMapsTests
    {
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            _mapper = UnityUlitity.SetupAutoMapper();
        }
        [Test]
        public void TaskMaps_EntityToBusinessObject()
        {
            var entity = new DataAccess.Entity.Task
            {
                Task_Id = 111,
                Task_Name = "Task",
                Parent_Id = 1,
                Status = true
            };
            var result = _mapper.Map<BusinessObjects.Task>(entity);
            Assert.That(result.TaskId == 111);
        }

        [Test]
        public void UserMaps_BusinessObjectToEntity()
        {
            var bo = new BusinessObjects.Task
            {
                TaskId = 111,
                TaskName = "test",
                Priority = 10,
                ProjectId = 1,
                ParentId = 1
            };
            var result = _mapper.Map<DataAccess.Entity.Task>(bo);
            Assert.That(result.Project_Id == 1);
            Assert.That(result.Parent_Id == 1);
            Assert.That(result.Task_Id == 111);
        }
    }
}
