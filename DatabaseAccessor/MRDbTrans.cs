// FileName: MRDbTrans.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System;
using System.Data.Common;
using MStar.ManagerResearch.Common.DBCommon;

#endregion

namespace WebApi2.DatabaseAccessor
{
    public class MRDbTrans : IDisposable
    {
        private readonly DbConnection conn;
        private readonly DbTransaction dbTrans;

        public DbConnection DbConnection
        {
            get { return conn; }
        }

        public DbTransaction DbTrans
        {
            get { return dbTrans; }
        }

        //public MRDbTrans()
        //{
        //    conn = MRDbHelper.CreateConnection();
        //    conn.Open();
        //    dbTrans = conn.BeginTransaction();
        //}

        public MRDbTrans(string connectionString)
        {
            conn = MRDbHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }

        public void Commit()
        {
            DbTrans.Commit();
            Colse();
        }

        public void RollBack()
        {
            DbTrans.Rollback();
            Colse();
        }

        public void Dispose()
        {
            Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}