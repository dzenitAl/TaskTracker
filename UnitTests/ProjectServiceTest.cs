using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Xunit;
using Moq;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BusinessLogicLayer.Entities;
using System;
using DataAccessLayer.Models;

namespace TaskTrackerUnitTests
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectRepository> projectRepository;
        private readonly Mock<IProjectTaskRepository> projectTaskRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly ProjectService _projectService;
       

        public ProjectServiceTest()
        {
            projectRepository = new Mock<IProjectRepository>();
            projectTaskRepository = new Mock<IProjectTaskRepository>();
            _mapper = new Mock<IMapper>();

            _projectService = new ProjectService(projectRepository.Object,projectTaskRepository.Object, _mapper.Object);
        }

        [Fact]
        public async void Task_Add_ValidProject_Return_OkResult()
        {
            var newProject = new Project
            {
                Name = "New unit test project",
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Priority = 5,
                Status = DataAccessLayer.Enums.ProjectStatus.Active
            };
            var newProjectDto = new ProjectDto
            {
                Name = "New unit test project",
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Priority = 5,
                Status = DataAccessLayer.Enums.ProjectStatus.Active
            };

            projectRepository.Setup(m => m.AddProjectAsync(It.IsAny<ProjectDto>())).ReturnsAsync(newProjectDto);

            _mapper.Setup(m => m.Map<Project>(It.IsAny<ProjectDto>())).Returns(newProject);
            _mapper.Setup(m => m.Map<ProjectDto>(It.IsAny<Project>())).Returns(newProjectDto);

            var result = await _projectService.AddProjectAsync(newProject);

            Assert.Equal(newProject.Name, result.Name);
            Assert.Equal(newProject.Priority, result.Priority);
            Assert.Equal(newProject.Status, result.Status);
            Assert.Equal(newProject.StartDate, result.StartDate);
            Assert.Equal(newProject.CompletionDate, result.CompletionDate);
        }
    }
}
