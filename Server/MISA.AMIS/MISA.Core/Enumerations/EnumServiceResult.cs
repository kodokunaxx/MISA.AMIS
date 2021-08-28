using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Enumerations
{
    /// <summary>
    /// Mã trả về
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public enum EnumServiceResult
    {
        Success = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }
}
