// FileName: IDBAccess.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System.Data;

#endregion

namespace MStar.ManagerResearch.Common.DBCommon
{
    public interface IDBAccess
    {
        DataSet ExecuteDataSet(string procName, object parameters);
        DataTable ExecuteDataTable(string procName, object parameters);
        int ExecuteNonQuery(string procName, object parameters);
        object ExecuteScalar(string procName, object parameters);
    }
}