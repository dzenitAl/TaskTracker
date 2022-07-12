using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
    public class ProjectTaskDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDto Project { get; set; }
    }
}
