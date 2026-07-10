using Elite_Webservices.Helpers.Custom;
using Elite_Webservices.Helpers.General;
using Elite_Webservices.Utility;
//using RRCDR_AttendanceDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
//using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
//using System.DirectoryServices.AccountManagement;

namespace Elite_Webservices.Models
{
    public class LoginModel
    {
        public static Object Login(RequestLogin Request)
        {
            EliteCMSEntities db = new EliteCMSEntities();
            ResponseLogin Response = new ResponseLogin();

            Request.AppName = "raideTime";

            try
            {
                if (Request == null)
                {
                    Response.IsSuccess = false;
                    Response.Message = Constant.RequestIsEmpty.ToString();
                    return Response;
                }


                if (Request != null)
                {
                    #region User 
                    //====================> match user <====================//
                    string _password = Utility.EncryptionHelper.EncrptPassword(Request.Password);
                    var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.Password.Equals(_password) && x.IsActive == true).FirstOrDefault();
                    if (log == null)
                    {
                        Response.IsSuccess = false;
                        Response.Message = Constant.InvalidUser.ToString();
                        Response.Status = Constant.InvalidUser.ToString();
                        return Response;
                    }
                    else
                    {
                        var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
                        bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
                        var roleid = 0;
                        var roleids= "";
                       // var appname = Request.AppName;
                        var Rolename = "";
                        int roleindex = 0;
                        string roleindexs = "";

                        var xRes = log.AppIDs.Split('-');
                        if (xRes.Length > 0)
                        {
                            for (int i = 0; i < xRes.Length; i++)
                            {
                                if (xRes[i].Trim() == Request.AppName)
                                {
                                    roleindexs += i;
                                    if (i != xRes.Length - 1)
                                    {
                                        roleindexs += ",";
                                    }
                                }

                            }
                            roleindexs = roleindexs.TrimEnd(',');
                        }
                        if (roleindexs == null || roleindexs.Trim() == "")
                        {
                            Response.Message = "UserName or Password Incorrect";
                            return Response;
                        }
                        if (log.AppWiseRoles != null)
                        {
                            var AppRolesid = log.AppWiseRoles.Split('-');
                            if (AppRolesid.Length > 0)
                            {
                                var roleindexesm = roleindexs.Split(',');
                                for (var k = 0; k < roleindexesm.Length; k++)
                                {
                                    //roleid += Convert.ToInt32(roleindexesm[k]);
                                    var RolenamM = log.RoleNames.Split('-');
                                    var RoleidM = log.AppWiseRoles.Split('-');

                                    Rolename += RolenamM[Convert.ToInt32(roleindexesm[k])] + ",";
                                    roleids += RoleidM[Convert.ToInt32(roleindexesm[k])] + ",";

                                }
                            }

                        }
                        Response.AppName = Request.AppName;
                        Response.AppRoleId = roleids.TrimEnd(',');
                        Response.AppRoleName = Rolename.TrimEnd(',');

                        int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
                        //var Supervisor = db.TblEmployees.Where(x => x.HOD == EmpNo).ToList();
                        //if (Supervisor.Count > 0)
                        //{
                        //    Response.IsSupervisor = "yes";
                        //}
                        //else
                        //{
                        //    Response.IsSupervisor = "no";
                        //}

                        Response.Status = Constant.Success;
                        Response.IsSuccess = true;
                        Response.UserId = log.ID.ToString();
                        Response.Empid = log.EmpId.ToString();
                        Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
                        Response.UserName = log.UserName;
                        Response.Language = log.Language.ToString();
                        //Response.wfh = _WFH;
                        //Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
                        //var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
                        //Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
                        Response.Role = log.Role;
                        //Response.checkin = spcheckin;
                        Response.UpdatedDate = log.UpdatedDate;
                    
                        var hodId = empnumber.HOD;
                        //if (hodId > 0 || hodId != null)
                        //{
                        //    string _hodID = Convert.ToString(hodId);
                        //    var ManagerName = db.TblEmployees.Where(x => x.ID.ToString() == _hodID).FirstOrDefault();
                        //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
                        //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
                        //}
                        //else
                        //{
                        //    Response.ManagerName = "";
                        //    Response.ManagerNameAR = "";
                        //}
                        Response.DepartmentId = empnumber.DepartmentId;
                        Response.ProfilePic = empnumber.ProfilePic;
                        Response.DepartmentList = log.DepartmentList;
                        Response.Message = Constant.LoginSuccessFull;
                        return Response;
                        // return new Response { Status = "Success", Message = "Login Successfully", id = empnumber.EMPLOYEE_NUMBER, wfh = _WFH, checkin = spcheckin };
                    }

                    #endregion
                }
                else
                {
                    // Response.Code = (int)Common.Response.Error;
                    Response.IsSuccess = false;
                    Response.Message = Constant.RequestNull.ToString();

                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.Message = Constant.GeneralErrorMsg.ToString();
                Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
            }

            return Response;
        }

