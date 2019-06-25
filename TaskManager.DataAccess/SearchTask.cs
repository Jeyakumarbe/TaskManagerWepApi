using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess
{
    public class SearchTask
    {
        public string Task;
        public int? ParentTaskID;
        public string ParentTask;
        public DateTime? Start_Date;
        public DateTime? End_Date;
        public int? PriorityFrom;
        public int? PriorityTo;
    }
}
