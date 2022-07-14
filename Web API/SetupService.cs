using DataAccessLayer.EF;
using Microsoft.EntityFrameworkCore;

namespace Web_API
{
    public class SetupService
    {
        public void Init(TaskTrackerDbContext context)
        {
            context.Database.Migrate();
        }
    }
}