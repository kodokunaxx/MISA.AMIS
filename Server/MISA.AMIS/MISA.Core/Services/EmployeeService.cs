using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq;
using System.Text;

namespace MISA.Core.Services
{
    /// <summary>
    /// Employee Service
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
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
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>Employees</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
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
                serviceResult.Total = _employeeRepository.GetCount();
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// Lấy bản ghi theo mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Employee tương ứng</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetByCode(string employeeCode)
        {
            throw new NotImplementedException();
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
        public ServiceResult GetFilter(string employeeCode, string fullName, string phoneNumber, int pageIndex, int pageSize)
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;

            try
            {
                var data = _employeeRepository.GetFilter(employeeCode, fullName, phoneNumber, pageIndex, pageSize);
                serviceResult.SetSuccess(serviceResult, data);
                serviceResult.Total = _employeeRepository.GetFilterCount(employeeCode, fullName, phoneNumber);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// Lấy mã NV mới
        /// </summary>
        /// <returns>Mã NV mới</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            var codeIfError = "";
            try
            {
                var lastestCode = _employeeRepository.GetLastCode();
                codeIfError = lastestCode;
                if (lastestCode == null)
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
            catch( FormatException e)
            {
                codeIfError += "0";
                serviceResult.Data = codeIfError;
                serviceResult.DevMessage.Add(e.Message);
            }
            catch (Exception e)
            {
                serviceResult.ResultCode = (int)EnumServiceResult.NotFound;
                serviceResult.DevMessage.Add(e.Message);
            }
            return serviceResult;
        }

        /// <summary>
        /// Export file Excel
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>file</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public Stream ExportExcel(string keyword, int pageIndex, int pageSize)
        {
            var res = _employeeRepository.GetAll(); ;
            if (keyword != null)
            {
                res = _employeeRepository.GetFilter(keyword, keyword, keyword, pageIndex, pageSize);
            }

            var list = res.ToList();
            var stream = new MemoryStream();
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

            // set độ rộng col
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(3).Width = 30;
            workSheet.Column(4).Width = 10;
            workSheet.Column(5).Width = 15;
            workSheet.Column(6).Width = 30;
            workSheet.Column(7).Width = 30;
            workSheet.Column(8).Width = 15;
            workSheet.Column(9).Width = 30;


            using (var range = workSheet.Cells["A1:I1"])
            {
                range.Merge = true;
                range.Value = "DANH SÁCH NHÂN VIÊN";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 2].Value = "Mã nhân viên";
            workSheet.Cells[3, 3].Value = "Tên nhân viên";
            workSheet.Cells[3, 4].Value = "Giới tính";
            workSheet.Cells[3, 5].Value = "Ngày sinh";
            workSheet.Cells[3, 6].Value = "Chức danh";
            workSheet.Cells[3, 7].Value = "Tên đơn vị";
            workSheet.Cells[3, 8].Value = "Số tài khoản";
            workSheet.Cells[3, 9].Value = "Tên ngân hàng";


            using (var range = workSheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var e in list)
            {
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = e.EmployeeCode;
                workSheet.Cells[i + 4, 3].Value = e.FullName.ToUpper();
                workSheet.Cells[i + 4, 4].Value = e.Gender == 1 ? "Nam" : "Nữ";
                workSheet.Cells[i + 4, 5].Value = e.DateOfBirth?.ToString("dd/MM/yyyy");
                workSheet.Cells[i + 4, 6].Value = e.SecondName;
                workSheet.Cells[i + 4, 7].Value = e.DepartmentName;
                workSheet.Cells[i + 4, 8].Value = e.BankAccount;
                workSheet.Cells[i + 4, 9].Value = e.BankName;

                using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                i++;
            }

            package.Save();
            stream.Position = 0;
            return package.Stream;
        }

        #endregion
    }
}
