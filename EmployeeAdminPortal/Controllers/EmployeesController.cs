using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly ApplicationDBContext dbContext;

        public EmployeesController(ApplicationDBContext dbContext)

        {
            this.dbContext = dbContext;

        }
        [Route("~/api/GetEmployees")]
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var AllEmployees = dbContext.Employees.ToList();
            return Ok(AllEmployees);
        }

        [Route("~/api/GetEmployeeId/{id:guid}")]
        [HttpGet]
        //[Route("{id:guid}")]
        public IActionResult GetEmployeebyId(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null) {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }

        }

        [Route("~/api/UpdateEmployees/{id:guid}")]
        [HttpPut]
        public IActionResult UpdateEmployees(Guid id, [FromBody] UpdateEmployeesDto updateEmployeeDto)
        {
          
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            else
            {
            
                employee.Name = updateEmployeeDto.Name;
                employee.Email = updateEmployeeDto.Email;
                employee.PhoneNumber = updateEmployeeDto.PhoneNumber;
                employee.Address = updateEmployeeDto.Address;
                employee.City = updateEmployeeDto.City;

                dbContext.SaveChanges();

                return Ok(employee);
            }
        }



        [Route("~/api/AddEmployees")]
        [HttpPost]
        public IActionResult AddEmployees([FromBody] AddEmployeesDto addEmployeesDto)
        {
            if (addEmployeesDto == null)
            {
                return BadRequest("Employee data is null.");
            }

            var employeeEntity = new Employee()
            {
                Name = addEmployeesDto.Name,
                Email = addEmployeesDto.Email,
                PhoneNumber = addEmployeesDto.PhoneNumber,
                Address = addEmployeesDto.Address,
                City = addEmployeesDto.City
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }




        [HttpDelete]
        [Route("~/api/DeleteEmployee/{id:guid}")]
        public IActionResult DeleteEmployees(Guid id)
            {
                var employee = dbContext.Employees.Find(id);
                if (employee is null)
                {
                    return NotFound();
                }
                else
                {
                    dbContext.Employees.Remove(employee);
                    dbContext.SaveChanges();
                    return Ok(employee);
                }
            }
        }
    }


