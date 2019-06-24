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
    public class UserMapsTests
    {
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            _mapper = UnityUlitity.SetupAutoMapper();
        }
        [Test]
        public void UserMaps_EntityToBusinessObject()
        {
            var entity = new DataAccess.Entity.User
            {
                User_Id = 111,
                First_Name = "Firstname",
                Last_Name = "Lastname",
                Employee_Id = "emp123"
            };
            var result = _mapper.Map<BusinessObjects.User>(entity);
            Assert.That(result.UserId == 111);
        }

        [Test]
        public void UserMaps_BusinessObjectToEntity()
        {
            var bo = new BusinessObjects.User
            {
                UserId = 111,
                FirstName = "Firstname",
                LastName = "Lastname",
                EmployeeId = "emp123"
            };
            var result = _mapper.Map<DataAccess.Entity.User>(bo);
            Assert.That(result.User_Id == 111);
        }
    }
}
