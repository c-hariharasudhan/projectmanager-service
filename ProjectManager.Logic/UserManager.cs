using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DataAccess.Repository;
using ProjectManager.DataAccess.Entity;
using ProjectManager.Logic.Interfaces;
using ProjectManager.BusinessObjects;
using AutoMapper;
using ProjectManager.Infrastructure.Logging;

namespace ProjectManager.Logic
{
    public class UserManager : IUserManager
    {
        public readonly IRepository<DataAccess.Entity.User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UserManager(IRepository<DataAccess.Entity.User> userRepository, IMapper mapper, ILogger logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
                
        public int DeleteUser(int userId)
        {
            return _userRepository.Delete(userId);
        }

        public BusinessObjects.User GetUserById(int userId)
        {
            var result = _userRepository.GetById(userId);
            return _mapper.Map<BusinessObjects.User>(result);
        }

        public IEnumerable<BusinessObjects.User> GetUsers()
        {            
            var result = _userRepository.Get();
            _logger.WriteMessage(GetType(), LogLevel.Info, string.Format("Total users : {0}", result.Count()));
            return _mapper.Map<IEnumerable<BusinessObjects.User>>(result);
        }

        public BusinessObjects.User SaveUser(BusinessObjects.User user)
        {
            var request = _mapper.Map<DataAccess.Entity.User>(user);
            DataAccess.Entity.User entity = request.User_Id == 0 ?
                _userRepository.Create(request) : _userRepository.Update(request);
            var result = _mapper.Map<BusinessObjects.User>(entity);
            //result.Status = true;
            //result.StatusMessage = request.User_Id == 0 ? "User added successfully!" : "User updated successfully!";
            return result;
        }
        
    }
}
