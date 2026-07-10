using Elite_Webservices.Helpers.Custom;
using Elite_Webservices.Models;
using Elite_Webservices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 


namespace Elite_Webservices.Controllers
{
  // [System.Web.Http.Authorize]
    [RoutePrefix("api/login")]
    public class loginController : ApiController
    {

        //[Route("Login")]
        [HttpPost]
        [Route("login")]
        public Object login(RequestLogin req)
        {
            return Elite_Webservices.Models.LoginModel.Login(req);
        }


        [HttpPost]
        [Route("logincheckpasswordpolicy")]
        // [ActionName("logincheckpasswordpolicy")]
        public Object logincheckpasswordpolicy()
        {
            // Logging.WriteLog(LogType.Error, "passwordpolicyexception : " + "calling controller method");
            return Elite_Webservices.Models.LoginModel.logincheckpasswordpolicy();
        }
        //[HttpPost]
        //[Route("CheckADUserExistAuto")]
        //public Object CheckADUserExistAuto(RequestLogin req)
        //{
        //    return Infogaurd_Webservices.Models.LoginModel.CheckADUserExistAuto(req);
        //}
        //[HttpPost]
        //[Route("CheckADUserExist")]
        //public Object CheckADUserExist(RequestLogin req)
        //{
        //    return Infogaurd_Webservices.Models.LoginModel.CheckADUserExist(req);
        //}

        //[HttpPost]
        //[Route("CheckDomainExist")]
        //public Object CheckDomainExist(RequestDomain req)
        //{
        //    return Infogaurd_Webservices.Models.LoginModel.CheckDomainExist(req);
        //}
        //[HttpPost]
        //[Route("Autologin")]
        //public Object Autologin(RequestAutoLogin req)
        //{
        //    return Infogaurd_Webservices.Models.LoginModel.AutoLogin(req);
        //}
        [HttpPost]
        [Route("UpdatePassword")]
        public Object UpdatePassword(RequestUpdatePassword req)
        {
            return Elite_Webservices.Models.LoginModel.UpdatePassword(req);
        }

        //[HttpPost]
        //[Route("mlogin")]
        //public Object mlogin(RequestLogin req)
        //{
        //    return Infogaurd_Webservices.Models.LoginModel.LoginMobile(req);
        //}
     
    }
}
