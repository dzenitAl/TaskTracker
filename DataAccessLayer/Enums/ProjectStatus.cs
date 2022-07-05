using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataAccessLayer.Enums
{
    public enum ProjectStatus
    {
        [Description("Not started")] 
        NotStarted,
        Active,
        Completed
    }
}
