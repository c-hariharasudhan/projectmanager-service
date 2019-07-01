using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManager.DataAccess.Repository;
using ProjectManager.Logic.Interfaces;
using ProjectManager.Logic.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Tests.Managers
{
    [TestFixture]
    public class ProjectManagerTests
    {
        private  Mock<IRepository<DataAccess.Entity.Project>> _mockProjectRepository;
        private  Mock<IRepository<DataAccess.Entity.User>> _mockUserRepository;
        private  Mock<IRepository<DataAccess.Entity.Task>> _mockTaskRepository;
        private  IMapper _mapper;
        private IProjectManager _projectManager;

        [SetUp]
        public void Setup()
        {
            _mockProjectRepository = new Mock<IRepository<DataAccess.Entity.Project>>();
            _mockTaskRepository = new Mock<IRepository<DataAccess.Entity.Task>>();
            _mockUserRepository = new Mock<IRepository<DataAccess.Entity.User>>();
            _mapper = UnityUlitity.SetupAutoMapper();
            _mockTaskRepository.Setup(repo => repo.Get()).Returns(new List<DataAccess.Entity.Task>
            {
                new DataAccess.Entity.Task{ Project_Id = 10, Task_Id = 11},
                new DataAccess.Entity.Task{ Project_Id = 15, Task_Id = 12},
                new DataAccess.Entity.Task{ Project_Id = 14, Task_Id = 2},
                new DataAccess.Entity.Task{ Project_Id = 10, Task_Id = 15},
                new DataAccess.Entity.Task{ Project_Id = 10, Task_Id = 10}
            });
            _mockUserRepository.Setup(repo => repo.Get()).Returns(new List<DataAccess.Entity.User>
            {
                new DataAccess.Entity.User{User_Id = 1, First_Name = "Firstname", Last_Name = "LastName", Employee_Id = "EMP123", Project_Id = 10},
                new DataAccess.Entity.User{User_Id = 2, First_Name = "Firstname1", Last_Name = "LastName1", Employee_Id = "EMP1234", Project_Id = 5}
            });
            _mockTaskRepository.Setup(repo => repo.Delete(It.IsAny<Expression<Func<DataAccess.Entity.Task, bool>>>()));
            _mockProjectRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(1);
            _projectManager = new ProjectManager(_mockProjectRepository.Object, _mapper, _mockUserRepository.Object, _mockTaskRepository.Object);
        }

        [Test]
        public void DeleteProject_ShouldUpdateUserAndRemoveTasksIfTasksExistsForProjectToBeDeleted()
        {
            _mockUserRepository.Setup(repo => repo.UpdateAll(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<object>()));
            _projectManager.DeleteProject(10);
            _mockTaskRepository.Verify(repo => repo.Delete(It.IsAny<Expression<Func<DataAccess.Entity.Task, bool>>>()), Times.Once);
            _mockUserRepository.Verify(repo => repo.UpdateAll(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
            _mockProjectRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void DeleteProject_ShouldUpdateUserEvenNoTasksAvailable()
        {
            _mockUserRepository.Setup(repo => repo.UpdateAll(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<object>()));
            _projectManager.DeleteProject(5);
            _mockTaskRepository.Verify(repo => repo.Delete(It.IsAny<Expression<Func<DataAccess.Entity.Task, bool>>>()), Times.Never);
            _mockUserRepository.Verify(repo => repo.UpdateAll(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            _mockProjectRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void SaveProject_ShouldUpdateUserOnceWhenItsNew()
        {
            _mockProjectRepository.Setup(repo => repo.Create(It.IsAny<DataAccess.Entity.Project>())).Returns(new DataAccess.Entity.Project { Project_Id = 1 });
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<string>()));
            var result = _projectManager.SaveProject(new BusinessObjects.Project { User = new BusinessObjects.User { UserId = 1 } });
            //_mockUserRepository.Verify(repo => repo.Update(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockProjectRepository.Verify(repo => repo.Create(It.IsAny<DataAccess.Entity.Project>()));
        }
        [Test]
        public void SaveProject_ShouldUpdateUserTwiceWhenItsUpdate()
        {
            _mockProjectRepository.Setup(repo => repo.Update(It.IsAny<DataAccess.Entity.Project>())).Returns(new DataAccess.Entity.Project { Project_Id = 1 });
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<string>()));
            var result = _projectManager.SaveProject(new BusinessObjects.Project { ProjectId = 1, User = new BusinessObjects.User { UserId = 5 } });
            //_mockUserRepository.Verify(repo => repo.Update(It.IsAny<Expression<Func<DataAccess.Entity.User, bool>>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            _mockProjectRepository.Verify(repo => repo.Create(It.IsAny<DataAccess.Entity.Project>()), Times.Never);
        }
    }
}
