using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EF
{
    public class TaskTrackerDbContext : DbContext
    {
        public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options) { }

        public DbSet<ProjectDto> Projects { get; set; }
        public DbSet<ProjectTaskDto> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectDto>()
                .Property(b => b.Id)
                .IsRequired();

            modelBuilder.Entity<ProjectTaskDto>()
                .Property(b => b.Id)
                .IsRequired();

            modelBuilder.Entity<ProjectTaskDto>()
            .HasOne(p => p.Project)
            .WithMany()
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
