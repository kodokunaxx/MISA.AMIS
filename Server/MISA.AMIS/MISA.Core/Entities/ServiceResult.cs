using MISA.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Lớp kết quả trả về
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public class ServiceResult
    {
        #region Declare

        #endregion

        #region Property
        /// <summary>
        /// Mã kết quả 
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Message trả về cho dev
        /// </summary>
        public List<string> DevMessage { get; set; }

        /// <summary>
        /// Message trả về cho user
        /// </summary>
        public List<string> UserMessage { get; set; }

        /// <summary>
        /// Thông tin khác
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Data trả về cho Client
        /// </summary>
        public object Data { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public ServiceResult()
        {
            ResultCode = (int)EnumServiceResult.Success;
            DevMessage = new List<string>();
            UserMessage = new List<string>();
            MoreInfo = null;
            Data = null;
        }
        #endregion

        #region Method
        /// <summary>
        /// Gán log thực hiện thành công
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <param name="data"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetSuccess(ServiceResult serviceResult, object data)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.Success;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Success_Msg);
            serviceResult.UserMessage.Add(Properties.Resource.User_Success_Msg);
            serviceResult.Data = data;
        }

        /// <summary>
        /// Gán log thực hiện thất bại
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetNoContent(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.NoContent;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Error_Msg);
            serviceResult.UserMessage.Add(Properties.Resource.User_Info_Msg);
        }

        /// <summary>
        /// Gán log lỗi máy chủ
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetInternalServerError(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.InternalServerError;
            serviceResult.UserMessage.Add(Properties.Resource.User_Error_Msg);
        }

        /// <summary>
        /// Gán log lỗi thông tin
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetBadRequest(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
            serviceResult.UserMessage.Add(Properties.Resource.User_Info_Msg);
        }
        #endregion
    }
}
