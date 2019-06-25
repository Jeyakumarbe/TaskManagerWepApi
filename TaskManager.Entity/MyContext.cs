using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DAL = TaskManager.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TaskManager.Entity
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name = MyContextDB")
        {

        }

        public DbSet<DAL.Task> Task { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

             modelBuilder.Entity<DAL.Task>()
                .Property(te => te.TaskName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<DAL.Task>()
               .Property(te => te.ParentTaskName)
               .HasMaxLength(100);
            modelBuilder.Entity<DAL.Task>()
                .HasMany(te => te.ChildTasks)
                .WithOptional(te => te.ParentTask)
                .HasForeignKey(te => te.ParentTask_ID)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
