using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi2.DAL;
using WebApi2.EntityModel;

namespace DAL.Test
{
    [TestClass]
    public class MyBatisNetCRUDTest
    {
        private MyBatisNetCRUD dao = null;
        //[ClassInitialize]
        //public void Initialize()
        //{
        //    dao = new MyBatisNetCRUD();
        //}

        [TestMethod]
        public void CreateTest()
        {
            try
            {
                dao = new MyBatisNetCRUD();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            UserInfo userInfo = new UserInfo
                {
                    UserName = "magast",
                    RealName = "Magast Zheng",
                    Age = 30,
                    Sex = 1,
                    Email = "magast.zheng@morningstar.com",
                    Mobile = "13632721546",
                    Phone = "075533111202"
                };
            bool result = dao.Create(userInfo);
            Assert.IsTrue(result);
        }
    }
}
