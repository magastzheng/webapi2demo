using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using MStar.ManagerResearch.Common.DBCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAccessor.Test
{
    [TestClass]
    public class MRDbHelperTest
    {
        private const string ConnString = @"data source=szpc1483\SQLEXPRESS;User ID=MRLogOwner;PWD=MR123456;Initial Catalog=ManagerResearchLog";

        private void Print(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        object v = row[column];
                        if (v == DBNull.Value)
                            continue;
                        Debug.Write(string.Format("\t{0}",v));
                    }
                    Debug.Write("\n");
                }
            }
        }

        [TestMethod]
        public void ExecuteDataSetTest()
        {
            MRDbHelper dbHelper = new MRDbHelper(ConnString);
            try
            {
                DbCommand cmd = dbHelper.GetSqlStringCommond("select * from MRUsageLog");
                DataSet ds = dbHelper.ExecuteDataSet(cmd);
                Print(ds);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        [TestMethod]
        public void ExecuteSelectSPTest()
        {
            try
            {
                MRDbHelper db = new MRDbHelper(ConnString);
                DbCommand cmd = db.GetStoredProcCommond("dbo.GetUsageLog");
                db.AddInParameter(cmd, "@SessionId", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@StartDate", DbType.String,
                                    DateTime.Now.AddDays(33).ToString("yyyy-MM-dd HH:mm:ss"));
                db.AddInParameter(cmd, "@EndDate", DbType.String, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.AddInParameter(cmd, "@MachineName", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@LoginAccount", DbType.String, DBNull.Value);
                //DataTable dt = db.ExecuteDataTable(cmd);
                DataSet ds = db.ExecuteDataSet(cmd);
                Print(ds);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
