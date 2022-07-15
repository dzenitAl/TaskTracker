using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TaskTrackerUnitTests
{
    public class ProjectTaskServiceTest
    {
        private readonly Mock<IProjectTaskRepository> projectTaskRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly ProjectTaskService _projectTaskServiceTest;

        public ProjectTaskServiceTest()
        {
            projectTaskRepository = new Mock<IProjectTaskRepository>();
            _mapper = new Mock<IMapper>();

            _projectTaskServiceTest = new ProjectTaskService(projectTaskRepository.Object, _mapper.Object);
        }

        [Fact]
        public async void Task_Add_ValidProjectTask_Return_OkResult()
        {
            var newProjectTask = new ProjectTask
            {
                Name = "New unit test Project Task",
                Description = "Description for New unit test Project Task",
                Priority = 5,
                Status = DataAccessLayer.Enums.TaskStatus.ToDo,
                ProjectId = 1
            };
            var newProjectTaskDto = new ProjectTaskDto
            {
                Name = "New unit test Project Task",
                Description = "Description for New unit test Project Task",
                Priority = 5,
                Status = DataAccessLayer.Enums.TaskStatus.ToDo,
                ProjectId = 1
            };

            projectTaskRepository.Setup(m => m.AddProjectTaskAsync(It.IsAny<ProjectTaskDto>())).ReturnsAsync(newProjectTaskDto);

            _mapper.Setup(m => m.Map<ProjectTask>(It.IsAny<ProjectTaskDto>())).Returns(newProjectTask);
            _mapper.Setup(m => m.Map<ProjectTaskDto>(It.IsAny<ProjectTask>())).Returns(newProjectTaskDto);

            var result = await _projectTaskServiceTest.AddProjectTaskAsync(newProjectTask);

            Assert.Equal(newProjectTask.Name, result.Name);
            Assert.Equal(newProjectTask.Priority, result.Priority);
            Assert.Equal(newProjectTask.Status, result.Status);
            Assert.Equal(newProjectTask.Description, result.Description);
            Assert.Equal(newProjectTask.ProjectId, result.ProjectId);
        }

        [Fact]
        public async void Task_Add_NullProjectTask_Return_ThrowExceptionMessage()
        {
            var newProjectTask = new ProjectTask() { };
            var newProjectTaskDto = new ProjectTaskDto() { };

            newProjectTask = null;
            newProjectTaskDto = null;

            _mapper.Setup(m => m.Map<ProjectTask>(It.IsAny<ProjectTaskDto>())).Returns(newProjectTask);
            projectTaskRepository.Setup(x => x.AddProjectTaskAsync(newProjectTaskDto)).ThrowsAsync(new Exception("Project task not found!"));

            var exceptionThrown = await Assert.ThrowsAsync<Exception>(async () => await _projectTaskServiceTest.AddProjectTaskAsync(newProjectTask));

            Assert.Equal("Project task not found!", exceptionThrown.Message);
        }
       
        [Fact]
        public async void Task_GetAllProjectTasks_EmptyDB_Return_ThrowExceptionMessage()
        {
            projectTaskRepository.Setup(x => x.GetAllProjectTasksAsync()).ThrowsAsync(new Exception("No Project Tasks in the database!"));

            var exceptionThrown = await Assert.ThrowsAsync<Exception>(async () => await _projectTaskServiceTest.GetAllProjectTasksAsync());

            Assert.Equal("No Project Tasks in the database!", exceptionThrown.Message);
        }

        [Fact]
        public async void Task_GetAllProjectTasks_ReturnNumberOfProjectTasks()
        {
            var projectTasksList = new List<ProjectTaskDto>
            {
                new ProjectTaskDto {Id = 1, Name="Project Task 1", Description="Description for New Project Task 1", Priority = 1, Status=DataAccessLayer.Enums.TaskStatus.ToDo, ProjectId = 1},
                new ProjectTaskDto {Id = 2, Name="Project Task 2", Description="Description for New Project Task 2", Priority = 2, Status=DataAccessLayer.Enums.TaskStatus.InProgress, ProjectId = 2},
            };

            projectTaskRepository.Setup(x => x.GetAllProjectTasksAsync()).ReturnsAsync(projectTasksList);

            var projectTasks = await _projectTaskServiceTest.GetAllProjectTasksAsync();

            Assert.Equal(2, projectTasks.Count());
        }
    }
}
