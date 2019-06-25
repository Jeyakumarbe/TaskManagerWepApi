using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using TaskManager.WebAPI.Controllers;
using TaskManager.DataAccess;
using NUnit.Framework;
using AutoMapper;


namespace TaskManager.Nuint
{
    [TestFixture]
    public class TaskManagerTest
    {
        [Test]
        public void GetAllTasksTest_Success()
        {
            TaskController taskController = new TaskController();
            var response = taskController.GetAllTask();
            var responseResult = response as OkNegotiatedContentResult<List<Task>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task.Task_ID);
                Assert.IsNotNull(task.TaskName);
                Assert.IsNotNull(task.Priority);
                Assert.IsNotNull(task.Start_Date);
                Assert.IsNotNull(task.End_Date);
            }
        }

        [Test]
        public void GetParentTasksTest_Success()
        {
            TaskController taskController = new TaskController();
            var response = taskController.GetParentTask();
            var responseResult = response as OkNegotiatedContentResult<List<DropDown>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task);
            }
        }

        [Test]
        public void GetAllTastByID_Success()
        {
            TaskController taskController = new TaskController();
            var response = taskController.GetTaskById(1);
            var responseResult = response as OkNegotiatedContentResult<Task>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            Assert.AreEqual(1, responseResult.Content.Task_ID);
            Assert.IsNotNull(responseResult.Content.TaskName);
            Assert.IsNotNull(responseResult.Content.Priority);
            Assert.IsNotNull(responseResult.Content.Start_Date);
            Assert.IsNotNull(responseResult.Content.End_Date);
        }

        [Test]
        public void AddTaskTest_Success()
        {
           
            TaskController controller = new TaskController();
            Task model = new Task
            {
                TaskName = "Tast Task",
                ParentTask_ID = 1,
                ParentTaskName = "Task1",
                Priority = 5,
                Start_Date = DateTime.Today,
                End_Date = DateTime.Today.AddDays(10)
            };

            // Act
            var response = controller.AddTask(model);

            var responseResult = response as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            // Assert
            Assert.IsTrue(responseResult.Content == "Success");
        }

        [Test]
        public void UpdateTaskTest_Success()
        {
          
            TaskController controller = new TaskController();
            Task model = new Task
            {
                Task_ID = 3,
                TaskName = "Tast Task",
                ParentTask_ID = 1,
                ParentTaskName = "Task1",
                Priority = 30,
                Start_Date = DateTime.Today,
                End_Date = DateTime.Today.AddDays(10)
            };


            // Act
            var response = controller.UpdateTask(model);

            // Assert
            var responseResult = response as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            // Assert
            Assert.IsTrue(responseResult.Content == "Success");
        }

        [Test]
        public void EndTaskTest_Success()
        {
            
            // Arrange
            TaskController controller = new TaskController();

            // Act
            var response = controller.EndTask(3);

            // Assert
            
            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [Test]
        public void DeleteTaskTest_Success()
        {

            // Arrange
            TaskController controller = new TaskController();

            // Act
            var response = controller.DeleteTask(2);

            // Assert
            var responseResult = response as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            // Assert
            Assert.IsTrue(responseResult.Content == "Success");
        }
    }
}
