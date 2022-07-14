using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BusinessLogicLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class TaskTrackerProfiles :Profile
    {
        public TaskTrackerProfiles()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
            CreateMap<TaskVM, ProjectTaskDto>().ReverseMap();
        }
    }
}
