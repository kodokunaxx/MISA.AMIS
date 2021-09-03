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

    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IBaseService<Department> baseService) : base(baseService)
        {
        }
    }
}
