using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;
using DAL = TaskManager.DataAccess;

namespace TaskManager.BusinessAccess
{
    public class TaskManagerService
    {
        public List<DAL.Task> GetAllTaskBySearchCriteria(DAL.SearchTask Search)
        {
            List<DAL.Task> Task = new List<DAL.Task>();
            using (MyContext DB = new MyContext())
            {
              var TaskDetails = (from p in DB.Task
                           where 
                           (Search.Task == null || Search.Task == p.TaskName) &&
                           (Search.ParentTaskID == null || Search.ParentTaskID == p.ParentTask_ID) &&
                           (Search.Start_Date == null||Search.Start_Date == p.Start_Date) &&
                           (Search.End_Date == null || Search.End_Date == p.End_Date) &&
                           (Search.PriorityFrom == null || Search.PriorityFrom == p.Priority) &&
                           (Search.PriorityTo == null || Search.PriorityTo == p.Priority) 
                        select new 
                        {
                            Task_ID = p.Task_ID,
                            TaskName = p.TaskName,
                            ParentTask_ID = p.ParentTask_ID,
                            ParentTaskName = p.ParentTaskName,
                            Start_Date = p.Start_Date,
                            End_Date = p.End_Date,
                            Priority = p.Priority,
                            Status = p.Status
                        }
                        ).ToList();

                foreach (var item in TaskDetails)
                {
                    DAL.Task T = new DAL.Task();
                    T.Task_ID = item.Task_ID;
                    T.TaskName = item.TaskName;
                    T.ParentTask_ID = item.ParentTask_ID;
                    T.ParentTaskName = item.ParentTaskName;
                    T.Start_Date = item.Start_Date;
                    T.End_Date = item.End_Date;
                    T.Priority = item.Priority;
                    T.Status = item.Status;
                    Task.Add(T);
                }

            }
            return Task;
        }

        public List<DAL.Task> GetAllTask()
        {
            List<DAL.Task> Task = new List<DAL.Task>();
            using (MyContext DB = new MyContext())
            {
                var TaskDetails = (from p in DB.Task
                        select new 
                        {
                            Task_ID = p.Task_ID,
                            TaskName = p.TaskName,
                            ParentTask_ID = p.ParentTask_ID,
                            ParentTaskName = p.ParentTaskName,
                            Start_Date = p.Start_Date,
                            End_Date = p.End_Date,
                            Priority = p.Priority,
                            Status = p.Status
                        }
                        ).ToList();

                foreach (var item in TaskDetails)
                {
                    DAL.Task T = new DAL.Task();
                    T.Task_ID = item.Task_ID;
                    T.TaskName = item.TaskName;
                    T.ParentTask_ID = item.ParentTask_ID;
                    T.ParentTaskName = item.ParentTaskName;
                    T.Start_Date = item.Start_Date;
                    T.End_Date = item.End_Date;
                    T.Priority = item.Priority;
                    T.Status = item.Status;
                    Task.Add(T);
                }

            }
            return Task;
        }

        public List<DAL.DropDown> GetAllParent()
        {
            List<DAL.DropDown> DD = new List<DAL.DropDown>();
            using (MyContext DB = new MyContext())
            {
                DD = (from T in DB.Task
                 where T.ParentTask_ID == null
                 select new DAL.DropDown
                 {
                     Id = T.Task_ID,
                     Value = T.TaskName
                 }).ToList();
            }
            return DD;
        }

        public DAL.Task GetTaskByID(int TaskID)
        {
            DAL.Task Task = new DAL.Task();
            using (MyContext DB = new MyContext())
            {
                var TaskDetails = (from T in DB.Task
                        where T.Task_ID == TaskID
                        select new
                        {
                            Task_ID = T.Task_ID,
                            TaskName = T.TaskName,
                            ParentTask_ID = T.ParentTask_ID,
                            ParentTaskName = T.ParentTaskName,
                            Start_Date = T.Start_Date,
                            End_Date = T.End_Date,
                            Priority = T.Priority,
                            Status = T.Status
                        }).FirstOrDefault();
                if(TaskDetails != null)
                {
                    Task.Task_ID = TaskDetails.Task_ID;
                    Task.TaskName = TaskDetails.TaskName;
                    Task.ParentTask_ID = TaskDetails.ParentTask_ID;
                    Task.ParentTaskName = TaskDetails.ParentTaskName;
                    Task.Start_Date = TaskDetails.Start_Date;
                    Task.End_Date = TaskDetails.End_Date;
                    Task.Priority = TaskDetails.Priority;
                    Task.Status = TaskDetails.Status;
                }

              
            }

            return Task;
        }

        public bool AddTask(DAL.Task Task)
        {
            try
            {
                if(Task.ParentTask_ID != null)
                {
                    List<DAL.DropDown> DD = new List<DAL.DropDown>();
                    DD = GetAllParent();
                    Task.ParentTaskName = DD.Where(x => x.Id == Task.ParentTask_ID).Select(x => x.Value).FirstOrDefault();
                }
                using (MyContext DB = new MyContext())
                {
                    DB.Task.Add(Task);
                    DB.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public bool DeleteTask(int TaskID)
        {
            try
            {
                using (MyContext DB = new MyContext())
                {
                    var taskDB = (from t in DB.Task where t.Task_ID == TaskID select t).FirstOrDefault();
                    if(taskDB != null)
                    {
                        DB.Task.Remove(taskDB);
                        DB.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditTask(DAL.Task Task)
        {
            try
            {
                if (Task.ParentTask_ID != null)
                {
                    List<DAL.DropDown> DD = new List<DAL.DropDown>();
                    DD = GetAllParent();
                    Task.ParentTaskName = DD.Where(x => x.Id == Task.ParentTask_ID).Select(x => x.Value).FirstOrDefault();
                }
                using (MyContext DB = new MyContext())
                {
                    var taskDB = (from t in DB.Task where t.Task_ID == Task.Task_ID select t).FirstOrDefault();
                    var ChildTaskDB = (from t in DB.Task where t.ParentTask_ID == Task.Task_ID && t.ParentTaskName != Task.TaskName select t);
                    if (taskDB != null)
                    {
                        taskDB.TaskName = Task.TaskName;
                        taskDB.ParentTask_ID = Task.ParentTask_ID;
                        taskDB.ParentTaskName = Task.ParentTaskName;
                        taskDB.Start_Date = Task.Start_Date;
                        taskDB.End_Date = Task.End_Date;
                        taskDB.Priority = Task.Priority;
                        taskDB.Status = Task.Status;
                        DB.SaveChanges();                       
                    }
                    if(ChildTaskDB != null)
                    {
                        foreach (var item in ChildTaskDB)
                        {
                            item.ParentTaskName = Task.TaskName;

                        }
                        DB.SaveChanges();
                    }
                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EndTask(int TaskID,string Status)
        {
            try
            {
                using (MyContext DB = new MyContext())
                {
                    var taskDB = (from t in DB.Task where t.Task_ID == TaskID select t).FirstOrDefault();
                    if (taskDB != null)
                    {
                        taskDB.Status = Status;
                        DB.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
