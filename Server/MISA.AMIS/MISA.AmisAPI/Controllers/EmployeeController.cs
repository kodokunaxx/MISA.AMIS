using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AmisAPI.Controllers
{
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("paging")]
        public IActionResult GetAll([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            ServiceResult serviceResult = _employeeService.GetAll(pageIndex, pageSize);
            return Ok(serviceResult);
        }

        [HttpGet("filter")]
        public IActionResult GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = _employeeService.GetFilter(employeeCode, fullName, phoneNumber, pageIndex, pageSize);
            return Ok(serviceResult);
        }

        [HttpGet("new-code")]
        public IActionResult GetNewCode()
        {
            ServiceResult serviceResult = _employeeService.GetNewCode();
            return Ok(serviceResult);
        }
    }
}
