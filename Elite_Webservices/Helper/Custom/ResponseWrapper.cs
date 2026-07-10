using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Webservices.Helpers.Custom
{

    #region .........Login...........

    public class Responsetoken
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string access_token { set; get; }
        public string token_type { set; get; }
        public string expires_in { set; get; }
        public string userName { set; get; }


    }
    public class ResponseLogin
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string UserId { get; set; }
        public string Empid { get; set; }
       
        public string EmpNo { get; set; }
        public string Language { get; set; }
        public string UserName { get; set; }
        public int? Role { get; set; }
        //public bool? wfh { get; set; }
        //public string workfromhomedayswise { get; set; }
        //public string ExcludefromTimesheet { get; set; }
        //public TimeSpan? checkin { get; set; }
        //public string IsSupervisor { get; set; }
        //public string ManagerName { get; set; }
        //public string ManagerNameAR { get; set; }
        public string AppRoleId { get; set; }
        public string AppRoleName{ get; set; }
        public string AppName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ProfilePic { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentList { get; set; }

    }


    public class ResponseLogincheckpasswordpolicy
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        //public Nullable<int> MinCharacter { get; set; }
        //public Nullable<int> MaxCharacter { get; set; }
        //public Nullable<int> MinNumCharacter { get; set; }
        //public Nullable<int> MaxSpecialCharacter { get; set; }
        //public Nullable<bool> IsPasswordExpired { get; set; }
        //public string PasswordExpiredDuration { get; set; }
        //public Nullable<bool> LowerCase { get; set; }
        //public Nullable<bool> UpperCase { get; set; }
        //public string SpecialCharAllowed { get; set; }

        public Models.PasswordPolicy passwordPolicy { get; set; }
       // public List<clsPasswordPolicy> passwordPolicy { get; set; }
       // public RRCDR_AttendanceDAL.PasswordPolicy passwordPolicy { get; set; }
    }

  
    public  class clsPasswordPolicy
    {
        public int ID { get; set; }
        public Nullable<int> MinCharacter { get; set; }
        public Nullable<int> MaxCharacter { get; set; }
        public Nullable<int> MinNumCharacter { get; set; }
        public Nullable<int> MaxSpecialCharacter { get; set; }
        public Nullable<bool> IsPasswordExpired { get; set; }
        public string PasswordExpiredDuration { get; set; }
        public Nullable<bool> LowerCase { get; set; }
        public Nullable<bool> UpperCase { get; set; }
        public string SpecialCharAllowed { get; set; }
    }

    public class ResponseADUserLogin
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string UserId { get; set; }
        public string DomainId { get; set; }
        public string DomainUser { get; set; }
        public string DomainName { get; set; }
        public string DomainIP { get; set; }
        public string EmpNo { get; set; }
        public string IsSupervisor { get; set; }
        public string ExcludefromTimesheet { get; set; }
        public int? Role { get; set; }
        public bool? wfh { get; set; }
        public string workfromhomedayswise { get; set; }
        public TimeSpan? checkin { get; set; }
        public string Language { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNameAR { get; set; }
        public byte[] ProfilePic { get; set; }
        public int? EmpId { get; set; }
        public string AppRoleId { get; set; }
        public string AppRoleName { get; set; }
        public string AppName { get; set; }
        public string DepartmentList { get; set; }
        public string DepartmentId { get; set; }
        
    }

    public class ResponseDomain
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public int ID { get; set; }
        public string DomainName { get; set; }
        public string DomainIP { get; set; }
        public string AppUserName { get; set; }
      //  public string AppPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string UserViewIds { get; set; }

    }

    public class ResponseMobileLogin
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string id { get; set; }
        //public string DomainId { get; set; }
        //public string DomainUser { get; set; }
        //public string DomainName { get; set; }
        //public string DomainIP { get; set; }
        public string EmpNo { get; set; }
        public string supervisor { get; set; }
        public int? Role { get; set; }
        public bool? wfh { get; set; }
        public string workfromhomedayswise { get; set; }
        public TimeSpan? checkin { get; set; }
        public string Location { get; set; }

    }

    #endregion

    #region .........General...........

    public class ResponseEmployeebyNum
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        // public RRCDR_AttendanceDAL.TblEmployee employee { get; set; }
        public List<clsEmployeeddl> EmployeeList { get; set; }
    }

    public class ResponseEmployeebyEmpNumber
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<Employeetbl> employee { get; set; }
    }

    public class ResponseDepartmentbyDeptno
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<Departmenttbl> department { get; set; }
    }
    public class clsEmployeeddl
    {
        public int ID { get; set; }
        public string EMPLOYEE_NUMBER { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
    }
    public class ResponseDepartmentddl
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<ClsDepartmentddl> departmentddlList { get; set; }
    }
    public class ClsDepartmentddl
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }

    }

    public class ResponseDepartmentwisesupervisoremployeeddl
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }

        public List<clsDepartmentwisesupervisoremployeeddl> departmentwisesupervisoremployee { get; set; }
    }

    public class ResponseSupervisorlist
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<supervisorddl> List { get; set; }

    }
    public class ResponseSupervisorlistForIncluded
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<supervisorddlIncluded> List { get; set; }

    }
    public class ResponseBusinessUnit
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsBusinessUnit> List { get; set; }

    }
    public class ResponseWorkLocation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsWorkLocation> List { get; set; }

    }
    public class ResponseEmployeeType
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmployeeType> List { get; set; }

    }
    public class ResponseDesignation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsDesignation> List { get; set; }

    }
    public class ResponseEmployeeRank
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmployeeRank> List { get; set; }

    }
    public class ResponseEducation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEducation> List { get; set; }

    }
    public class ResponseCountry
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsCountry> List { get; set; }

    }
    public class ResponseNationality
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsNationality> List { get; set; }

    }
    public class ResponseReligion
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsReligion> List { get; set; }

    }
    public class ResponseBank
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsBank> List { get; set; }

    }
    public class ResponseSponsor
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSponsor> List { get; set; }

    }
    public class ResponseJob
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsJob> List { get; set; }

    }

    public class ResponseDepartment
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsDepartment> List { get; set; }

    }

    public class ResponseBranchWorklocation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsbranch> List { get; set; }

    }
    public class supervisorddl
    {
        public string EMPLOYEE_NUMBER { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string Department { get; set; }
        public int? DepartmentId { get; set; }
    }
    public class supervisorddlIncluded
    {
        public string EMPLOYEE_NUMBER { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string Department { get; set; }
        public int? DepartmentId { get; set; }
        public int? Included { get; set; }
    }
    public class clsDepartmentwisesupervisoremployeeddl
    {
        public string EMPLOYEE_NUMBER { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string Department { get; set; }
        public int? DepartmentId { get; set; }
    }

    public  class Employeetbl
    {
        public int ID { get; set; }
        public Nullable<int> PERSON_TYPE_ID { get; set; }
        public string EMPLOYEE_NUMBER { get; set; }
        public string OLD_EMPLOYEE_NUMBER { get; set; }
        public Nullable<int> PAYROLL_ID { get; set; }
        public Nullable<int> PERSON_ID { get; set; }
        public string BADGE { get; set; }
        public string TITLE { get; set; }
        public string FIRST_NAME { get; set; }
        public string SECOND_NAME { get; set; }
        public string THIRD_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FIRST_NAME_AR { get; set; }
        public string SECOND_NAME_AR { get; set; }
        public string THIRD_NAME_AR { get; set; }
        public string LAST_NAME_AR { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
        public string NATIONAL_IDENTIFIER { get; set; }
        public string MANAGER_FLAG { get; set; }
        public string SUPERVISOR_NUMBER { get; set; }
        public Nullable<int> ORGANIZATION_ID { get; set; }
        public Nullable<int> JOB_ID { get; set; }
        public Nullable<int> POSITION_ID { get; set; }
        public Nullable<int> GRADE_ID { get; set; }
        public string NATIONALITY_CODE { get; set; }
        public string SEX { get; set; }
        public string MARITAL_STATUS_CODE { get; set; }
        public string RELIGION_CODE { get; set; }
        public string SECT { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public Nullable<System.DateTime> ORIGINAL_DATE_OF_HIRE { get; set; }
        public Nullable<System.DateTime> HIRE_DATE { get; set; }
        public Nullable<System.DateTime> ACTUAL_TERMINATION_DATE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string MOBILE_SMS { get; set; }
        public string MOBILE_M { get; set; }
        public Nullable<int> LOCATION_ID { get; set; }
        public string OFFICE_NUMBER { get; set; }
        public string INTERNAL_LOCATION { get; set; }
        public string CURRENT_FLAG { get; set; }
        public string PRIMARY_FLAG { get; set; }
        public string ASSIGNMENT_TYPE { get; set; }
        public string ASSIGNMENT_CATEGORY { get; set; }
        public string EMPLOYMENT_CATEGORY { get; set; }
        public Nullable<int> ASSIGNMENT_ID { get; set; }
        public string ASSIGNMENT_NUMBER { get; set; }
        public Nullable<int> PAY_BASIS_ID { get; set; }
        public Nullable<int> ASSIGNMENT_STATUS_TYPE_ID { get; set; }
        public Nullable<int> PEOPLE_GROUP_ID { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PHONE_NOS { get; set; }
        public Nullable<System.DateTime> CONTRACT_END_DATE { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DATE_PEOPLE { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DATE_ASSIGNMENTS { get; set; }
        public Nullable<int> BUSINESS_GROUP_ID { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> HOD { get; set; }
        public Nullable<int> EmployeeType { get; set; }
        public Nullable<int> EducationId { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhoneNo { get; set; }
        public string BloodGroup { get; set; }
        public string Idenitification { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> NoOfChild { get; set; }
        public string ProfilePicName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Shift { get; set; }
        public Nullable<bool> WorkFromHome { get; set; }
        public byte[] ProfilePic { get; set; }
        public Nullable<bool> ExcludefromTimesheet { get; set; }
        public string WorkFromHomeDayWise { get; set; }
    }

    public  class Departmenttbl
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string DepartmentName { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> DepartmentLeadId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public class ResponseRights
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string read { get; set; }
        public string write { get; set; }
        public string delete { get; set; }
    }

    public class ResponseAllRights
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsrightslist> List { get; set; }
    }
    public class clsrightslist
    {
        public string name { get; set; }
        public string read { get; set; }
        public string write { get; set; }
        public string delete { get; set; }
    }
    public  class clsBusinessUnit
    {
        public int ID { get; set; }
        public string BusinessUnitName { get; set; }
        public string NameAr { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Createdby { get; set; }
    }
    public  class clsWorkLocation
    {
        public int ID { get; set; }
        public Nullable<int> BusinessUnitId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string WorkLocationName { get; set; }
        public string NameAr { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public  class clsEmployeeType
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string TypeName { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public  class clsDesignation
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string DesigationName { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public class clsEmployeeRank
    {
        public int EmpRankID { get; set; }
        public Nullable<int> code { get; set; }
        public string Grade { get; set; }
        public string NameAr { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> UserID { get; set; }
    }

    public  class clsEducation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public  class clsCountry
    {
        public int CountryId { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public Nullable<bool> isActive { get; set; }
    }

    public class clsNationality
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }

    public class clsReligion
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public  class clsBank
    {
        public int ID { get; set; }
        public string BankName { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public  class clsSponsor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public  class clsJob
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }

    public  class clsDepartment
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string DepartmentName { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> DepartmentLeadId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public class ResponseLocation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsLocation> List { get; set; }

    }

    public class clsLocation
    {
        public int ID { get; set; }
        public string LocationName { get; set; }
        public string NameAr { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class clsbranch
    {
        public int id { get; set; }
        public string businessid { get; set; }
        public string locationid { get; set; }
        public string name { get; set; }
    }
    #endregion

    #region ........Report......
    public class ResponseReport
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheetReport> reportlist { get; set; }
      
    }

    public class ResponseReportMobile
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheetReportMobile> reportlist { get; set; }

    }
    public class ResponseSupervisorReport
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheetReport> reportlist { get; set; }

    }

    public class ResponseReportMonthly
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPEmployeemonthlyViolation> reportlist { get; set; }

    }
    public class ResponseTimeSheet
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheet> List { get; set; }

    }
    public class ResponseTimeSheetdetail
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsdetail> List { get; set; }

    }

    public class ResponseTimeSheetAbsent
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheetAbsentReport> List { get; set; }

    }
    public class ResponseSupervisorUnderEmployeeReport
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPSupervisorUnderEmployeeReport> List { get; set; }

    }
    public class clsSPTimeSheetReport
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public string Locations { get; set; }

        public TimeSpan? ShiftEndHour { get; set; }
        public TimeSpan? attendEndHour { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }
        public int? workHours { get; set; }
        public string TotalHoursMinute { get; set; }
        public int? TotalMinute { get; set; }
        public string IsEverycheckINOut { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public string IsLateApprove { get; set; }
        public string IsEarlyApprove { get; set; }
        public string IsDuringApprove { get; set; }
        public TimeSpan? ViolationDuringTime { get; set; }



        public TimeSpan? TotalViolationTime { get; set; }
        public TimeSpan? LateInViolationTime { get; set; }
        public TimeSpan? LateOutViolationTime { get; set; }
        public TimeSpan? PersonalLeaveOut { get; set; }
        public TimeSpan? OfficialLeaveOut { get; set; }
        public TimeSpan? WeatherLeaveOut { get; set; }
        public TimeSpan? TotalApprovedLeaveOutHours { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorNameAr { get; set; }
        public TimeSpan? ExpectedcheckOut { get; set; }
        public TimeSpan? TotalViolationDuringTimeForExactWorkHour { get; set; }

        // for violation..
        public TimeSpan? StartFromExact { get; set; }
        public TimeSpan? StartFromEarly { get; set; }
        public TimeSpan? EndExact { get; set; }
        public TimeSpan? EndEarly { get; set; }
        public TimeSpan? EndExactFriday { get; set; }
        public TimeSpan? EndEarlyFriday { get; set; }
        public string weekend1 { get; set; } //saturday
        public string weekend2 { get; set; } //sunday

        public int TotalDepartmentEmployeeCount { get; set; }
        public bool isHoliday { get; set; }
    }


    public class clsSPTimeSheetReportMobile
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public string Locations { get; set; }

        public TimeSpan? ShiftEndHour { get; set; }
        public TimeSpan? attendEndHour { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }
        public TimeSpan? TotalworkHours { get; set; }
        public int? workHours { get; set; }
        public string TotalHoursMinute { get; set; }
        public int? TotalMinute { get; set; }
        public string IsEverycheckINOut { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public string IsLateApprove { get; set; }
        public string IsEarlyApprove { get; set; }
        public string IsDuringApprove { get; set; }
        public TimeSpan? ViolationDuringTime { get; set; }



        public TimeSpan? TotalViolationTime { get; set; }
        public TimeSpan? LateInViolationTime { get; set; }
        public TimeSpan? LateOutViolationTime { get; set; }
        public TimeSpan? PersonalLeaveOut { get; set; }
        public TimeSpan? OfficialLeaveOut { get; set; }
        public TimeSpan? WeatherLeaveOut { get; set; }
        public TimeSpan? TotalApprovedLeaveOutHours { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorNameAr { get; set; }
        public TimeSpan? ExpectedcheckOut { get; set; }
        public TimeSpan? TotalViolationDuringTimeForExactWorkHour { get; set; }

        // for violation..
        public TimeSpan? StartFromExact { get; set; }
        public TimeSpan? StartFromEarly { get; set; }
        public TimeSpan? EndExact { get; set; }
        public TimeSpan? EndEarly { get; set; }
        public TimeSpan? EndExactFriday { get; set; }
        public TimeSpan? EndEarlyFriday { get; set; }
        public string weekend1 { get; set; } //saturday
        public string weekend2 { get; set; } //sunday

        public int TotalDepartmentEmployeeCount { get; set; }
    }


    public class clsSPTimeSheet
    {
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName_AR { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }
        public int? workHours { get; set; }
        public string TotalHoursMinute { get; set; }
        public string Location { get; set; }
        public string DeviceLocation { get; set; }
        public string IsEverycheckINOut { get; set; }
        public decimal? Temperature { get; set; }
        public bool? Mask { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public TimeSpan? ViolationDuringTime { get; set; }
        public TimeSpan? TotalViolationDuringTime { get; set; }
    }

    public class clsdetail
    {
        public DateTime? AttendaceDate { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }

        public string Location { get; set; }
        public string DeviceLocation { get; set; }
    }

    public class clsSPTimeSheetAbsentReport
    {
        public string SupervisorName { get; set; }
        public string SupervisorNameAr { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FIRST_NAME { get; set; }
        public string FIRST_NAME_AR { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAME_AR { get; set; }
        public string EMPLOYEE_NUMBER { get; set; }
        // public string IN_TIME { get; set; }
        public DateTime AttendanceDate { get; set; }
        // public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public string Locations { get; set; }
        public string Department { get; set; }
    }

    public class clsSPSupervisorUnderEmployeeReport
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string SupervisorNumber { get; set; }

        public DateTime? AttendaceDate { get; set; }

        public TimeSpan? checkIn { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
    }

    public class clsSPTimeEmployeeMonthly
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public string Locations { get; set; }

        public TimeSpan? ShiftEndHour { get; set; }
        public TimeSpan? attendEndHour { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }
        public int? workHours { get; set; }
        public string TotalHoursMinute { get; set; }
        public int? TotalMinute { get; set; }
        public string IsEverycheckINOut { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public string IsLateApprove { get; set; }
        public string IsEarlyApprove { get; set; }
        public string IsDuringApprove { get; set; }
        public TimeSpan? ViolationDuringTime { get; set; }
    }

    //public class clsSPTimeSheetReport
    //{
    //    public string EmployeeName { get; set; }
    //    public string EmployeeName_AR { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string EmployeeNumber { get; set; }
    //    public string IN_TIME { get; set; }
    //    public DateTime? AttendaceDate { get; set; }
    //    public string OUT_TIME { get; set; }
    //    public string Shifts { get; set; }
    //    public string Locations { get; set; }

    //    public TimeSpan? ShiftEndHour { get; set; }
    //    public TimeSpan? attendEndHour { get; set; }
    //    public TimeSpan? checkIn { get; set; }
    //    public TimeSpan? checkOut { get; set; }
    //    public int? workHours { get; set; }
    //    public string TotalHoursMinute { get; set; }
    //    public int? TotalMinute { get; set; }
    //    public string IsEverycheckINOut { get; set; }
    //    public string VoilationReason { get; set; }
    //    public string Notes { get; set; }
    //    public string Department { get; set; }
    //    public string Remarks { get; set; }
    //    public string IsLateApprove { get; set; }
    //    public string IsEarlyApprove { get; set; }
    //}
    #endregion


    #region Employee Dashboard

    public class ResponseEmpAbsentAttendance
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPTimeSheetReport> reportlist { get; set; }

    }

    public class ResponseMonthlyviolation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSPEmployeemonthlyViolation> reportlist { get; set; }

    }

    public class Responseexistviolation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string count { get; set; }
       
    }

    public class ResponseRemainingBalance
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
       
        public List<Violationview> RemBalancelist { get; set; }
    }

    public class ResponseShift
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }

        public List<Shiftview> Shiftlist { get; set; }
    }

    public class Responselateemployeecount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }

        public List<clsEmployeeCount> EmployeeCountlist { get; set; }
    }

    public class ResponseCheckinCheckout
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }

        public List<AccessControlAttendanceLogview> list { get; set; }
    }

    public class ResponsesavecheckinOut
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string result { get; set; }

    }
    public class ResponsesavecheckinOutmob
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        // public string result { get; set; }

    }
    public class ResponseSyncLog
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<SyncLogview> list { get; set; }

    }
    public class SyncLogview
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
    }

    public class ResponseAttendanceChart
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clschart> list { get; set; }

    }
    public class ResponseAttendanceChartDepartmentwise
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clschartdepartmentwise> list { get; set; }

    }
    public class ResponseChartSupervisorwiseViolation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clschartsupervisorwiseviolation> list { get; set; }

    }
    public class clschart
    {
        public int AtHome { get; set; }
        public int AtOffice { get; set; }
        public int Present { get; set; }
        public int AbsentEmployee { get; set; }
        public DateTime AttendanceDate { get; set; }

    }
    public class clschartdepartmentwise
    {
        public string PresentEmployee { get; set; }
        public string TotalEmployee { get; set; }
        public string Department { get; set; }

    }
    public class clschartsupervisorwiseviolation
    {
        public int Supervisornumber { get; set; }
        public string Supervisor { get; set; }
        public string Supervisornumber_AR { get; set; }
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Reject { get; set; }
        public int Total { get; set; }

    }
    public class clsSPEmployeemonthlyViolation
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public string Shifts { get; set; }
        public string Locations { get; set; }

        public TimeSpan? ShiftEndHour { get; set; }
        public TimeSpan? attendEndHour { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }
        public int? workHours { get; set; }
        public string TotalHoursMinute { get; set; }
        public int? TotalMinute { get; set; }
        public string IsEverycheckINOut { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
    }
    public class Violationview
    {
        public int ID { get; set; }
        public string EmpNum { get; set; }
        public Nullable<System.DateTime> AttenanceDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string EmployeeName { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string Comments { get; set; }
        public Nullable<System.TimeSpan> Balance { get; set; }
        public Nullable<System.TimeSpan> RemainingBalance { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string DurationTime { get; set; }
        public string DurationType { get; set; }
        public Nullable<System.TimeSpan> ViolationDurationTime { get; set; }
        public byte[] AttachFile { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Approved { get; set; }
        public Nullable<bool> Isedit { get; set; }
    }

    public class Shiftview
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<System.TimeSpan> ShiftFrom { get; set; }
        public Nullable<System.TimeSpan> ShiftTo { get; set; }
        public Nullable<bool> ShiftMarginEnable { get; set; }
        public Nullable<int> ShiftMarginStarthr { get; set; }
        public Nullable<int> ShiftMarginStartMin { get; set; }
        public Nullable<int> ShiftMarginEndhr { get; set; }
        public Nullable<int> ShiftMarginEndMin { get; set; }
        public Nullable<bool> ShiftAllowanceEnable { get; set; }
        public Nullable<decimal> RatePerDay { get; set; }
        public string ApplicableFor { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> EffectiveFrom { get; set; }
        public Nullable<bool> THourFirstCheckIn { get; set; }
        public Nullable<bool> THourEveryCheckIn { get; set; }
        public Nullable<bool> MHourStrictMode { get; set; }
        public Nullable<bool> MHourLenietMode { get; set; }
        public Nullable<bool> StrictManualInput { get; set; }
        public Nullable<bool> StrictShiftHours { get; set; }
        public Nullable<int> FullDayhr { get; set; }
        public Nullable<int> FullDaymin { get; set; }
        public Nullable<int> HalfDayhr { get; set; }
        public Nullable<int> HalfDaymin { get; set; }
        public Nullable<bool> LenietManualInput { get; set; }
        public Nullable<bool> LenietShiftHours { get; set; }
        public Nullable<int> LenietPerDayhr { get; set; }
        public Nullable<int> LenietPerDaymin { get; set; }
        public Nullable<bool> ShowOverTime { get; set; }
        public Nullable<bool> MaxHourReq { get; set; }
        public Nullable<int> MaxHourPerDayhr { get; set; }
        public Nullable<int> MaxHourPerDaymin { get; set; }
        public Nullable<bool> GracePeriodEnable { get; set; }
        public Nullable<bool> DeviationFCIEnable { get; set; }
        public Nullable<int> DeviationFCIhr { get; set; }
        public Nullable<int> DeviationFCImin { get; set; }
        public Nullable<bool> DeviationLCOEnable { get; set; }
        public Nullable<int> DeviationLCOhr { get; set; }
        public Nullable<int> DeviationLCOmin { get; set; }
        public Nullable<bool> DeviationWorkHoursEnable { get; set; }
        public Nullable<int> DeviationWorkHourshr { get; set; }
        public Nullable<int> DeviationWorkHoursmin { get; set; }
        public Nullable<int> NoOfDeviation { get; set; }
        public string DeviationPer { get; set; }
        public string DeductPer { get; set; }
        public string daysleaveper { get; set; }
        public string daysleavebalance { get; set; }
        public Nullable<System.TimeSpan> EarlyTime { get; set; }
        public Nullable<System.TimeSpan> EarlyTimeFriday { get; set; }
        public Nullable<System.TimeSpan> ShiftToFriday { get; set; }
        public Nullable<System.DateTime> EffectiveTo { get; set; }
        public string Weekend1 { get; set; }
        public string Weekend2 { get; set; }
    }

    public class clsEmployeeCount
    {

        public string EmployeeNumber { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }

    }

    public  class AccessControlAttendanceLogview
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> DateTimeOfTxn { get; set; }
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string Department { get; set; }
        public string EmployeeNumber { get; set; }
        public string DeviceLocation { get; set; }
        public string VoilationReason { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> Isworkfromhome { get; set; }
    }
    #endregion

    #region Team Dashboard

    public class ResponseTeamAttendanceCount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsDasboardCount> empattendance { get; set; }

    }
    public class ResponseTeamAttendanceEarlylateCount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEarlyLateEmployeeCount> empattendance { get; set; }

    }
    public class ResponseTeamPendingviolation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsviolation> pendinglist { get; set; }

    }
    public class clsDasboardCount
    {
        public int TotalEmployee { get; set; }
        public int present { get; set; }
        public int TotalabsentEmployee { get; set; }
        public int absentCount { get; set; }
        public int leaveCount { get; set; }
        public int PersonalLateIn { get; set; }
        public int OfficialLateIn { get; set; }
        public int PersonalLateOut { get; set; }
        public int OfficialLateOut { get; set; }
        public int workfromhome { get; set; }

    }
    public class clsEarlyLateEmployeeCount
    {

        public string EmployeeNumber { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }

    }
    public class clsviolation
    {
        public int ID { get; set; }
        public string EmpNum { get; set; }
        public DateTime? AttenanceDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string DurationType { get; set; }
        public bool? Approved { get; set; }
    }
    public class clsviolationApproveForList
    {
        public int ID { get; set; }
        public string EmpNum { get; set; }
        public DateTime? AttenanceDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string DurationType { get; set; }
        public bool? Approved { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool? Isedit { get; set; }
        public TimeSpan ViolationDurationTime { get; set; }
    }
    #endregion

    #region Dashboard
    public class ResponseDepartmentwiseviolationchart
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsDepartmentwiseviolationchart> reportlist { get; set; }

    }
    public class ResponseDashoardAttonworkCount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clssp_GetDashboardData> attcount { get; set; }

    }
    public class ResponseDashoardAttleaveabsentCount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clssp_GetLeaveAbsentDashboardData> attcount { get; set; }

    }
    public class ResponseDashoardEarlylatecount
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clssp_GetDashboardEarlyLateData> attcount { get; set; }

    }
    public class ResponseCheckLanguage
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsUser chkuser { get; set; }

    }

    public class ResponseAnnouncement
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsAnnouncement> List { get; set; }

    }
    public class ResponseGetAnnouncementById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsAnnouncement List { get; set; }
    }
    public class ResponseHoliday
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsHoliday> List { get; set; }

    }
    public  class clsAnnouncement
    {
        public int ID { get; set; }
        public string Description { get; set; }
    }
    
    public  class clsHoliday
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
       
        public string Description { get; set; }
    }
    public  class clssp_GetDashboardData
    {
        public Nullable<int> totalemployee { get; set; }
        public Nullable<int> present { get; set; }
        public Nullable<int> absent { get; set; }
        public Nullable<int> workfromhome { get; set; }
    }

    public  class clssp_GetLeaveAbsentDashboardData
    {
        public Nullable<int> absentCount { get; set; }
        public Nullable<int> leaveCount { get; set; }
        public Nullable<int> PersonalLateIn { get; set; }
        public Nullable<int> OfficialLateIn { get; set; }
        public Nullable<int> PersonalLateOut { get; set; }
        public Nullable<int> OfficialLateOut { get; set; }
    }

    public class clssp_GetDashboardEarlyLateData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public Nullable<System.DateTime> AttendaceDate { get; set; }
        public Nullable<System.TimeSpan> checkIn { get; set; }
        public Nullable<System.TimeSpan> checkOut { get; set; }
    }
    public class clsDepartmentwiseviolationchart
    {
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string EmployeeNumber { get; set; }
        public string IN_TIME { get; set; }
        public DateTime? AttendaceDate { get; set; }
        public string OUT_TIME { get; set; }
        public TimeSpan? checkIn { get; set; }
        public TimeSpan? checkOut { get; set; }

        public int deptid { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public int TotalDepartmentEmployeeCount { get; set; }



    }
    public class clsUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> EmpId { get; set; }
        public string Domain { get; set; }
        public string DomainUser { get; set; }
        public string UserType { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> InspectRole { get; set; }
        public string AppId { get; set; }
        public string AppInspection { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Language { get; set; }
    }
    #endregion

    #region ...violation......

    public class violationResponse
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public int empno { get; set; }
        public DateTime? date { get; set; }
        public string from { get; set; }
        public string to { get; set; }

        public TimeSpan? duration { get; set; }
        public TimeSpan? remainingbalance { get; set; }
        public string type { get; set; }
        public string durationtype { get; set; }
        public string Remarks { get; set; }
    }

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
       
    }
    public class ResponseViolationedit
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public Violationmodel violationmodel { get; set; }

    }
    public class ResponseViolationeditbydateandEmpNo
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<Violationmodel> List { get; set; }

    }
    public class Violationmodel
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> AttenanceDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string DurationTime { get; set; }

        public Nullable<System.TimeSpan> RemainingBalance { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string DurationType { get; set; }

    }

    public class ResponseViolationApproveReject
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsviolationApprovereject> List { get; set; }

    }

    public class ResponseViolationApproveList
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsviolationApproveForList> List { get; set; }

    }
    public class clsviolationApprovereject
    {
        public int ID { get; set; }
        public string EmpNum { get; set; }
        public DateTime? AttenanceDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string DurationType { get; set; }
        public bool? Approved { get; set; }
        public string Comments { get; set; }
        public int Isedit { get; set; }
        public string FromTime { get; set; }
        public TimeSpan? ViolationDurationTime { get; set; }
    }

    public class ResponseViolationdetail
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsviolation> List { get; set; }
    }


    public class ResponseViolationListForAdminview
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsviolationListForAdmin> List { get; set; }
    }
    public class clsviolationListForAdmin
    {
        public int ID { get; set; }
        public string EmpNum { get; set; }
        public DateTime? AttenanceDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string ViolationType { get; set; }
        public string Remarks { get; set; }
        public string DurationType { get; set; }
        public bool? Approved { get; set; }
        public string FromTime { get; set; }
    }

    #endregion


    #region  ......Employee........


    public class ResponseEmployee
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsemployeelist> List { get; set; }

    }
    public class ResponseEmployeeFieldsettingErp
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmployee_FieldsettingErp> List { get; set; }
    }
    public class ResponseEmployeeGet
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsEmployeemodel List { get; set; }
    }

    public class ResponseEmployeeProfile
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsemployeeprofile profile { get; set; }
    }

    public class ResponseWorkExperience
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsWorkExperience> List { get; set; }
    }
    public class ResponseSkill
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsSkill> List { get; set; }
    }
    public class ResponseEmployeeEducation
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmployeeEducation> List { get; set; }
    }
    public class clsemployeelist
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeName_AR { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        // public string  location { get; set; }
        public string EmployeeType { get; set; }
        public string Nationality { get; set; }
        public string Branch { get; set; }
        public string SupervisorNumber { get; set; }
        public string PhoneNo { get; set; }
        public string Shift { get; set; }
        public bool? workfromhome { get; set; }
        public string WorkFromHomeDayWise { get; set; }
    }

    public class clsEmployee_FieldsettingErp
    {
        public int ID { get; set; }
        public string FieldName { get; set; }
        public Nullable<bool> Mandatroy { get; set; }
        public Nullable<bool> Visibility { get; set; }
    }
    public class clsEmployeemodel
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string Solutation { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FirstNameAr { get; set; }
        public string SecondNameAr { get; set; }
        public string ThirdNameAr { get; set; }
        public string LastNameAr { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> locationId { get; set; }
        public Nullable<int> EmployeeType { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> HOD { get; set; }
        public Nullable<int> ReportingTo { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> EducationId { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhoneNo { get; set; }
        public Nullable<int> CountryId { get; set; }
        public DateTime? DOB { get; set; }
        public string Nationality { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? OrgHireDate { get; set; }
        public string SupervisorNumber { get; set; }
        public string sect { get; set; }
        public string BloodGroup { get; set; }
        public string Idenitification { get; set; }
        public string ReligionId { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> NoOfChild { get; set; }
        public string picturename { get; set; }
        public string ProfilePicName { get; set; }
        public byte[] ProfilePic { get; set; }
        public HttpPostedFileBase file { get; set; }
        public bool? ExcludefromTimesheet { get; set; }
        public Nullable<int> JOB_ID { get; set; }
        public int CreatedBy { get; set; }
        // bank detail
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string AccountTitle { get; set; }
        public string IBANNO { get; set; }
        public string SwiftCode { get; set; }
        public string TransactionID { get; set; }
        public string PersonID { get; set; }
        public bool defaultaccount { get; set; }

        // visa detail

        public string NameInPassport { get; set; }
        public Nullable<int> passportCountryId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PassportNo { get; set; }
        public string PlaceOfIssue { get; set; }
        public string PassportRefNo { get; set; }
        public string VisaType { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? Expirydate { get; set; }
        public string VisaNo { get; set; }
        public Nullable<int> VisaCountryId { get; set; }
        public string VisaPlaceOfIssue { get; set; }
        public string Sponsor { get; set; }
        public DateTime? VisaIssueDate { get; set; }
        public DateTime? VisaExpiryDate { get; set; }
        public string EmimratesIDNO { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> CardExpiryDate { get; set; }

        // salary detail
        public Nullable<decimal> BasicSalary { get; set; }
        public Nullable<decimal> SalaryRange { get; set; }
        public string salarydate { get; set; }
        public DateTime? CreatedDate { get; set; }
        // public RequestEmployeeManagementmodel employeecreatemodel { get; set; }
    }

    public class clsemployeeprofile
    {
        public int id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public int deptid { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string profilepicname { get; set; }
        public byte[] ProfilePic { get; set; }
        public DateTime? DateOfHire { get; set; }
        public string EmployeeType { get; set; }
        public string Grade { get; set; }
        public string MaritialStatus { get; set; }
        public string BloodGroup { get; set; }
    }

    public  class clsWorkExperience
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string PreviousCompany { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApprove { get; set; }
    }

    public class clsEmployeeEducation
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string FieldsOfStudy { get; set; }
        public Nullable<System.DateTime> YearCompletion { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApprove { get; set; }
    }

    public  class clsSkill
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string SkillName { get; set; }
        public string Experience { get; set; }
        public string ExpLevel { get; set; }
        public Nullable<System.DateTime> StartedYear { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApprove { get; set; }
    }
    #endregion

    #region .... Shift....

    public class ResponseShiftList
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsShiftview> List { get; set; }

    }

    public class ResponseShiftattendancesetting
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsattendancesetting> List { get; set; }

    }

    public class ResponseShiftgraceperiod
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsgraceperiod> List { get; set; }

    }
    public class ResponseGetShiftByid
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsShift shift { get; set; }

    }
    public class clsShiftview
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime? effectivefrom { get; set; }
        public TimeSpan? ShiftFrom { get; set; }
        public TimeSpan? ShiftTo { get; set; }
        public Nullable<bool> ShiftMarginEnable { get; set; }
        public string ShiftMarginStart { get; set; }
        public string ShiftMarginEnd { get; set; }
        public Nullable<bool> ShiftAllowanceEnable { get; set; }
        public Nullable<decimal> RatePerDay { get; set; }
        public string createdby { get; set; }
    }

    public class clsattendancesetting
    {
        public int shiftId { get; set; }

        public string totalhrscalc { get; set; }
        public string minhrforday { get; set; }
        public string strickmaual { get; set; }
        public Nullable<int> FullDayhr { get; set; }
        public Nullable<int> FullDaymin { get; set; }
        public Nullable<int> HalfDayhr { get; set; }
        public Nullable<int> HalfDaymin { get; set; }
        public Nullable<int> LenietPerDayhr { get; set; }
        public Nullable<int> LenietPerDaymin { get; set; }
        public bool showovertime { get; set; }
        public string maxhrreq { get; set; }
        public Nullable<int> MaxHourPerDayhr { get; set; }
        public Nullable<int> MaxHourPerDaymin { get; set; }

    }

    public class clsgraceperiod
    {
        public int shiftid { get; set; }
        public Nullable<bool> GracePeriodEnable { get; set; }
        public Nullable<bool> DeviationFCIEnable { get; set; }
        public Nullable<int> DeviationFCIhr { get; set; }
        public Nullable<int> DeviationFCImin { get; set; }
        public Nullable<bool> DeviationLCOEnable { get; set; }
        public Nullable<int> DeviationLCOhr { get; set; }
        public Nullable<int> DeviationLCOmin { get; set; }
        public Nullable<bool> DeviationWorkHoursEnable { get; set; }
        public Nullable<int> DeviationWorkHourshr { get; set; }
        public Nullable<int> DeviationWorkHoursmin { get; set; }
        public Nullable<int> NoOfDeviation { get; set; }
        public string DeviationPer { get; set; }
        public string DeductPer { get; set; }
        public string daysleaveper { get; set; }
        public string daysleavebalance { get; set; }
    }

    public  class clsShift
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<System.TimeSpan> ShiftFrom { get; set; }
        public Nullable<System.TimeSpan> ShiftTo { get; set; }
        public Nullable<bool> ShiftMarginEnable { get; set; }
        public Nullable<int> ShiftMarginStarthr { get; set; }
        public Nullable<int> ShiftMarginStartMin { get; set; }
        public Nullable<int> ShiftMarginEndhr { get; set; }
        public Nullable<int> ShiftMarginEndMin { get; set; }
        public Nullable<bool> ShiftAllowanceEnable { get; set; }
        public Nullable<decimal> RatePerDay { get; set; }
        public string ApplicableFor { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> EffectiveFrom { get; set; }
        public Nullable<bool> THourFirstCheckIn { get; set; }
        public Nullable<bool> THourEveryCheckIn { get; set; }
        public Nullable<bool> MHourStrictMode { get; set; }
        public Nullable<bool> MHourLenietMode { get; set; }
        public Nullable<bool> StrictManualInput { get; set; }
        public Nullable<bool> StrictShiftHours { get; set; }
        public Nullable<int> FullDayhr { get; set; }
        public Nullable<int> FullDaymin { get; set; }
        public Nullable<int> HalfDayhr { get; set; }
        public Nullable<int> HalfDaymin { get; set; }
        public Nullable<bool> LenietManualInput { get; set; }
        public Nullable<bool> LenietShiftHours { get; set; }
        public Nullable<int> LenietPerDayhr { get; set; }
        public Nullable<int> LenietPerDaymin { get; set; }
        public Nullable<bool> ShowOverTime { get; set; }
        public Nullable<bool> MaxHourReq { get; set; }
        public Nullable<int> MaxHourPerDayhr { get; set; }
        public Nullable<int> MaxHourPerDaymin { get; set; }
        public Nullable<bool> GracePeriodEnable { get; set; }
        public Nullable<bool> DeviationFCIEnable { get; set; }
        public Nullable<int> DeviationFCIhr { get; set; }
        public Nullable<int> DeviationFCImin { get; set; }
        public Nullable<bool> DeviationLCOEnable { get; set; }
        public Nullable<int> DeviationLCOhr { get; set; }
        public Nullable<int> DeviationLCOmin { get; set; }
        public Nullable<bool> DeviationWorkHoursEnable { get; set; }
        public Nullable<int> DeviationWorkHourshr { get; set; }
        public Nullable<int> DeviationWorkHoursmin { get; set; }
        public Nullable<int> NoOfDeviation { get; set; }
        public string DeviationPer { get; set; }
        public string DeductPer { get; set; }
        public string daysleaveper { get; set; }
        public string daysleavebalance { get; set; }
        public Nullable<System.TimeSpan> EarlyTime { get; set; }
        public Nullable<System.TimeSpan> EarlyTimeFriday { get; set; }
        public Nullable<System.TimeSpan> ShiftToFriday { get; set; }
        public Nullable<System.DateTime> EffectiveTo { get; set; }
        public string Weekend1 { get; set; }
        public string Weekend2 { get; set; }
    }

    #endregion

    #region........... Organisation structure.....

    public class Responsebusinessview
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsbusinessview> List { get; set; }

    }
    public class Responsebranchview
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsbranchview> List { get; set; }

    }
    public class ResponseGetbusinessById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsBusinessUnit List { get; set; }

    }
    public class ResponseGetbranchById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsWorkLocation List { get; set; }

    }
    public class ResponseGetlocationById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsLocation List { get; set; }

    }

    public class ResponseGetDesignationById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsDesignation List { get; set; }

    }

    public class ResponseDepartmentviewlist
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsdepartmentview> List { get; set; }

    }

    public class ResponseGetDepartmentbyId
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsDepartment List { get; set; }

    }

    public class ResponseDirectoryview
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsdeptwithcount> List { get; set; }

    }
    public class ResponseDirectorydepartmentwiseemployee
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsdirectorydepartmentwiseemployee> List { get; set; }

    }
    public class ResponseDirectorydepartmentwiseemployeeById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsdirectorydepartmentwiseemployee> List { get; set; }

    }

    public class ResponseHolidayManagement
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsHolidayManagement> List { get; set; }

    }
    public class ResponseGetHolidayManagementById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsHolidayManagement List { get; set; }

    }
    public class clsbusinessview
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }

        public string createdby { get; set; }
    }
    public class clsbranchview
    {
        public int id { get; set; }
        public string business { get; set; }
        public string location { get; set; }
        public string Name { get; set; }
    }

    public class clsdepartmentview
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Lead { get; set; }
        public string createdby { get; set; }
    }

    public class clsdeptwithcount
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public int empcount { get; set; }
    }

    public class clsdirectorydepartmentwiseemployee
    {
        public int id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int deptid { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string profilepicname { get; set; }
        public byte[] ProfilePic { get; set; }
    }

    public  class clsHolidayManagement
    {
        public int ID { get; set; }
        public Nullable<int> code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DaysReminder { get; set; }
        public Nullable<bool> NotifyEmployee { get; set; }
        public Nullable<bool> ReprocessHoliday { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
    #endregion


    #region Configuration.................

    public class ResponseEmployee_Fieldsetting
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmployee_Fieldsetting> List { get; set; }
    }
    public class clsEmployee_Fieldsetting
    {
        public int ID { get; set; }
        public string FieldName { get; set; }
        public Nullable<bool> Mandatroy { get; set; }
        public Nullable<bool> Visibility { get; set; }
    }

    public class ResponseEducationById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsEducation List { get; set; }
    }
    public class ResponseEmployeetypeById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsEmployeeType List { get; set; }
    }
    public class ResponseReligionById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsReligion List { get; set; }
    }
    public class ResponseNationalityById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsNationality List { get; set; }
    }
    public class ResponseJobById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsJob List { get; set; }
    }
    public class ResponseEmployeerankById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsEmployeeRank List { get; set; }
    }

    public class ResponseEmailTemplate
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsEmailTemplate> List { get; set; }
    }

    public  class clsEmailTemplate
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class ResponseEmailTemplateById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsEmailTemplate List { get; set; }
    }
    public class ResponseBankById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsBank List { get; set; }
    }
    public class ResponseSponsorById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsSponsor List { get; set; }
    }
    #endregion

    #region ......... Holiday....

    public class ResponseRequestHolidayList
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clsRequestHoliday> List { get; set; }
    }

    public class ResponseRequestHolidayById
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public clsRequestHoliday List { get; set; }
    }
    public class clsRequestHoliday
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> ValidityFromDate { get; set; }
        public string Unit { get; set; }
        public Nullable<int> EffectiveAfterYear { get; set; }
        public string EffectiveAfterFrom { get; set; }
        public Nullable<System.DateTime> ValidityToDate { get; set; }
        public Nullable<bool> Accurate { get; set; }
        public string AccuralYearly { get; set; }
        public string AccuralDay { get; set; }
        public string AccuralMonth { get; set; }
        public Nullable<bool> Reset { get; set; }
        public string ResetYearly { get; set; }
        public string ResetDay { get; set; }
        public string ResetMonth { get; set; }
        public string CarryForward { get; set; }
        public string CarryForwardMaxLimit { get; set; }
        public string Encashment { get; set; }
        public string EncashmentMaxLimit { get; set; }
        public Nullable<int> ApplicableReligion { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> Department { get; set; }
        public Nullable<int> Nationality { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> RestrictionYear { get; set; }
        public Nullable<int> RestrictionDays { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }


    #endregion


    public class ResponseCheckHolidayForReport
    {
        public bool IsSuccess { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
        public List<clscheckholidayforreport> list { get; set; }

    }

    public class clscheckholidayforreport
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
}