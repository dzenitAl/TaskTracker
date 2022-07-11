using System;

namespace BusinessLogicLayer.Models
{
    public class ProjectFilters
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Priority { get; set; }
    }
}
