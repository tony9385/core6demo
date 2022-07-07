using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class AppModel
    {
        public string ControlType { get; set; }    
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        // GET: api/<AppController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
          Process[] processes = Process.GetProcessesByName("WindowsFormsApp1");
            Process myProcess = processes[0];
            Console.WriteLine($"  Physical memory usage     : {myProcess.WorkingSet64}");
            Console.WriteLine($"  Base priority             : {myProcess.BasePriority}");
            Console.WriteLine($"  Priority class            : {myProcess.PriorityClass}");
            Console.WriteLine($"  User processor time       : {myProcess.UserProcessorTime}");
            Console.WriteLine($"  Privileged processor time : {myProcess.PrivilegedProcessorTime}");
            Console.WriteLine($"  Total processor time      : {myProcess.TotalProcessorTime}");
            Console.WriteLine($"  Paged system memory size  : {myProcess.PagedSystemMemorySize64}");
            Console.WriteLine($"  Paged memory size         : {myProcess.PagedMemorySize64}");


            return "value";
        }

        // POST api/<AppController>
        [HttpPost]
        public void Post([FromBody] AppModel value)
        {
            if(value.ControlType.ToLower()=="close")
            {
                ColoseApp();    
            }
            
            if(value.ControlType.ToLower()=="open")
            {
                OpenApp();    
            }
        }

        private bool ColoseApp()
        {
            foreach (var item in Process.GetProcessesByName("WindowsFormsApp1"))
            {
                item.Kill();
            }

            return true;    
        }
        private bool OpenApp()
        {
            var path = @"C:\Users\tony\source\repos\WebApplication1\WindowsFormsApp1\bin\Debug\WindowsFormsApp1.exe"; ;
            Process.Start(path);
            return true;    
        }


        // PUT api/<AppController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
