using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure.Repositories
{
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
        public IEnumerable<Employee> GetAll(int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetEmployeePaging";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@pageIndex", pageIndex);
            dynamicParameters.Add("@pageSize", pageSize);

            return _dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public Employee GetByCode(string employeeCode)
        {
            string sqlCommand = "Proc_GetEmployeeByCode";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@employeeCode", employeeCode);

            return _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Employee> GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetEmployeeFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@employeeCode", employeeCode);
            dynamicParameters.Add("@fullName", fullName);
            dynamicParameters.Add("@phoneNumber", phoneNumber);
            dynamicParameters.Add("@pageIndex", pageIndex);
            dynamicParameters.Add("@pageSize", pageSize);

            return _dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public string GetLastCode()
        {
            string sqlCommand = "Proc_GetLastestVendorCode";
            return _dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
        }
        #endregion

    }
}
