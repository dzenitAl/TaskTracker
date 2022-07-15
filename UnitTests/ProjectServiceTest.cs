using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Xunit;
using Moq;
using AutoMapper;
using BusinessLogicLayer.Entities;
using System;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskTrackerUnitTests
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectRepository> projectRepository;
        private readonly Mock<IProjectTaskRepository> projectTaskRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly ProjectService _projectServiceTest;

        public ProjectServiceTest()
        {
            projectRepository = new Mock<IProjectRepository>();
            projectTaskRepository = new Mock<IProjectTaskRepository>();
            _mapper = new Mock<IMapper>();

            _projectServiceTest = new ProjectService(projectRepository.Object,projectTaskRepository.Object, _mapper.Object);
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

            var result = await _projectServiceTest.AddProjectAsync(newProject);

            Assert.Equal(newProject.Name, result.Name);
            Assert.Equal(newProject.Priority, result.Priority);
            Assert.Equal(newProject.Status, result.Status);
            Assert.Equal(newProject.StartDate, result.StartDate);
            Assert.Equal(newProject.CompletionDate, result.CompletionDate);
        }

        [Fact]
        public async void Task_Add_NullProject_Return_ThrowExceptionMessage()
        {
            var newProject = new Project() { };
            var newProjectDto = new ProjectDto() { };
                
            newProject = null;
            newProjectDto = null;

            _mapper.Setup(m => m.Map<Project>(It.IsAny<ProjectDto>())).Returns(newProject);
            projectRepository.Setup(x => x.AddProjectAsync(newProjectDto)).ThrowsAsync(new Exception("Project not found!"));

            var exceptionThrown = await Assert.ThrowsAsync<Exception>(async () => await _projectServiceTest.AddProjectAsync(newProject));

            Assert.Equal("Project not found!", exceptionThrown.Message);
        }

        [Fact]
        public async void Task_GetAllProjects_ReturnNumberOfProjects()
        {
            var projectsList = new List<ProjectDto>
            {
                new ProjectDto {Id = 1, Name="Project 1", StartDate=DateTime.Now, CompletionDate=DateTime.Now, Priority = 1, Status=DataAccessLayer.Enums.ProjectStatus.NotStarted},
                new ProjectDto {Id = 2, Name="Project 2", StartDate=DateTime.Now, CompletionDate=DateTime.Now, Priority = 2, Status=DataAccessLayer.Enums.ProjectStatus.Active},
            };

            projectRepository.Setup(x => x.GetAllProjectsAsync()).ReturnsAsync(projectsList);
            
            var project = await _projectServiceTest.GetAllProjectsAsync();
            
            Assert.Equal(2, project.Count());
        }

        [Fact]
        public async void Task_GetAllProjects_EmptyDB_Return_ThrowExceptionMessage()
        {
            var projectsList = new List<ProjectDto> {  };

            projectRepository.Setup(x => x.GetAllProjectsAsync()).ThrowsAsync(new Exception("No Projects in the database!"));

            var exceptionThrown = await Assert.ThrowsAsync<Exception>(async () => await _projectServiceTest.GetAllProjectsAsync());

            Assert.Equal("No Projects in the database!", exceptionThrown.Message);
        }


        [Theory]
        [InlineData(1)]
        public async void Task_GetProjectId_Return_OkResult(int projectId)
        {
            var firstProjectDto = new ProjectDto { Id = 1, Name = "Project 1", StartDate = DateTime.Now, CompletionDate = DateTime.Now, Priority = 1, Status = DataAccessLayer.Enums.ProjectStatus.NotStarted };

            projectRepository.Setup(x => x.GetProjectAsync(firstProjectDto.Id)).ReturnsAsync(firstProjectDto);

            _mapper.Setup(m => m.Map<ProjectDto>(It.IsAny<Project>())).Returns(firstProjectDto);

            var project = await _projectServiceTest.GetProjectAsync(firstProjectDto.Id);

            Assert.Equal(firstProjectDto.Id, projectId);
        }

        [Theory]
        [InlineData(11)]
        public async void Task_GetProjectId_Return_ThrowExceptionMessage(int projectId)
        {
            var firstProjectDto = new ProjectDto { Id = 1, Name = "Project 1", StartDate = DateTime.Now, CompletionDate = DateTime.Now, Priority = 1, Status = DataAccessLayer.Enums.ProjectStatus.NotStarted };

            projectRepository.Setup(x => x.GetProjectAsync(firstProjectDto.Id)).ThrowsAsync(new Exception($"No Project with Id: {projectId} in the database!"));

            _mapper.Setup(m => m.Map<ProjectDto>(It.IsAny<Project>())).Returns(firstProjectDto);

            var exceptionThrown = await Assert.ThrowsAsync<Exception>(async () => await _projectServiceTest.GetProjectAsync(firstProjectDto.Id));

            Assert.Equal($"No Project with Id: {projectId} in the database!", exceptionThrown.Message);
        }
    }
}
