using System;
using System.Data;
using System.Diagnostics;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace WebApi2.SqlServerDAL
{
    public class SqlServerAccessor
    {
        private const string conString = "Data Source='szpc1483\\SQLEXPRESS';User ID='MRLogOwner';Password='MR123456';Initial Catalog='ManagerResearchLog';Integrated Security=True;Connect Timeout=120";
        public void Open()
        {
            IDbConnectionFactory dbFactory = null;
            try
            {
                dbFactory = new OrmLiteConnectionFactory(conString, SqlServerOrmLiteDialectProvider.Instance);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            IDbConnection dbConnection = dbFactory.OpenDbConnection();
            IDbCommand dbCommand = dbConnection.CreateCommand();

            //dbCommand.CreateTable<>
            //dbCommand.Delete()
            //dbCommand.
        }
    }
}
