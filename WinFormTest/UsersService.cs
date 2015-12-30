﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WinFormTest
{
    public class UsersService
    {
      
        /// <summary>
        /// 验证用户登录，登录成功则返回该用户对象，失败返回null
        /// </summary>
        /// <param name="user">要验证的用户对象</param>
        /// <returns>返回用户对象，登录失败返回null</returns>
        public Users CheckLogin(Users user)
        {
            Users returnUser = null;
            string sql = "select * from users where loginName=@loginName and password=@password";
            SqlParameter[] values = new SqlParameter[]{
                new SqlParameter("@loginName",user.LoginName),
                new SqlParameter("@password",user.Password)
            };
            DataTable dt = DBHelper. GetDataTable(sql, values);
            if (dt.Rows.Count > 0)
            {
                DataRow dr=dt.Rows[0];
                returnUser = new Users();
                returnUser.LoginName = user.LoginName;
                returnUser.Password = user.Password;
                returnUser.Id = Convert.ToInt32(dr["id"]);
                returnUser.RealName = dr["realName"].ToString();
            }
            return returnUser;
        }
    }
}
