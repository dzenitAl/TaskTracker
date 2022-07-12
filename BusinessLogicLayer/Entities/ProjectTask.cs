using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using System.Text.Json.Serialization;

namespace BusinessLogicLayer.Entities
{
    public class ProjectTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        [JsonIgnore]
        public ProjectDto Project { get; set; }
    }
}
