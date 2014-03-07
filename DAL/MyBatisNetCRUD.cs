using System.Collections.Generic;
using System.Diagnostics;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using WebApi2.EntityModel;

namespace WebApi2.DAL
{
    public class MyBatisNetCRUD
    {
        private static SqlMapper sqlMapper = null;

        static MyBatisNetCRUD()
        {
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            sqlMapper = builder.Configure() as SqlMapper;
        }

        public int Count()
        {
            int result = sqlMapper.QueryForObject<int>("SelectUserInfoCount", null);
            return result;
        }

        public bool Create(UserInfo info)
        {
            int id = (int) sqlMapper.Insert("InsertUserInfo", info);
            return id > 0;
        }

        public UserInfo Read(int userId)
        {
            UserInfo userInfo = sqlMapper.QueryForObject<UserInfo>("SelectUserById", userId);
            return userInfo;
        }

        public IList<UserInfo> GetUserList()
        {
            IList<UserInfo> userList = sqlMapper.QueryForList<UserInfo>("SelectAllUser", null);
            return userList;
        }

        public IList<UserInfo> GetUserList(int index, int size)
        {
            string connectionString = sqlMapper.DataSource.ConnectionString;
            Debug.WriteLine(connectionString);
            IList<UserInfo> userList = sqlMapper.QueryForList<UserInfo>("SelectAllUser", null, index, size);
            return userList;
        }

        public bool Update(UserInfo userInfo)
        {
            int result = sqlMapper.Update("UpdateUserInfo", userInfo);
            return result > 0;
        }

        public bool Delete(int userId)
        {
            int result = sqlMapper.Delete("DeleteUserInfo", userId);
            return result > 0;
        }

        public int GetMaxUserId()
        {
            int result = sqlMapper.QueryForObject<int>("SelectMaxUserId", null);
            return result;
        }
    }
}
