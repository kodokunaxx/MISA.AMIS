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
    /// <summary>
    /// EmployeeController
    /// </summary>
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
        public IActionResult GetFilter(string keyword, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = _employeeService.GetFilter(keyword, keyword, keyword, pageIndex, pageSize);
            return Ok(serviceResult);
        }

        [HttpGet("new-code")]
        public IActionResult GetNewCode()
        {
            ServiceResult serviceResult = _employeeService.GetNewCode();
            return Ok(serviceResult);
        }


        [HttpGet("Export")]
        public IActionResult ExportExcel(string keyword, int pageIndex, int pageSize)
        {
            var stream = _employeeService.ExportExcel(keyword, pageIndex, pageSize);
            string excelName = $"Danh-sach-nhan-vien-{DateTime.Now.ToString("yyyy_MM_dd")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
