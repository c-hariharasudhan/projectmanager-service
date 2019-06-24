using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Mapping
{
    public class ProjectMaps : Profile
    {
        public ProjectMaps()
        {
            CreateMap<BusinessObjects.Project, DataAccess.Entity.Project>()
                .ForMember(entity => entity.Project_Id, options => options.MapFrom(bo => bo.ProjectId))
                .ForMember(entity => entity.Project_Name, options => options.MapFrom(bo => bo.ProjectName))
                .ForMember(entity => entity.Start_Date, options => options.MapFrom(bo => bo.StartDate))
                .ForMember(entity => entity.End_Date, options => options.MapFrom(bo => bo.EndDate))
                .ForMember(entity => entity.Priority, options => options.MapFrom(bo => bo.Priority))
                .ForMember(entity => entity.Tasks, options => options.Ignore())
                .ForMember(entity => entity.Users, options => options.Ignore());

            CreateMap<DataAccess.Entity.Project, BusinessObjects.Project>()
                .ForMember(bo => bo.ProjectId, options => options.MapFrom(entity => entity.Project_Id))
                .ForMember(bo => bo.ProjectName, options => options.MapFrom(entity => entity.Project_Name))
                .ForMember(bo => bo.StartDate, options => options.MapFrom(entity => entity.Start_Date))
                .ForMember(bo => bo.EndDate, options => options.MapFrom(entity => entity.End_Date))
                .ForMember(bo => bo.Priority, options => options.MapFrom(entity => entity.Priority));
        }
    }
}
