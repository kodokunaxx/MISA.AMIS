using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Property

        #endregion
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Method
        public ServiceResult GetAll(int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;

            try
            {
                var data = _employeeRepository.GetAll(pageIndex, pageSize);
                serviceResult.SetSuccess(serviceResult, data);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }

            return serviceResult;
        }

        public ServiceResult GetByCode(string employeeCode)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize)
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;

            try
            {
                var data = _employeeRepository.GetFilter(employeeCode, fullName, phoneNumber,pageIndex, pageSize);
                serviceResult.SetSuccess(serviceResult, data);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }

            return serviceResult;
        }

        public ServiceResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var lastestCode = _employeeRepository.GetLastCode();
                if(lastestCode == null)
                {
                    serviceResult.SetSuccess(serviceResult, "NV-00001");
                    return serviceResult;
                }
                string number = lastestCode.Substring(3);
                int firstIndex = 0;

                while (firstIndex < number.Length && number[firstIndex] == '0')
                {
                    firstIndex++;
                }

                int currentNumberLength = number.Length - firstIndex;
                if (((Int32.Parse(number) + 1) + "").Length > currentNumberLength && firstIndex > 0)
                {
                    firstIndex--;
                }

                serviceResult.Data = lastestCode.Substring(0, 3 + firstIndex) + (Int32.Parse(number) + 1);
            }
            catch (Exception e)
            {
                serviceResult.ResultCode = (int)EnumServiceResult.NotFound;
                serviceResult.DevMessage.Add(e.Message);
            }
            throw new NotImplementedException();
        }
        #endregion
    }
}
