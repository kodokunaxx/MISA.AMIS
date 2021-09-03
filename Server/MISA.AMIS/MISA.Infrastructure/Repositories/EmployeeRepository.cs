using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure.Repositories
{
    /// <summary>
    /// EmployeeRepository
    /// </summary>
    /// CreatedBy: hadm (28/8/2021)
    /// ModifiedBy: null
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Declare
        #endregion

        #region Property

        #endregion

        #region Constructor
        public EmployeeRepository()
        {
           
        }
        #endregion

        #region Method
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>Employees</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public IEnumerable<Employee> GetAll(int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetEmployeePaging";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@IsCount", null);

            return _dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy bản ghi theo mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Employee tương ứng</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public Employee GetByCode(string employeeCode)
        {
            string sqlCommand = "Proc_GetEmployeeByCode";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);

            return _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

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
        public IEnumerable<Employee> GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetEmployeeFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);
            dynamicParameters.Add("@FullName", fullName);
            dynamicParameters.Add("@PhoneNumber", phoneNumber);
            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@IsCount", null);

            return _dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Đếm tất cả nhân viên
        /// </summary>
        /// <returns>số nhân viên</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int GetCount()
        {
            string sqlCommand = "Proc_GetEmployeePaging";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PageIndex", null);
            dynamicParameters.Add("@PageSize", null);
            dynamicParameters.Add("@IsCount", 1);

            return _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Đếm số nhân viên với keyword
        /// </summary>
        /// <param name="employeeCode">Mã NV</param>
        /// <param name="fullName">Tên</param>
        /// <param name="phoneNumber">SĐT</param>
        /// <returns>số nhân viên</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int GetFilterCount(string employeeCode, string fullName, string phoneNumber)
        {
            string sqlCommand = "Proc_GetEmployeeFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);
            dynamicParameters.Add("@FullName", fullName);
            dynamicParameters.Add("@PhoneNumber", phoneNumber);
            dynamicParameters.Add("@PageIndex", null);
            dynamicParameters.Add("@PageSize", null);
            dynamicParameters.Add("@IsCount", 1);

            return _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        // <summary>
        /// Lấy mã NV mới nhất
        /// </summary>
        /// <returns>Mã NV mới nhất</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public string GetLastCode()
        {
            string sqlCommand = "Proc_GetLastestEmployeeCode";
            return _dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
        }
        #endregion

    }
}
