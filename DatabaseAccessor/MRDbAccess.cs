// FileName: MRDbAccess.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using MStar.ManagerResearch.EntityModel.Common;
using WebApi2.DatabaseAccessor;

#endregion

namespace MStar.ManagerResearch.Common.DBCommon
{
    public class MRDbAccess : IDBAccess
    {
        private readonly MRDbHelper db;

        public MRDbAccess(string constring)
        {
            db = new MRDbHelper(constring);
        }
 
        #region IDBAccess Members

        public DataSet ExecuteDataSet(string procName, object parameters)
        {
            DbCommand cmd = GetDBCommand(procName, parameters);
            return db.ExecuteDataSet(cmd);
        }

        public DataTable ExecuteDataTable(string procName, object parameters)
        {
            DbCommand cmd = GetDBCommand(procName, parameters);
            return db.ExecuteDataTable(cmd);
        }

        public int ExecuteNonQuery(string procName, object parameters)
        {
            DbCommand cmd = GetDBCommand(procName, parameters);
            return db.ExecuteNonQuery(cmd);
        }

        public object ExecuteScalar(string procName, object parameters)
        {
            DbCommand cmd = GetDBCommand(procName, parameters);
            return db.ExecuteScalar(cmd);
        }

        #endregion

        private DbCommand GetDBCommand(string procName, object parameter)
        {
            DBAccessUtil readProc = new DBAccessUtil();

            List<ProcedureModel> procedureCollection = readProc.ReadDBAccessProcedures();
            ProcedureModel p =
                procedureCollection.Find(pm => pm.Name.Equals(procName, StringComparison.InvariantCultureIgnoreCase));
            if (p != null)
            {
                DbCommand cmd = db.GetStoredProcCommond(p.ProcName);
                if (p.Parameters != null && p.Parameters.Count > 0 && parameter != null)
                {
                    int index = 0;
                    foreach (DBParameter param in p.Parameters)
                    {
                        object pValue = null;
                        if (parameter.GetType() == typeof (object[]))
                        {
                            pValue = ((object[]) parameter).GetValue(index);
                        }
                        else
                        {
                            PropertyInfo[] pis = parameter.GetType().GetProperties();
                            foreach (PropertyInfo property in pis)
                            {
                                if (string.Equals(property.Name, param.Name, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    object obj = parameter.GetType()
                                                          .GetProperty(property.Name)
                                                          .GetValue(parameter, null);
                                    pValue = obj == null ? null : obj.ToString();
                                    break;
                                }
                            }
                        }

                        DbType type = Parse2DbType(param.DataType);
                        if (type == DbType.DateTime)
                        {
                            if (pValue != null)
                            {
                                DateTime dateTime = Convert.ToDateTime(pValue.ToString());
                                pValue = dateTime.ToString("yyyy-MM-dd");
                                //pValue = dateTime.ToString(ConstVariable.ShortDateTimeFormat);
                            }
                        }

                        if (pValue == null)
                        {
                            pValue = DBNull.Value;
                        }

                        if (param.IOType == ParameterTypeEnum.Input)
                        {
                            db.AddInParameter(cmd, "@" + param.Name, type, pValue);
                            index++;
                        }
                        else
                        {
                            db.AddOutParameter(cmd, "@" + param.Name, type, param.Size);
                        }
                    }
                }
                return cmd;
            }
            return null;
        }


        private DbType Parse2DbType(string dataType)
        {
            switch (dataType)
            {
                case "string":
                case "varchar":
                case "nvarchar":
                    return DbType.String;
                case "int":
                    return DbType.Int32;
                case "datetime":
                    return DbType.DateTime;
                default:
                    return DbType.String;
            }
        }
    }
}