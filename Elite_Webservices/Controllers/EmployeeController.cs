using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elite_Webservices.Models;

namespace Elite_Webservices.Controllers
{
    [System.Web.Http.Authorize]

    public class EmployeeController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Employee/EmployeeProfile")]
        public HttpResponseMessage EmployeeProfile(string EmpNo)
        {
            var result = (from e in db.TblEmployees
                              //join d in db.Departments on e.DepartmentId.ToString() equals d.DeptID.ToString() into dep
                              //from dpp in dep.DefaultIfEmpty()
                              //join des in db.Designations on e.POSITION_ID equals des.ID into desig// des.ID
                              //from de in desig.DefaultIfEmpty()
                          where e.EMPLOYEE_NUMBER == EmpNo
                          select new
                          {
                              id = e.ID,
                              EmployeeNumber = e.EMPLOYEE_NUMBER,
                              EmployeeName = e.FULL_NAME == null ? e.FULL_NAME_AR : e.FULL_NAME,
                              EmployeeNameAr = e.FULL_NAME_AR == null ? e.FULL_NAME : e.FULL_NAME_AR,
                              Email = e.EMAIL_ADDRESS == null ? "__" : e.EMAIL_ADDRESS,
                              Birthday = e.DATE_OF_BIRTH,
                              Gender = e.SEX == null ? "__" : e.SEX,
                              Phone = e.MOBILE_M == null ? "__" : e.MOBILE_M,
                              Address = e.Address == null ? "__" : e.Address,
                              //Department = dpp.DeptNameEn == null ? "__" : dpp.DeptNameEn,
                              Department = "",
                              //DepartmentAr = dpp.DeptNameAr == null ? "__" : dpp.DeptNameAr,
                              DepartmentAr = "",
                              Designation = "",
                              //Designation = de.DesigationName == null ? "__" : de.DesigationName,
                              profilepicname = e.ProfilePicName,
                              ProfilePic = e.ProfilePic,
                              DateOfHire = e.HIRE_DATE,
                              MaritialStatus = e.MARITAL_STATUS_CODE == null ? "__" : e.MARITAL_STATUS_CODE,
                              Mobile = e.MOBILE_M,
                              PersonalEmail = e.PersonalEmail,
                              BloodGroup = e.BloodGroup == null ? "__" : e.BloodGroup,
                              //    Nationality = db.Nationalities.Where(x => x.Code == e.NATIONALITY_CODE).FirstOrDefault().desc_E == null ? "__" : db.Nationalities.Where(x => x.Code == e.NATIONALITY_CODE).FirstOrDefault().desc_E,
                              //    EmployeeType = db.EmployeeTypes.Where(x => x.code == e.EmployeeType).FirstOrDefault().TypeName == null ? "__" : db.EmployeeTypes.Where(x => x.code == e.EmployeeType).FirstOrDefault().TypeName,
                              //    Grade = db.EmployeeRanks.Where(x => x.code == e.GRADE_ID).FirstOrDefault().Grade == null ? "__" : db.EmployeeRanks.Where(x => x.code == e.GRADE_ID).FirstOrDefault().Grade
                              Nationality = "__",
                              EmployeeType =   "__" ,
                              Grade = "__"  

                          }).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }
    }
}
