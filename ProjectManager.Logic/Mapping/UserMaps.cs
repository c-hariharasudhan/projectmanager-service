using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Mapping
{
    public class UserMaps : AutoMapper.Profile
    {
        public UserMaps()
        {
            CreateMap<BusinessObjects.User, DataAccess.Entity.User>()
                .ForMember(u => u.User_Id, options => options.MapFrom(obj => obj.UserId))
                .ForMember(u => u.First_Name, options => options.MapFrom(obj => obj.FirstName))
                .ForMember(u => u.Last_Name, options => options.MapFrom(obj => obj.LastName))
                .ForMember(u => u.Employee_Id, options => options.MapFrom(obj => obj.EmployeeId))
                .ForMember(u => u.Project_Id, options => options.Ignore())
                .ForMember(u => u.Task_Id, options => options.Ignore());
            CreateMap<DataAccess.Entity.User, BusinessObjects.User>()
                .ForMember(u => u.UserId, options => options.MapFrom(obj => obj.User_Id))
                .ForMember(u => u.FirstName, options => options.MapFrom(obj => obj.First_Name))
                .ForMember(u => u.LastName, options => options.MapFrom(obj => obj.Last_Name))
                .ForMember(u => u.EmployeeId, options => options.MapFrom(obj => obj.Employee_Id));
        }
    }
}
