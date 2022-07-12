using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessLogicLayer.Entities
{
    public class Project
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }
        [JsonIgnore]
        public List<ProjectTask> Tasks { get; set; }
    }
}
