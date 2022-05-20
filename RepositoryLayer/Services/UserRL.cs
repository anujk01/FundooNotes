using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using RepositoryLayer.DBContext;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.Users;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MSMQ.Messaging;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        public IConfiguration Configuration { get; set; }
        public UserRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundooContext = fundoo;
            this.Configuration = configuration;
        }

        public void AddUser(UserModel user)
        {
            try
            {
                User userdata = new User();
                userdata.firstName = user.firstName;
                userdata.lastName = user.lastName;
                userdata.email = user.email;
                userdata.password = user.password;
                fundooContext.Add(userdata);
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundooContext.User.FirstOrDefault(u => u.email == email && u.password == password);
                if (result == null)
                {
                    return null;
                }
                return GetJWTToken(email, result.userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetJWTToken(string email, int userId)
        {
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userid", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                var result = fundooContext.User.FirstOrDefault(u => u.email == email);
                if (result == null)
                {
                    return false;
                }
                MessageQueue FundooQ;
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    FundooQ = new MessageQueue(@".\Private$\FundooQueue");
                }
                else 
                {
                    FundooQ = MessageQueue.Create(@".\Private$\FundooQueue");
                }
                

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GetJWTToken(email, result.userid);
                message.Label = "Forget Password Email";
                FundooQ.Send(message);
                Message msg = FundooQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(email, msg.Body.ToString());
                FundooQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                FundooQ.BeginReceive();
                FundooQ.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message message = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ChangePassword(string email, ChangePasswordModel changePassword)
        {
            try
            {
                if(changePassword.password.Equals(changePassword.confirmPassword))
                {
                    var user = fundooContext.User.Where(x => x.email == email).FirstOrDefault();
                    user.password = StringCipher.EncodePasswordToBase64(changePassword.password);
                    fundooContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}
