using BusinessLayer.Interface;
using CommonLayer.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        IUserRL userRL;

        public void AddUser(UserModel user)
        {
            try
            {
                this.userRL.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                return this.userRL.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                return this.userRL.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
