using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.WebAPI.Controllers;
using NBench;

namespace TaskManager.PerformanceTest
{
    public class TestSuits : PerformanceTest<TestSuits>
    {
        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTaskMemory_Test()
        {
            var taskController = new TaskController();
            var response = taskController.GetAllTask();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetParentTaskMemory_Test()
        {
            var taskController = new TaskController();
            var response = taskController.GetParentTask();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTaskByIdMemory_Test()
        {
            var taskController = new TaskController();
            var response = taskController.GetTaskById(1);
        }
    }
}
