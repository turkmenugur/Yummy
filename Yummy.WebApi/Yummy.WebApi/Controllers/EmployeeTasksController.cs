using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebApi.Context;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeTasksController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EmployeeTaskList()
        {
            var EmployeeTaskList = _context.EmployeeTasks.ToList();
            return Ok(EmployeeTaskList);
        }

        [HttpPost]
        public IActionResult CreateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Add(EmployeeTask);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteEmployeeTask(int id)
        {
            var EmployeeTask = _context.EmployeeTasks.Find(id);
            _context.EmployeeTasks.Remove(EmployeeTask);
            _context.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet("GetEmployeeTask")]
        public IActionResult GetEmployeeTask(int id)
        {
            var EmployeeTask = _context.EmployeeTasks.Find(id);
            return Ok(EmployeeTask);
        }

        [HttpPut]
        public IActionResult UpdateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Update(EmployeeTask);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }
    }
}
