using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DataAccess;
using TaskManager.BusinessAccess;
using System.Web.Http.Description;


namespace TaskManager.WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class TaskController : ApiController
    {
        TaskManagerService service = null;

        [HttpPost]
        [Route("GetAllTaskBySearch")]
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetAllTaskBySearch(SearchTask Search)
        {
            service = new TaskManagerService();
            List<Task> tasks = service.GetAllTaskBySearchCriteria(Search);
            return Ok(tasks);
        }

        [HttpPost]
        [Route("GetAllTask")]
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetAllTask()
        {
            service = new TaskManagerService();
            List<Task> tasks = service.GetAllTask();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetParentTask")]
        [ResponseType(typeof(void))]
        public IHttpActionResult GetParentTask()
        {
            service = new TaskManagerService();
            List<DropDown> DD =service.GetAllParent();
            return Ok(DD);
        }

        [HttpGet]
        [Route("GetTaskById/{Id}")]
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTaskById(int Id)
        {
            service = new TaskManagerService();
            Task task = service.GetTaskByID(Id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Route("AddTask")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddTask(Task newTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                service = new TaskManagerService();
                service.AddTask(newTask);
                return Ok("Success");
            }
            catch (Exception)
            {

                return Ok("Error");

            }
            
        }

        [HttpPost]
        [Route("UpdateTask")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateTask(Task editTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                service = new TaskManagerService();
                service.EditTask(editTask);
                return Ok("Success");
            }
            catch (Exception)
            {

                return Ok("Error");
            }
           
        }

        [HttpGet]
        [Route("DeleteTask/{Id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteTask(int Id)
        {
            try
            {
                service = new TaskManagerService();
                service.DeleteTask(Id);
                return Ok("Success");
            }
            catch (Exception)
            {

                return Ok("Error");
            }
           
        }

        [HttpGet]
        [Route("EndTask/{ID}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EndTask(int ID)
        {
            try
            {
                service = new TaskManagerService();
                service.EndTask(ID, "EndTask");
                return Ok();
            }
            catch (Exception)
            {

                return Ok("Error");
            }
            
        }

        

    }
}
