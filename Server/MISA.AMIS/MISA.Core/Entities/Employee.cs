using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Lớp Nhân viên
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public class Employee : BaseEntity
    {
        #region Declare

        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [Duplication]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính 0 - Nam, 1 - Nũ, 2 - Khác
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Chức danh
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Số căn cước
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string LandlineNumber { get; set; }
        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [Email]
        public string Email { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }
        /// <summary>
        /// Phòng ban, đơn vị
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã phòng ban, đơn vị
        /// </summary>
        //public Guid DepartmentId { get; set; }
        /// <summary>
        /// Là khách hàng 0 - sai, 1 - đúng
        /// </summary>
        public int? IsCustomer { get; set; }
        /// <summary>
        /// Là nhà cung cấp 0 - sai, 1 - đúng
        /// </summary>
        public int? IsSupplier { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region Method

        #endregion
    }
}
