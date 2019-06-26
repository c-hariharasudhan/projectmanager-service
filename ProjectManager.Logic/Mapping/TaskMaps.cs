using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Logic.Mapping
{
    public class TaskMaps : Profile 
    {
        public TaskMaps()
        {
            CreateMap<DataAccess.Entity.Task, BusinessObjects.Task>()
                .ForMember(bo => bo.TaskId, options => options.MapFrom(entity => entity.Task_Id))
                .ForMember(bo => bo.TaskName, options => options.MapFrom(entity => entity.Task_Name))
                .ForMember(bo => bo.StartDate, options => options.MapFrom(entity => entity.Start_Date))
                .ForMember(bo => bo.EndDate, options => options.MapFrom(entity => entity.End_Date))
                .ForMember(bo => bo.Priority, options => options.MapFrom(entity => entity.Priority))
                .ForMember(bo => bo.Status, options => options.MapFrom(entity => entity.Status))
                .ForMember(bo => bo.ParentId, options => options.MapFrom(entity => entity.ParentTask == null ? 0 : entity.ParentTask.Parent_Id))
                .ForMember(bo => bo.ParentTaskName, options => options.MapFrom(entity => entity.ParentTask == null ? "" : entity.ParentTask.Parent_Task))
                .ForMember(bo => bo.ProjectId, options => options.MapFrom(entity => entity.Project_Id))
                .ForMember(bo => bo.User, options => options.MapFrom(entity => entity.Users.FirstOrDefault()));
            CreateMap<BusinessObjects.Task, DataAccess.Entity.Task>()
                .ForMember(entity => entity.Task_Id, options => options.MapFrom(bo => bo.TaskId))
                .ForMember(entity => entity.Task_Name, options => options.MapFrom(bo => bo.TaskName))
                .ForMember(entity => entity.Start_Date, options => options.MapFrom(bo => bo.StartDate))
                .ForMember(entity => entity.End_Date, options => options.MapFrom(bo => bo.EndDate))
                .ForMember(entity => entity.Priority, options => options.MapFrom(bo => bo.Priority))
                .ForMember(entity => entity.Status, options => options.MapFrom(bo => bo.Status))
                .ForMember(entity => entity.Parent_Id, options => options.MapFrom(bo => bo.ParentId))
                .ForMember(entity => entity.Project_Id, options => options.MapFrom(bo => bo.ProjectId));

            CreateMap<DataAccess.Entity.ParentTask, BusinessObjects.ParentTask>()
                .ForMember(bo => bo.ParentTaskId, options => options.MapFrom(entity => entity.Parent_Id))
                .ForMember(bo => bo.ParentTaskName, options => options.MapFrom(entity => entity.Parent_Task));

            CreateMap<BusinessObjects.ParentTask, DataAccess.Entity.ParentTask>()
                .ForMember(bo => bo.Parent_Id, options => options.MapFrom(entity => entity.ParentTaskId))
                .ForMember(bo => bo.Parent_Task, options => options.MapFrom(entity => entity.ParentTaskName));

        }
    }
}
