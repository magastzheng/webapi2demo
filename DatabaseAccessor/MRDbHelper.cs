// FileName: MRDbHelper.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using WebApi2.DatabaseAccessor;

#endregion

namespace MStar.ManagerResearch.Common.DBCommon
{
    public class MRDbHelper
    {
        private static readonly string DBProviderName = "System.Data.SqlClient";//ConfigurationManager.AppSettings["DbHelperProvider"];
        //private static readonly string DBConnectionString = ConfigurationManagement.GetDBServerByName("LogDB");
        private static readonly string SampleConnString = @"data source=szpc1483\SQLEXPRESS;User ID=MRLogOwner;PWD=MR123456;Initial Catalog=ManagerResearchLog";

        private readonly DbConnection connection;

        //public MRDbHelper()
        //{
        //    connection = CreateConnection(DBConnectionString);
        //}

        public MRDbHelper(string connectionString)
        {
            connection = CreateConnection(connectionString);
        }

        //public static DbConnection CreateConnection()
        //{
        //    DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
        //    DbConnection dbconn = dbfactory.CreateConnection();
        //    if (dbconn != null)
        //    {
        //        dbconn.ConnectionString = DBConnectionString;
        //    }
        //    return dbconn;
        //}

        public static DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
            DbConnection dbconn = dbfactory.CreateConnection();
            if (dbconn != null)
            {
                dbconn.ConnectionString = connectionString;
            }
            return dbconn;
        }

        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }

        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region add parameter

        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;

            dbParameter.Value = value ?? DBNull.Value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region excute

        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            DataSet ds = new DataSet();
            if (dbDataAdapter != null)
            {
                dbDataAdapter.SelectCommand = cmd;
                dbDataAdapter.Fill(ds);
            }
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            DataTable dataTable = new DataTable();
            if (dbDataAdapter != null)
            {
                dbDataAdapter.SelectCommand = cmd;
                dbDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }

        #endregion

        #region execute transaction

        public DataSet ExecuteDataSet(DbCommand cmd, MRDbTrans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            DataSet ds = new DataSet();
            if (dbDataAdapter != null)
            {
                dbDataAdapter.SelectCommand = cmd;
                dbDataAdapter.Fill(ds);
            }
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, MRDbTrans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            DataTable dataTable = new DataTable();
            if (dbDataAdapter != null)
            {
                dbDataAdapter.SelectCommand = cmd;
                dbDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, MRDbTrans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd, MRDbTrans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, MRDbTrans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }

        #endregion
    }
}