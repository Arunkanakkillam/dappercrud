using Dapperbegin.Model;
using Dapperbegin.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapperbegin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            var result = await _repository.AddAsync(employee);
            if (result > 0) return Ok(new { message = "Employee created successfully." });
            return BadRequest(new { message = "Failed to create employee." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            var result = await _repository.UpdateAsync(employee);
            if (result > 0) return Ok(new { message = "Employee updated successfully." });
            return NotFound(new { message = "Employee not found." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result > 0) return Ok(new { message = "Employee deleted successfully." });
            return NotFound(new { message = "Employee not found." });
        }
    }
}
