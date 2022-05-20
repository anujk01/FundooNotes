using BusinessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.DBContext;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooContext fundooContext;
        IUserBL userBL;

        public UserController(FundooContext fundoo, IUserBL userBL)
        {
            this.fundooContext = fundoo;
            this.userBL = userBL;

    }

        [HttpPost("Register")]
        public IActionResult AddUser(UserModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"User Added Successfully" });
            }
            catch (SystemException)
            {
                throw;
            }
        }

        [HttpPost("login/{email}/{password}")]
        public IActionResult LoginUser(string email, string password)
        {
            try
            {
                var userdata = fundooContext.User.FirstOrDefault(u => u.email == email);

                if (userdata == null)
                {
                    return this.BadRequest(new { success = false, message = $"Email and Password Is Invalid" });
                }
                string result = this.userBL.LoginUser(email, password);

                return this.Ok(new { success = true, message = $"Login Successfull", Token = result });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgetPassword/{email}")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool result = this.userBL.ForgetPassword(email);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = $"Mail Sent Successfully : {result}" });
                }
                return this.BadRequest(new { success = false, message = $"Failed to Sent Mail : {result}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]

        public IActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                var result = fundooContext.User.Where(u => u.userid == userID).FirstOrDefault();
                string email = result.email.ToString();
                //var currentUser = HttpContext.User;
                //int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "userid").Value);
                //var email = (currentUser.Claims.FirstOrDefault(c => c.Type == "email").Value);

                bool res = userBL.ChangePassword(email, changePassword);
                if( res == false)
                {
                    return this.BadRequest(new { success = false, message = "Enter Valid Password" });
                }
                return this.Ok(new { success = true, message = "Password Changed Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
