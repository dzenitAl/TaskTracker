using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataAccessLayer.Enums
{
    public enum TaskStatus
    {
        [Description("To do")]
        ToDo,
        [Description("In progress")]
        InProgress,
        Done
    }
}
