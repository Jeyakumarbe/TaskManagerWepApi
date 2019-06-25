using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TaskManager.DataAccess
{
    [Table("Task")]
    public class Task
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Task_ID { get; set; }
        
        public int? ParentTask_ID { get; set; }
        
        public string TaskName { get; set; }     
        
        public string ParentTaskName { get; set; }
        
        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }
        
        public int Priority { get; set; }
        
        public string Status { get; set; }

        public virtual Task ParentTask { get; set; }

        public virtual ICollection<Task> ChildTasks { get; set; }
    }
}
