using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.Core.Services
{
    /// <summary>
    /// Base Service
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public class BaseService<T> : IBaseService<T>
    {
        #region Declare
        IBaseRepository<T> _baseRepository;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Method
        public ServiceResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.DELETE;

            try
            {
                var rowEffects = _baseRepository.Delete(entityId);
                if (rowEffects > 0)
                {
                    serviceResult.SetSuccess(serviceResult, rowEffects);
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorDelete_Msg, entityId));
                }
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }
            return serviceResult;
        }

        public ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;

            try
            {
                var data = _baseRepository.GetAll();
                serviceResult.SetSuccess(serviceResult, data);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }
            return serviceResult;
        }

        public ServiceResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;

            try
            {
                var data = _baseRepository.GetById(entityId);
                if (data != null) {
                    serviceResult.SetSuccess(serviceResult, data);
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorGet_Msg, entityId));
                }
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }
            return serviceResult;
        }

        public ServiceResult Insert(T entity)
        {
            ServiceResult serviceResult = CheckValidate(entity);
            serviceResult.MoreInfo = Properties.Resource.POST;
            try
            {
                var rowEffects = _baseRepository.Insert(entity);
                if (rowEffects > 0)
                {
                    serviceResult.SetSuccess(serviceResult, rowEffects);
                    serviceResult.ResultCode = (int)Enumerations.EnumServiceResult.Created;
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(Properties.Resource.Dev_Info_Msg);
                }

            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }
            

            return serviceResult;
        }

        public ServiceResult Update(T entity, Guid entityId)
        {
            ServiceResult serviceResult = CheckValidate(entity);
            serviceResult.MoreInfo = Properties.Resource.PUT;
            T isExistEntity = _baseRepository.GetById(entityId);
            try
            {
                if (isExistEntity == null)
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorGet_Msg, entityId));
                }
                else
                {
                    serviceResult.SetSuccess(serviceResult, _baseRepository.Update(entity, entityId));
                }

            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(e.Message);
            }

            return serviceResult;
        }

        public ServiceResult CheckValidate(T entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var column = prop.Name;
                var value = (string)prop.GetValue(entity) ?? "";

                if (prop.IsDefined(typeof(Required), true) && CheckEmpty(value))
                {
                    serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Required_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Required_Msg, column));
                }
                if (prop.IsDefined(typeof(Email), true) && CheckEmail(value))
                {
                    serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Email_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Email_Msg, column));
                }
                if (prop.IsDefined(typeof(Duplication), true) && CheckDuplicate(column, value))
                {
                    serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, column));
                }
            }
            return serviceResult;
        }

        public bool CheckEmpty(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }

        public bool CheckDuplicate(string column, string value)
        {
            var isDuplicate = _baseRepository.GetByProperty(column, value);
            if (isDuplicate != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail(string value)
        {
            string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(value);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
