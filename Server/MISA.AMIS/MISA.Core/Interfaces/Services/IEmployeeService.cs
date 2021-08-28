using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Interface Employee Service
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public interface IEmployeeService : IBaseService<Employee>
    {
        #region Method
        /// <summary>
        /// Lấy bản ghi theo mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Employee tương ứng</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetByCode(string employeeCode);
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>Employees</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetAll(int pageIndex, int pageSize);
        /// <summary>
        /// Lấy bản ghi qua keyword
        /// </summary>
        /// <param name="employeeCode">Mã NV</param>
        /// <param name="fullName">Tên</param>
        /// <param name="phoneNumber">SĐT</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>Employees</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize);
        /// <summary>
        /// Lấy mã NV mới
        /// </summary>
        /// <returns>Mã NV mới</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetNewCode();
        #endregion
    }
}
