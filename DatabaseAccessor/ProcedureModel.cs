// FileName: ProcedureModel.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

#region

using System.Collections.Generic;

#endregion

namespace MStar.ManagerResearch.EntityModel.Common
{
    public class ProcedureModel
    {
        public string Name { get; set; }
        public string ProcName { get; set; }
        public List<DBParameter> Parameters { get; set; }
    }
}