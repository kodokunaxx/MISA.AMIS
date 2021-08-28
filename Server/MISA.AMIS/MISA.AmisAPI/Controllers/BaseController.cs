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
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Declare
        IBaseService<T> _baseService;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        // GET: api/v1/<BaseController>
        [HttpGet]
        public IActionResult Get()
        {
            ServiceResult serviceResult = _baseService.GetAll();
            return Ok(serviceResult);
        }

        // GET: api/v1/<BaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = _baseService.GetById(entityId);
            return Ok(serviceResult);
        }

        // POST: api/v1/<BaseController>
        [HttpPost]
        public IActionResult Insert([FromBody] T entity)
        {
            ServiceResult serviceResult = _baseService.Insert(entity);
            return Ok(serviceResult);
        }

        // PUT: api/v1/<BaseController>/5
        [HttpPut("{entityId}")]
        public IActionResult Update([FromBody] T entity, Guid entityId)
        {
            ServiceResult serviceResult = _baseService.Update(entity, entityId);
            return Ok(serviceResult);
        }

        // DELETE: api/v1/<BaseController>/5
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = _baseService.Delete(entityId);
            return Ok(serviceResult);
        }
        #endregion
    }
}
