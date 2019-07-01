using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManager.DataAccess.Repository;
using ProjectManager.Infrastructure.Logging;
using ProjectManager.Logic.Interfaces;
using ProjectManager.Logic.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Tests.Managers
{
    [TestFixture]
    public class UserManagerTests
    {
        private Mock<IRepository<DataAccess.Entity.User>> _mockUserRepository;
        private Mock<ILogger> _mockLogger;
        private IMapper _mapper;
        private IUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _mapper = UnityUlitity.SetupAutoMapper();
            _mockLogger = new Mock<ILogger>();
            _mockUserRepository = new Mock<IRepository<DataAccess.Entity.User>>();
            _userManager = new UserManager(_mockUserRepository.Object, _mapper, _mockLogger.Object);
        }

        [Test]
        public void GetUsers_ReturnsValidUserList()
        {
            _mockUserRepository.Setup(repo => repo.Get()).Returns(new List<DataAccess.Entity.User>
            {
                new DataAccess.Entity.User{User_Id = 1, First_Name = "Firstname", Last_Name = "LastName", Employee_Id = "EMP123"},
                new DataAccess.Entity.User{User_Id = 2, First_Name = "Firstname1", Last_Name = "LastName1", Employee_Id = "EMP1234"}
            });
            var result = _userManager.GetUsers();
            Assert.That(result.Count() == 2);
        }

        [Test]
        public void SaveUser_ShouldDoInsertWhenUserIdEmpty()
        {
            _mockUserRepository.Setup(repo => repo.Create(It.IsAny<DataAccess.Entity.User>())).Returns(new DataAccess.Entity.User { User_Id = 1000 });
            var result = _userManager.SaveUser(new BusinessObjects.User { FirstName = "Hariharasudhan", LastName = "Chandramurthy", EmployeeId = "195500" });
            Assert.That(result.UserId == 1000);
            _mockUserRepository.Verify(repo => repo.Create(It.IsAny<DataAccess.Entity.User>()), Times.Once);
        }
        [Test]
        public void SaveUser_ShouldNotDoUpdateWhenUserIdEmpty()
        {
            _mockUserRepository.Setup(repo => repo.Create(It.IsAny<DataAccess.Entity.User>())).Returns(new DataAccess.Entity.User { User_Id = 1000 });
            var result = _userManager.SaveUser(new BusinessObjects.User { FirstName = "Hariharasudhan", LastName = "Chandramurthy", EmployeeId = "195500" });
            Assert.That(result.UserId == 1000);
            _mockUserRepository.Verify(repo => repo.Update(It.IsAny<DataAccess.Entity.User>()), Times.Never);
        }
    }
}
