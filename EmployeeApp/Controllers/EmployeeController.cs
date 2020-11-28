namespace EmployeeApp.Controllers
{
    using EmployeeApp.Data;
    using EmployeeApp.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeDbContext _context;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Insert About Me
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileMetadata"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeVM employeeVM)
        {
            try
            {
                if (employeeVM == null || !ModelState.IsValid)
                {
                    _logger.LogWarning($"Invalid request");
                    return BadRequest();
                }
                var empEntity = Map(employeeVM);
                await _context.Employees.AddAsync(empEntity);
                await _context.SaveChangesAsync();
                _logger.LogTrace($"Successfully Inserted/Updated employee record");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add about me with the error [{ex.Message}]");
                return StatusCode(500, ex);
            }
        }

        #region PRIVATE METHOD

        private Employee Map(EmployeeVM employeeVM)
        {
            return new Employee()
            {
                Name = employeeVM.Name,
                Age = employeeVM.Age
            };
        }

        #endregion PRIVATE METHOD
    }
}