        public static Object logincheckpasswordpolicy()
        {
            EliteCMSEntities db = new EliteCMSEntities();
            ResponseLogincheckpasswordpolicy Response = new ResponseLogincheckpasswordpolicy();

            try
            {

                // var result = db.PasswordPolicies.FirstOrDefault();
                var result = db.PasswordPolicies.ToList();
                if (result == null)
                {
                    Response.IsSuccess = false;
                    Response.Message = Constant.RequestNull.ToString();
                    Response.Status = Constant.RequestNull.ToString();

                    // Logging.WriteLog(LogType.Error, "passwordpolicyexception : " + "when result is null" + result +Response.Message);
                    return Response;
                }
                else
                {

                    Response.Status = Constant.Success;
                    Response.IsSuccess = true;
                    Response.Message = Constant.GetSuccessfully;
                    //Response.IsPasswordExpired = result.IsPasswordExpired;
                    //Response.PasswordExpiredDuration = result.PasswordExpiredDuration;
                    //Response.MaxCharacter = result.MaxCharacter;

                    Response.passwordPolicy = result.FirstOrDefault();

                    // Logging.WriteLog(LogType.Error, "passwordpolicyexception : " + "when result is not null" + result + Response.Message);
                    return Response;
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.Message = Constant.GeneralErrorMsg.ToString();
                Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
            }

            return Response;
        }
        //public static Object CheckADUserExistAuto(RequestLogin Request)
        //{
        //    InfoGaurdCMSEntities db = new InfoGaurdCMSEntities();
        //    ResponseADUserLogin Response = new ResponseADUserLogin();

        //    try
        //    {
        //        if (Request == null)
        //        {
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestIsEmpty.ToString();
        //            return Response;
        //        }


        //        if (Request != null)
        //        {
        //            #region User 
        //            //====================> match user <====================//
        //            //var _password = Utility.EncryptionHelper.DecryptPass(Request.Password);
        //            var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.UserType.Equals("active") && x.IsActive == true).FirstOrDefault();
        //            var domid = log.Domain;
        //            //string _password = EncryptionHelper.EncryptPass(Request.Password);
        //            var domain = db.ADConfigurations.Where(x => x.ID.ToString() == domid.ToString() /*&& x.AppPassword == _password*/).ToList();
        //            if (log == null || domain.Count() == 0)
        //            {
        //                Response.IsSuccess = false;
        //                Response.Message = Constant.InvalidUser.ToString();
        //                Response.Status = Constant.InvalidUser.ToString();
        //                return Response;
        //            }
        //            else
        //            {
        //                int domainid = Convert.ToInt32(log.Domain);
        //                var adconfig = db.ADConfigurations.Where(x => x.ID == domainid).FirstOrDefault();

        //                var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
        //                bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
        //                //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                //if (getcheckin.Count > 0)
        //                //{
        //                //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                //}
        //                int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                int Empid = Convert.ToInt32(empnumber.ID);
        //                var Supervisor = db.TblEmployees.Where(x => x.HOD == Empid).ToList();
        //                if (Supervisor.Count > 0)
        //                {
        //                    Response.IsSupervisor = "yes";
        //                }
        //                else
        //                {
        //                    Response.IsSupervisor = "no";
        //                }

        //                var roleid = 0;
        //                var roleids = "";
        //                var appname = Request.AppName;
        //                var Rolename = "";
        //                int roleindex = 0;
        //                string roleindexs = "";

        //                var xRes = log.AppIDs.Split('-');
        //                if (xRes.Length > 0)
        //                {
        //                    for (int i = 0; i < xRes.Length; i++)
        //                    {
        //                        if (xRes[i].Trim() == Request.AppName)
        //                        {
        //                            roleindexs += i;
        //                            if (i != xRes.Length - 1)
        //                            {
        //                                roleindexs += ",";
        //                            }
        //                        }

        //                    }
        //                    roleindexs = roleindexs.TrimEnd(',');
        //                }
        //                if (roleindexs == null || roleindexs.Trim() == "")
        //                {
        //                    Response.Message = "UserName or Password Incorrect";
        //                    return Response;
        //                }
        //                if (log.AppWiseRoles != null)
        //                {
        //                    var AppRolesid = log.AppWiseRoles.Split('-');
        //                    if (AppRolesid.Length > 0)
        //                    {
        //                        var roleindexesm = roleindexs.Split(',');
        //                        for (var k = 0; k < roleindexesm.Length; k++)
        //                        {
        //                            //roleid += Convert.ToInt32(roleindexesm[k]);
        //                            var RolenamM = log.RoleNames.Split('-');
        //                            var RoleidM = log.AppWiseRoles.Split('-');

        //                            Rolename += RolenamM[Convert.ToInt32(roleindexesm[k])] + ",";
        //                            roleids += RoleidM[Convert.ToInt32(roleindexesm[k])] + ",";

        //                        }
        //                    }

        //                }
        //                Response.AppName = Request.AppName;
        //                Response.AppRoleId = roleids.TrimEnd(',');
        //                Response.AppRoleName = Rolename.TrimEnd(',');
        //                //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                //if (getcheckin.Count > 0)
        //                //{
        //                //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                //}
        //                int EmpNo1 = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                var Supervisor1 = db.TblEmployees.Where(x => x.HOD == EmpNo1).ToList();
        //                if (Supervisor1.Count > 0)
        //                {
        //                    Response.IsSupervisor = "yes";
        //                }
        //                else
        //                {
        //                    Response.IsSupervisor = "no";
        //                }

        //                Response.Status = Constant.Success;
        //                Response.IsSuccess = true;
        //                Response.UserId = log.ID.ToString();
        //                Response.DomainId = log.Domain;
        //                Response.DomainUser = log.DomainUser;
        //                Response.Message = Constant.LoginSuccessFull;
        //                Response.DomainIP = adconfig.DomainIP;
        //                Response.DomainName = adconfig.DomainName;
        //                Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
        //                Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
        //                var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
        //                Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
        //                Response.Language = log.Language.ToString();
        //                Response.wfh = _WFH;
        //                Response.Role = log.Role;
        //                Response.EmpId = log.EmpId;
        //                Response.DepartmentList = log.DepartmentList;

        //                //Response.checkin = spcheckin;
        //                // Response.UpdatedDate = log.UpdatedDate;

        //                //var hodId = empnumber.HOD;
        //                //if (hodId > 0 || hodId != null)
        //                //{
        //                //    string _hodID = Convert.ToString(hodId);
        //                //    var ManagerName = db.TblEmployees.Where(x => x.ID.ToString() == _hodID).FirstOrDefault();
        //                //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
        //                //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
        //                //}
        //                //else
        //                //{
        //                Response.ManagerName = "";
        //                Response.ManagerNameAR = "";
        //                // }
        //                Response.ProfilePic = empnumber.ProfilePic;
        //                return Response;
        //                // return new Response { Status = "Success", Message = "Login Successfully", id = empnumber.EMPLOYEE_NUMBER, wfh = _WFH, checkin = spcheckin };
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            // Response.Code = (int)Common.Response.Error;
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestNull.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.IsSuccess = false;
        //        Response.Message = Constant.GeneralErrorMsg.ToString();
        //        Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
        //    }

        //    return Response;
        //}
        //public static Object CheckADUserExist(RequestLogin Request)
        //{
        //    InfoGaurdCMSEntities db = new InfoGaurdCMSEntities();
        //    ResponseADUserLogin Response = new ResponseADUserLogin();

        //    try
        //    {
        //        if (Request == null)
        //        {
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestIsEmpty.ToString();
        //            return Response;
        //        }


        //        if (Request != null)
        //        {
        //            #region User 
        //            //====================> match user <====================//
        //            //  var  _password = Utility.EncryptionHelper.EncryptPass(Request.Password);
        //            var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.UserType.Equals("active") && x.IsActive == true).FirstOrDefault();
        //            var domid = log.Domain;
        //            //string _password = EncryptionHelper.EncryptPass(Request.Password);
        //            var domain = db.ADConfigurations.Where(x => x.ID.ToString() == domid.ToString() ).ToList();
        //            if (log == null || domain.Count() == 0)
        //            {
        //                Response.IsSuccess = false;
        //                Response.Message = Constant.InvalidUser.ToString();
        //                Response.Status = Constant.InvalidUser.ToString();
        //                return Response;
        //            }
        //            else
        //            {
        //                int domainid = Convert.ToInt32(log.Domain);
        //                var adconfig = db.ADConfigurations.Where(x => x.ID == domainid).FirstOrDefault();

        //                var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
        //                bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
        //                //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                //if (getcheckin.Count > 0)
        //                //{
        //                //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                //}
        //                int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                int Empid = Convert.ToInt32(empnumber.ID);
        //                var Supervisor = db.TblEmployees.Where(x => x.HOD == Empid).ToList();
        //                if (Supervisor.Count > 0)
        //                {
        //                    Response.IsSupervisor = "yes";
        //                }
        //                else
        //                {
        //                    Response.IsSupervisor = "no";
        //                }

        //                var roleid = 0;
        //                var roleids = "";
        //                var appname = Request.AppName;
        //                var Rolename = "";
        //                int roleindex = 0;
        //                string roleindexs = "";

        //                var xRes = log.AppIDs.Split('-');
        //                if (xRes.Length > 0)
        //                {
        //                    for (int i = 0; i < xRes.Length; i++)
        //                    {
        //                        if (xRes[i].Trim() == Request.AppName)
        //                        {
        //                            roleindexs += i;
        //                            if (i != xRes.Length - 1)
        //                            {
        //                                roleindexs += ",";
        //                            }
        //                        }

        //                    }
        //                    roleindexs = roleindexs.TrimEnd(',');
        //                }
        //                if (roleindexs == null || roleindexs.Trim() == "")
        //                {
        //                    Response.Message = "UserName or Password Incorrect";
        //                    return Response;
        //                }
        //                if (log.AppWiseRoles != null)
        //                {
        //                    var AppRolesid = log.AppWiseRoles.Split('-');
        //                    if (AppRolesid.Length > 0)
        //                    {
        //                        var roleindexesm = roleindexs.Split(',');
        //                        for (var k = 0; k < roleindexesm.Length; k++)
        //                        {
        //                            //roleid += Convert.ToInt32(roleindexesm[k]);
        //                            var RolenamM = log.RoleNames.Split('-');
        //                            var RoleidM = log.AppWiseRoles.Split('-');

        //                            Rolename += RolenamM[Convert.ToInt32(roleindexesm[k])] + ",";
        //                            roleids += RoleidM[Convert.ToInt32(roleindexesm[k])] + ",";

        //                        }
        //                    }

        //                }
        //                Response.AppName = Request.AppName;
        //                Response.AppRoleId = roleids.TrimEnd(',');
        //                Response.AppRoleName = Rolename.TrimEnd(',');
        //                //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                //if (getcheckin.Count > 0)
        //                //{
        //                //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                //}
        //                int EmpNo1 = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                var Supervisor1 = db.TblEmployees.Where(x => x.HOD == EmpNo1).ToList();
        //                if (Supervisor1.Count > 0)
        //                {
        //                    Response.IsSupervisor = "yes";
        //                }
        //                else
        //                {
        //                    Response.IsSupervisor = "no";
        //                }

        //                Response.Status = Constant.Success;
        //                Response.IsSuccess = true;
        //                Response.UserId = log.ID.ToString();
        //                Response.DomainId = log.Domain;
        //                Response.DomainUser = log.DomainUser;
        //                Response.Message = Constant.LoginSuccessFull;
        //                Response.DomainIP = adconfig.DomainIP;
        //                Response.DomainName = adconfig.DomainName;
        //                Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
        //                 Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
        //                var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
        //                Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
        //                Response.Language = log.Language.ToString();
        //                Response.wfh = _WFH;
        //                Response.Role = log.Role;
        //                Response.EmpId = log.EmpId;
        //                Response.DepartmentList = log.DepartmentList;

        //                //Response.checkin = spcheckin;
        //                // Response.UpdatedDate = log.UpdatedDate;

        //                //var hodId = empnumber.HOD;
        //                //if (hodId > 0 || hodId != null)
        //                //{
        //                //    string _hodID = Convert.ToString(hodId);
        //                //    var ManagerName = db.TblEmployees.Where(x => x.ID.ToString() == _hodID).FirstOrDefault();
        //                //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
        //                //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
        //                //}
        //                //else
        //                //{
        //                Response.ManagerName = "";
        //                    Response.ManagerNameAR = "";
        //               // }
        //                Response.ProfilePic = empnumber.ProfilePic;
        //                return Response;
        //                // return new Response { Status = "Success", Message = "Login Successfully", id = empnumber.EMPLOYEE_NUMBER, wfh = _WFH, checkin = spcheckin };
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            // Response.Code = (int)Common.Response.Error;
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestNull.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.IsSuccess = false;
        //        Response.Message = Constant.GeneralErrorMsg.ToString();
        //        Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
        //    }

        //    return Response;
        //}

        //public static Object CheckDomainExist(RequestDomain Request)
        //{
        //    InfoGaurdCMSEntities db = new InfoGaurdCMSEntities();
        //    ResponseDomain Response = new ResponseDomain();

        //    try
        //    {
        //        if (Request == null)
        //        {
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestIsEmpty.ToString();
        //            return Response;
        //        }


        //        if (Request != null)
        //        {
        //            #region domain 
        //            //====================> match domain <====================//
        //            var log = db.ADConfigurations.Where(x => x.DomainName == Request.Domain).FirstOrDefault();
        //            if (log == null)
        //            {
        //                Response.IsSuccess = false;
        //                Response.Message = Constant.InvalidUser.ToString();
        //                Response.Status = Constant.InvalidUser.ToString();
        //                return Response;
        //            }
        //            else
        //            {

        //                Response.Status = Constant.Success;
        //                Response.IsSuccess = true;
        //                Response.Message = Constant.GetSuccessfully;
        //                Response.DomainIP = log.DomainIP;
        //                Response.DomainName = log.DomainName;
        //                Response.IsActive = log.IsActive;
        //                Response.UserViewIds = log.UserViewIds;
        //                return Response;
        //                // return new Response { Status = "Success", Message = "Login Successfully", id = empnumber.EMPLOYEE_NUMBER, wfh = _WFH, checkin = spcheckin };
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            // Response.Code = (int)Common.Response.Error;
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestNull.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.IsSuccess = false;
        //        Response.Message = Constant.GeneralErrorMsg.ToString();
        //        Logging.WriteLog(LogType.Error, "domainexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
        //    }

        //    return Response;
        //}

        //public static Object AutoLogin(RequestAutoLogin Request)
        //{
        //    InfoGaurdCMSEntities db = new InfoGaurdCMSEntities();
        //    ResponseLogin Response = new ResponseLogin();

        //    try
        //    {
        //        if (Request == null)
        //        {
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestIsEmpty.ToString();
        //            return Response;
        //        }


        //        if (Request != null)
        //        {
        //            #region User 
        //            //====================> match user <====================//
        //            // string _password = Utility.EncryptionHelper.EncrptPassword(Request.Password);
        //            var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.Domain.Equals(Request.Domain) && x.IsActive == true).FirstOrDefault();
        //            if (log == null)
        //            {
        //                Response.IsSuccess = false;
        //                Response.Message = Constant.InvalidUser.ToString();
        //                Response.Status = Constant.InvalidUser.ToString();
        //                return Response;
        //            }
        //            else
        //            {
        //                var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
        //                bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
        //                //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                //if (getcheckin.Count > 0)
        //                //{
        //                //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                //}
        //                int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                var Supervisor = db.TblEmployees.Where(x => x.HOD == EmpNo).ToList();
        //                if (Supervisor.Count > 0)
        //                {
        //                    Response.IsSupervisor = "yes";
        //                }
        //                else
        //                {
        //                    Response.IsSupervisor = "no";
        //                }

        //                Response.Status = Constant.Success;
        //                Response.IsSuccess = true;
        //                Response.Empid = log.EmpId.ToString();
        //                Response.UserId = log.ID.ToString();
        //                Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
        //                var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
        //                Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
        //                Response.UserName = log.UserName;
        //                Response.Language = log.Language.ToString();
        //                Response.wfh = _WFH;
        //            Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
        //                Response.Role = log.Role;
        //                //Response.checkin = spcheckin;
        //                Response.UpdatedDate = log.UpdatedDate;

        //                var hodId = empnumber.HOD;
        //                //if (hodId > 0 || hodId != null)
        //                //{
        //                //    string _hodID = Convert.ToString(hodId);
        //                //    var ManagerName = db.TblEmployees.Where(x => x.EMPLOYEE_NUMBER == _hodID).FirstOrDefault();
        //                //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
        //                //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
        //                //}
        //                //else
        //                //{
        //                    Response.ManagerName = "";
        //                    Response.ManagerNameAR = "";
        //             //   }
        //                Response.ProfilePic = empnumber.ProfilePic;
        //                Response.Message = Constant.LoginSuccessFull;
        //                return Response;
        //                // return new Response { Status = "Success", Message = "Login Successfully", id = empnumber.EMPLOYEE_NUMBER, wfh = _WFH, checkin = spcheckin };
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            // Response.Code = (int)Common.Response.Error;
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestNull.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.IsSuccess = false;
        //        Response.Message = Constant.GeneralErrorMsg.ToString();
        //        Logging.WriteLog(LogType.Error, "autologinexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
        //    }

        //    return Response;
        //}

        public static Object UpdatePassword(RequestUpdatePassword Request)
        {
            EliteCMSEntities db = new EliteCMSEntities();
            Response Response = new Response();

            try
            {
                if (Request == null)
                {
                    Response.IsSuccess = false;
                    Response.Message = Constant.RequestIsEmpty.ToString();
                    return Response;
                }


                if (Request != null)
                {
                    bool Status = false;
                    var dbcheck = db.Users.Where(x => x.UserName == Request.username).ToList();
                    if (dbcheck.Count() > 0)
                    {

                        Status = true;
                        dbcheck.FirstOrDefault().Password = EncryptionHelper.EncrptPassword(Request.newpassword);
                        dbcheck.FirstOrDefault().UpdatedDate = DateTime.Now;
                        db.SaveChanges();

                        Response.Status = Constant.Success;
                        Response.IsSuccess = Status;
                        Response.Message = Constant.UpdateSuccessFull;

                        return Response;
                    }
                    else
                    {
                        Response.Status = Constant.RequestNull;
                        Response.IsSuccess = Status;
                        Response.Message = Constant.Norecord;

                        return Response;
                    }

                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.Message = Constant.GeneralErrorMsg.ToString();
                Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
            }

            return Response;
        }

        //public static Object LoginMobile(RequestLogin Request)
        //{
        //    InfoGaurdCMSEntities db = new InfoGaurdCMSEntities();
        //    ResponseADUserLogin Response = new ResponseADUserLogin();

        //    try
        //    {
        //        if (Request == null)
        //        {
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestIsEmpty.ToString();
        //            return Response;
        //        }


        //        if (Request != null)
        //        {
        //            #region User 
        //            //====================> match user with active and standard <====================//
        //            // string _password = Utility.EncryptionHelper.EncrptPassword(Request.Password);
        //            var loginuser = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.UserType.Equals("standard") && x.IsActive == true).FirstOrDefault();
        //            if (loginuser != null)
        //            {
        //                string _password = Utility.EncryptionHelper.EncrptPassword(Request.Password);
        //                var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.Password.Equals(_password) && x.IsActive == true).FirstOrDefault();
        //                if (log == null)
        //                {
        //                    Response.IsSuccess = false;
        //                    Response.Message = Constant.InvalidUser.ToString();
        //                    Response.Status = Constant.InvalidUser.ToString();
        //                    return Response;
        //                }
        //                else
        //                {
        //                    var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
        //                    bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
        //                    //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                    //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                    //if (getcheckin.Count > 0)
        //                    //{
        //                    //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                    //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                    //}
        //                    int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                    int Empid = Convert.ToInt32(empnumber.ID);
        //                    var Supervisor = db.TblEmployees.Where(x => x.HOD == Empid).ToList();
        //                    if (Supervisor.Count > 0)
        //                    {
        //                        Response.IsSupervisor = "yes";
        //                    }
        //                    else
        //                    {
        //                        Response.IsSupervisor = "no";
        //                    }

        //                    var roleid = 0;
        //                    var roleids = "";
        //                    var appname = Request.AppName;
        //                    var Rolename = "";
        //                    int roleindex = 0;
        //                    string roleindexs = "";

        //                    var xRes = log.AppIDs.Split('-');
        //                    if (xRes.Length > 0)
        //                    {
        //                        for (int i = 0; i < xRes.Length; i++)
        //                        {
        //                            if (xRes[i].Trim() == Request.AppName)
        //                            {
        //                                roleindexs += i;
        //                                if (i != xRes.Length - 1)
        //                                {
        //                                    roleindexs += ",";
        //                                }
        //                            }

        //                        }
        //                        roleindexs = roleindexs.TrimEnd(',');
        //                    }
        //                    if (roleindexs == null || roleindexs.Trim() == "")
        //                    {
        //                        Response.Message = "UserName or Password Incorrect";
        //                        return Response;
        //                    }
        //                    if (log.AppWiseRoles != null)
        //                    {
        //                        var AppRolesid = log.AppWiseRoles.Split('-');
        //                        if (AppRolesid.Length > 0)
        //                        {
        //                            var roleindexesm = roleindexs.Split(',');
        //                            for (var k = 0; k < roleindexesm.Length; k++)
        //                            {
        //                                //roleid += Convert.ToInt32(roleindexesm[k]);
        //                                var RolenamM = log.RoleNames.Split('-');
        //                                var RoleidM = log.AppWiseRoles.Split('-');

        //                                Rolename += RolenamM[Convert.ToInt32(roleindexesm[k])] + ",";
        //                                roleids += RoleidM[Convert.ToInt32(roleindexesm[k])] + ",";

        //                            }
        //                        }

        //                    }
        //                    Response.AppName = Request.AppName;
        //                    Response.AppRoleId = roleids.TrimEnd(',');
        //                    Response.AppRoleName = Rolename.TrimEnd(',');
        //                    //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                    //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                    //if (getcheckin.Count > 0)
        //                    //{
        //                    //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                    //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                    //}
        //                    int EmpNo1 = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                    var Supervisor1 = db.TblEmployees.Where(x => x.HOD == EmpNo1).ToList();
        //                    if (Supervisor1.Count > 0)
        //                    {
        //                        Response.IsSupervisor = "yes";
        //                    }
        //                    else
        //                    {
        //                        Response.IsSupervisor = "no";
        //                    }

        //                    Response.Status = Constant.Success;
        //                    Response.IsSuccess = true;
        //                    Response.UserId = log.ID.ToString();
        //                    Response.DomainId = log.Domain;
        //                    Response.DomainUser = log.DomainUser;
        //                    Response.Message = Constant.LoginSuccessFull;
        //                    Response.DomainIP = "-";
        //                    Response.DomainName = "-";
        //                    Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
        //                    Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
        //                    var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
        //                    Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
        //                    Response.Language = log.Language.ToString();
        //                    Response.wfh = _WFH;
        //                    Response.Role = log.Role;
        //                    Response.EmpId = log.EmpId;
        //                    Response.DepartmentList = log.DepartmentList;
        //                    Response.DepartmentId = empnumber.DepartmentId.ToString();
        //                    //Response.checkin = spcheckin;
        //                    // Response.UpdatedDate = log.UpdatedDate;

        //                    //var hodId = empnumber.HOD;
        //                    //if (hodId > 0 || hodId != null)
        //                    //{
        //                    //    string _hodID = Convert.ToString(hodId);
        //                    //    var ManagerName = db.TblEmployees.Where(x => x.ID.ToString() == _hodID).FirstOrDefault();
        //                    //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
        //                    //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
        //                    //}
        //                    //else
        //                    //{
        //                    Response.ManagerName = "";
        //                    Response.ManagerNameAR = "";
        //                    // }
        //                    Response.ProfilePic = empnumber.ProfilePic;
        //                    return Response;
        //                }
        //            }

        //            var loginuserActive = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.UserType.Equals("active") && x.IsActive == true).FirstOrDefault();
                   

        //            if (loginuserActive == null)
        //            {
        //                Response.IsSuccess = false;
        //                Response.Message = Constant.InvalidUser.ToString();
        //                Response.Status = Constant.InvalidUser.ToString();
        //                return Response;
        //            }
        //            else
        //            {
        //                int domainid = Convert.ToInt32(loginuserActive.Domain);
        //                var adconfig = db.ADConfigurations.Where(x => x.ID == domainid).FirstOrDefault();
        //                var log = db.Users.Where(x => x.UserName.Equals(Request.UserName) && x.UserType.Equals("active") && x.IsActive == true).FirstOrDefault();  

        //                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, adconfig.DomainIP))
        //                {
        //                    string userPrincipleName = Request.UserName;// + "@" + adconfig.DomainName;
        //                    bool isValid = context.ValidateCredentials(userPrincipleName, Request.Password);
        //                    if (isValid)
        //                    {
        //                        var empnumber = db.TblEmployees.Where(x => x.ID == log.EmpId).FirstOrDefault();
        //                        bool? _WFH = empnumber.WorkFromHome == null ? false : empnumber.WorkFromHome;
        //                        //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                        //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                        //if (getcheckin.Count > 0)
        //                        //{
        //                        //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                        //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                        //}
        //                        int EmpNo = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                        int Empid = Convert.ToInt32(empnumber.ID);
        //                        var Supervisor = db.TblEmployees.Where(x => x.HOD == Empid).ToList();
        //                        if (Supervisor.Count > 0)
        //                        {
        //                            Response.IsSupervisor = "yes";
        //                        }
        //                        else
        //                        {
        //                            Response.IsSupervisor = "no";
        //                        }

        //                        var roleid = 0;
        //                        var roleids = "";
        //                        var appname = Request.AppName;
        //                        var Rolename = "";
        //                        int roleindex = 0;
        //                        string roleindexs = "";

        //                        var xRes = log.AppIDs.Split('-');
        //                        if (xRes.Length > 0)
        //                        {
        //                            for (int i = 0; i < xRes.Length; i++)
        //                            {
        //                                if (xRes[i].Trim() == Request.AppName)
        //                                {
        //                                    roleindexs += i;
        //                                    if (i != xRes.Length - 1)
        //                                    {
        //                                        roleindexs += ",";
        //                                    }
        //                                }

        //                            }
        //                            roleindexs = roleindexs.TrimEnd(',');
        //                        }
        //                        if (roleindexs == null || roleindexs.Trim() == "")
        //                        {
        //                            Response.Message = "UserName or Password Incorrect";
        //                            return Response;
        //                        }
        //                        if (log.AppWiseRoles != null)
        //                        {
        //                            var AppRolesid = log.AppWiseRoles.Split('-');
        //                            if (AppRolesid.Length > 0)
        //                            {
        //                                var roleindexesm = roleindexs.Split(',');
        //                                for (var k = 0; k < roleindexesm.Length; k++)
        //                                {
        //                                    //roleid += Convert.ToInt32(roleindexesm[k]);
        //                                    var RolenamM = log.RoleNames.Split('-');
        //                                    var RoleidM = log.AppWiseRoles.Split('-');

        //                                    Rolename += RolenamM[Convert.ToInt32(roleindexesm[k])] + ",";
        //                                    roleids += RoleidM[Convert.ToInt32(roleindexesm[k])] + ",";

        //                                }
        //                            }

        //                        }
        //                        Response.AppName = Request.AppName;
        //                        Response.AppRoleId = roleids.TrimEnd(',');
        //                        Response.AppRoleName = Rolename.TrimEnd(',');
        //                        //var getcheckin = db.AccessControlAttendanceLogs.Where(x => x.EmployeeNumber == empnumber.EMPLOYEE_NUMBER && DbFunctions.TruncateTime(x.DateTimeOfTxn) == DbFunctions.TruncateTime(DateTime.Now) && x.Status == "IN").ToList();
        //                        //TimeSpan spcheckin = new TimeSpan(0, 0, 0);
        //                        //if (getcheckin.Count > 0)
        //                        //{
        //                        //    var checkin = getcheckin.FirstOrDefault().DateTimeOfTxn.Value.TimeOfDay;
        //                        //    spcheckin = new TimeSpan(checkin.Hours, checkin.Minutes, checkin.Seconds);
        //                        //}
        //                        int EmpNo1 = Convert.ToInt32(empnumber.EMPLOYEE_NUMBER);
        //                        var Supervisor1 = db.TblEmployees.Where(x => x.HOD == EmpNo1).ToList();
        //                        if (Supervisor1.Count > 0)
        //                        {
        //                            Response.IsSupervisor = "yes";
        //                        }
        //                        else
        //                        {
        //                            Response.IsSupervisor = "no";
        //                        }

        //                        Response.Status = Constant.Success;
        //                        Response.IsSuccess = true;
        //                        Response.UserId = log.ID.ToString();
        //                        Response.DomainId = log.Domain;
        //                        Response.DomainUser = log.DomainUser;
        //                        Response.Message = Constant.LoginSuccessFull;
        //                        Response.DomainIP = "-";
        //                        Response.DomainName = "-";
        //                        Response.EmpNo = empnumber.EMPLOYEE_NUMBER;
        //                        Response.workfromhomedayswise = empnumber.WorkFromHomeDayWise;
        //                        var excludefromtimesht = empnumber.ExcludefromTimesheet == null ? false : empnumber.ExcludefromTimesheet;
        //                        Response.ExcludefromTimesheet = excludefromtimesht == false ? "0" : "1";
        //                        Response.Language = log.Language.ToString();
        //                        Response.wfh = _WFH;
        //                        Response.Role = log.Role;
        //                        Response.EmpId = log.EmpId;
        //                        Response.DepartmentList = log.DepartmentList;
        //                        Response.DepartmentId = empnumber.DepartmentId.ToString();
                                

        //                        //Response.checkin = spcheckin;
        //                        // Response.UpdatedDate = log.UpdatedDate;

        //                        //var hodId = empnumber.HOD;
        //                        //if (hodId > 0 || hodId != null)
        //                        //{
        //                        //    string _hodID = Convert.ToString(hodId);
        //                        //    var ManagerName = db.TblEmployees.Where(x => x.ID.ToString() == _hodID).FirstOrDefault();
        //                        //    Response.ManagerName = ManagerName.FULL_NAME == null ? ManagerName.FULL_NAME_AR : ManagerName.FULL_NAME;
        //                        //    Response.ManagerNameAR = ManagerName.FULL_NAME_AR == null ? ManagerName.FULL_NAME : ManagerName.FULL_NAME_AR;
        //                        //}
        //                        //else
        //                        //{
        //                        Response.ManagerName = "";
        //                        Response.ManagerNameAR = "";
        //                        // }
        //                        Response.ProfilePic = empnumber.ProfilePic;
        //                        return Response;

        //                    }
        //                    else
        //                    {
        //                        Response.IsSuccess = false;
        //                        Response.Message = Constant.InvalidUser.ToString();
        //                        Response.Status = Constant.InvalidUser.ToString();
        //                        return Response;
        //                    }

        //                }

        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            // Response.Code = (int)Common.Response.Error;
        //            Response.IsSuccess = false;
        //            Response.Message = Constant.RequestNull.ToString();
        //            Response.Status = Constant.RequestNull.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.IsSuccess = false;
        //        Response.Message = ex.Message;// Constant.GeneralErrorMsg.ToString();
        //        Logging.WriteLog(LogType.Error, "loginexception : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
        //    }

        //    return Response;
        //}

    }
}