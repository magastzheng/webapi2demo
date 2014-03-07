// FileName: DBParameter.cs
// Author: MZHENG
// Created Date: 2012-08-31 4:56 PM
// Modified Date: 2013-01-15 4:07 PM
// Description: 

namespace MStar.ManagerResearch.EntityModel.Common
{
    public class DBParameter
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Size { get; set; }
        public ParameterTypeEnum IOType { get; set; }
    }
}