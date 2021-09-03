using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface Repository Base
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public interface IBaseRepository<T>
    {
        #region Method
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public IEnumerable<T> GetAll();
        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Entity</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public T GetById(Guid entityId);
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int Insert(T entity);
        /// <summary>
        /// Sửa một bản ghi theo Id
        /// </summary>
        /// <param name="entity">Id của entity</param>
        /// <param name="entityId">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int Update(T entity, Guid entityId);
        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int Delete(Guid entityId);

        /// <summary>
        /// Lấy ra 1 thực thể qua property name
        /// </summary>
        /// <param name="column">Column cần lấy</param>
        /// <param name="value">Giá trị cần lấy</param>
        /// <returns>entity</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public T GetByProperty(string column, string value, string id);
        #endregion
    }
}
