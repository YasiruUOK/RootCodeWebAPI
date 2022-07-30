using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RootCodeWebAPI.Models;
using RootCodeWebAPI.VM;

namespace RootCodeWebAPI.Controllers
{
    [RoutePrefix("Api/login")]
    public class LoginController : ApiController
    {
        RootCodeTestDBEntities DB = new RootCodeTestDBEntities();
        [Route("InsertUser")]
        [HttpPost]
        public object InsertEmployee(Register Reg)
        {
            try
            {

                UserInfo EL = new UserInfo();
                if (EL.UserId == 0)
                {
                    EL.FirstName = Reg.FirstName;
                    EL.LastName = Reg.LastName;
                    EL.EmailAddress = Reg.EmailAddress;
                    EL.PasswordHash = Reg.PasswordHash;
                    EL.UserRole = Reg.UserRole;
                    EL.IsEditor = false;
                    EL.IsBanned = false;
                    DB.UserInfoes.Add(EL);
                    DB.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "Record SuccessFully Saved." };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }
        [Route("Login")]
        [HttpPost]
        public Response employeeLogin(Login login)
        {
            var log = DB.UserInfoes.Where(x => x.EmailAddress.Equals(login.EmailAddress) && x.PasswordHash.Equals(login.PasswordHash)).FirstOrDefault();

            if (log == null)
            {
                return new Response { Status = "Invalid", Message = "Invalid User." };
            }
            else
                return new Response { Status = "Success", Message = "Login Successfully", Data = log.UserId.ToString()+"|||"+log.UserRole };
        }
    }
}