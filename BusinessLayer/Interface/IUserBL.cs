using CommonLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public void AddUser(UserModel user);
        public string LoginUser(string email, string password);
        public bool ForgetPassword(string email);
        public bool ChangePassword(string email, ChangePasswordModel changepassword);
    }
}
