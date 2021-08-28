using Dapper;
using MISA.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
    {
        #region Declare
        protected string _tableName;
        protected string _connectString;
        protected IDbConnection _dbConnection;
        #endregion

        #region Property

        #endregion
        
        #region Constructor
        public BaseRepository()
        {
            _tableName = typeof(T).Name;
            _connectString = "" +
            "Host = 47.241.69.179;" +
            "Port = 3306;" +
            "Database =  backup_amis;" +
            "User Id = dev;" +
            "Password = 12345678";
            _dbConnection = new MySqlConnection(_connectString);
        }
        #endregion

        #region Method
        public virtual int Delete(Guid entityId)
        {
            string sqlCommand = $"Proc_Delete{_tableName}ById";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", entityId.ToString());
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowEffects;
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            var sqlCommand = $"Proc_Get{_tableName}s";

            return _dbConnection.Query<T>(sqlCommand, commandType: CommandType.StoredProcedure);
        }

        public T GetById(Guid entityId)
        {
            string sqlCommand = $"Proc_Get{_tableName}ById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", entityId.ToString());

            return _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public T GetByProperty(string column, string value)
        {
            string sqlCommand = $"Proc_Get{_tableName}By{column}";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{column}", value);

            return _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public virtual int Insert(T entity)
        {
            string sqlCommand = $"Proc_Insert{_tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var value = prop.GetValue(entity) == "" ? null : prop.GetValue(entity);
                dynamicParameters.Add($"@{prop.Name}", value);
            }

            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowAffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowAffects;
            }
        }

        public virtual int Update(T entity, Guid entityId)
        {
            string sqlCommand = $"Proc_Update{_tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                dynamicParameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }
            dynamicParameters.Add($"@{_tableName}Id", entityId.ToString());

            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowAffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowAffects;
            }
        }
        /// <summary>
        /// Đóng connection
        /// </summary>
        /// CreatedBy: hadm (28/8/2021)
        /// ModifiedBy: null
        public void Dispose()
        {
            if(_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
