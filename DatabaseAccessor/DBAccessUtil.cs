// FileName: DBAccessUtil.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using MStar.ManagerResearch.EntityModel.Common;

#endregion

namespace WebApi2.DatabaseAccessor
{
    public class DBAccessUtil
    {
        /// <summary>
        ///  Read Store Procedures from Config File
        /// </summary>
        /// <returns></returns>
        public List<ProcedureModel> ReadDBAccessProcedures()
        {
            string configName = "DBAccessProdecuresXML";
            List<ProcedureModel> procedures = new List<ProcedureModel>();
            string filePath = ConfigurationManager.AppSettings[configName];
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("procedures");
            if (root != null)
            {
                if (root.ChildNodes.Count > 0)
                {
                    foreach (XmlNode procNode in root.ChildNodes)
                    {
                        ProcedureModel pm = new ProcedureModel();
                        pm.Name = procNode.Attributes["name"].Value;
                        pm.ProcName = procNode.Attributes["procName"].Value;
                        if (procNode.HasChildNodes)
                        {
                            pm.Parameters = new List<DBParameter>();
                            XmlNode paramCollectionNode = procNode.FirstChild;
                            foreach (XmlNode paramNode in paramCollectionNode.ChildNodes)
                            {
                                DBParameter p = new DBParameter();
                                foreach (XmlAttribute attr in paramNode.Attributes)
                                {
                                    if (attr.Name.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        p.Name = attr.Value;
                                    }
                                    if (attr.Name.Equals("dataType", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        p.DataType = paramNode.Attributes["dataType"].Value;
                                    }
                                    if (attr.Name.Equals("size", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        p.Size = Convert.ToInt32(attr.Value);
                                    }
                                    if (attr.Name.Equals("ioType", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (attr.Value.Equals("out", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            p.IOType = ParameterTypeEnum.OutPut;
                                        }
                                    }
                                }
                                pm.Parameters.Add(p);
                            }
                        }
                        procedures.Add(pm);
                    }
                }
            }
            return procedures;
        }
    }
}