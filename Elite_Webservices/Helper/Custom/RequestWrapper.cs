using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Webservices.Helpers.Custom
{

    #region ..........Login ...............

    public class RequestToken
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }
        public string grant_type { get; set; }
    }
    public class RequestLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AppName { get; set; }
    }

    public class RequestLogincheckpasswordpolicy
    {
        public int id { get; set; }
    }
    public class RequestDomain
    {
        public string Domain { get; set; }
    }
    public class RequestAutoLogin
    {
        public string UserName { get; set; }
        public string Domain { get; set; }
        public string AppName { get; set; }
    }
    public class RequestUpdatePassword
    {
      
        public string username { get; set; }
        public string newpassword { get; set; }
    }
    #endregion

    #region .......General................

    public class RequestRecordGetById
    {
        public int id { get; set; }

    }
    public class RequestEmployeebyNum
    {
        public string EmployeeNumber { get; set; }
       
    }
    public class RequestEmployeebyDeprtNo
    {
        public int Departno { get; set; }

    }
    public class RequestDepartmentbyDeptno
    {
        public int deptno { get; set; }

    }
    public class RequestDepartmentddl
    {
        public string EmployeeNumber { get; set; }

    }
    public class RequestDepartmentwiseemployeeddl
    {
        public int EmployeeNumber { get; set; }
        public int departmentid { get; set; }

    }
    public class RequestDepartmentwiseemployee
    {
        public int id { get; set; }

    }
    public class RequestTimesheetdetail
    {
        public int EmployeeNumber { get; set; }
        public DateTime? date { get; set; }

    }

    public class RequestRights
    {
        public int userid { get; set; }
        public string  RightName { get; set; }

    }
    public class RequestAllRights
    {
       
        public int userid { get; set; }


    }
    #endregion

    #region ......Report...............
    public class RequestEmpReport
    {
        public int EmployeeNumber { get; set; }
        public int locationid { get; set; }
        public int departmentid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }
    public class RequestSupervisorEmpReport
    {
        public int supervisorid { get; set; }
        public int EmployeeNumber { get; set; }
        public int locationid { get; set; }
        public int departmentid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }

    public class RequestReportTimeSheetSuperVisorWiseEmployees
    {
       
        public int HOD { get; set; }
        public int locationid { get; set; }
        public int departmentid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }

    public class RequestTimesheet
    {
        public int empno { get; set; }
        public int depno { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

    }
    public class RequestTimesheetAbsent
    {
       
        public int empno { get; set; }
        public int depno { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }
    public class RequestReport
    {
        public int EmployeeNumber { get; set; }
        public int departmentid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public int EmployeeNo { get; set; }

    }

    public class RequestReporAllEmployeesUnderSuperVisor
    {
        
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }

    public class RequestManualAttendance
    {
        public string  empno { get; set; }
        public DateTime? date { get; set; }
        public TimeSpan? checktime { get; set; }
        public TimeSpan? checktime2 { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public string location2 { get; set; }
        public string createdby { get; set; }

    }
    #endregion


    #region Employee Dashbaord

    public class RequestViolation
    {
        public int EmployeeNumber { get; set; }
        public int locationid { get; set; }
        public int departmentid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }
    public class RequestMonthlyViolation
    {
        public int EmployeeNumber { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }
    public class RequestExistViolation
    {
        
        public string EmployeeNumber { get; set; }
        public DateTime? date { get; set; }
        public string DurationType { get; set; }
        public string fromtime { get; set; }
      

    }

    public class RequestRemianingBalance
    {

        public string EmployeeNumber { get; set; }
        
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public string ViolationType { get; set; }

    }
    public class Requestlateemployeecount
    {
       
        public int EmployeeNumber { get; set; }
    }
    public class RequestCheckinCheckout
    {
        
        public string EmployeeNumber { get; set; }
        public string Status { get; set; }
    }

    public class RequestsavecheckinOut
    {
      
        public string EmployeeNumber { get; set; }
        public string Status { get; set; }
    }

    public class RequestsavecheckinOutmob
    {
        public string Location { get; set; }
        public DateTime DateTimeOfTxn { get; set; }
        public string EmployeeNumber { get; set; }
        public string Status { get; set; }
       
    }

    #endregion

    #region Team Dashboard
    public class RequestTeamAttendanceCount
    {
        public int EmployeeNumber { get; set; }
    }
    public class RequestTeamAttendanceEarlylateCount
    {
        public int EmployeeNumber { get; set; }
    }

    public class RequestTeamPendingviolation
    {
        public int EmployeeNumber { get; set; }
    }

    #endregion

    #region Dashboard
    public class RequestChecklanguage
    {
     
        public int userid { get; set; }
    }
    public class RequestlanguageUpdate
    {

        public int userid { get; set; }
        public int id { get; set; }
    }
    public class RequestAddAnnouncement
    {
       
        public int id { get; set; }
        public string name { get; set; }
    }
    #endregion

    #region .........Violation........
    public class RequestViolationmodel
    {
        public int empno { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }
    public class violationRequestModel
    {
        public int empno { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }

        public string duration { get; set; }

        public string durationtype { get; set; }
    }

    public class violationSaveModel
    {
        public int empno { get; set; }
        public DateTime? date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string duration { get; set; }
        public string remainingbalance { get; set; }
        public string type { get; set; }
        public string durationtype { get; set; }
        public string Remarks { get; set; }
    }

    public class approveviolationModel
    {
        public string  empno { get; set; }
        public bool? approve { get; set; }
        public DateTime? date { get; set; }
        public string comment { get; set; }
        
        public string type { get; set; }
        public bool? isedit { get; set; }
        public string fromtime { get; set; }

    }
    public class editviolationModel
    {
        public int id { get; set; }
      
    }
    public class EditviolationRequest
    {
        public int id { get; set; }
        public string duration { get; set; }
        public string remainingbalance { get; set; }
        public string type { get; set; }
        public string remarks { get; set; }
    }

    public class RequestApproverejectModel
    {
        public int userid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }

    public class RequestViolationApprovelistmodel
    {
        public int userid { get; set; }
        public string  empno { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }

    }

    public class RequestViolationGet
    {
        public string empno { get; set; }
        public DateTime? date { get; set; }
    }

    public class RequestViolationListForAdminView
    {
      
        public string empno { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
    }
    #endregion


    #region  ..... Employee......
    public class RequestEmployeeCreate
    {
       
        public string EmployeeNumber { get; set; }
        public string Solutation { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FirstArName { get; set; }
        public string SecondArName { get; set; }
        public string ThirdArName { get; set; }
        public string LastArName { get; set; }
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
        public string DOB { get; set; }
        public string Nationality { get; set; }
        public string HireDate { get; set; }
        public string OrgHireDate { get; set; }
        public string SupervisorNumber { get; set; }
        public string sect { get; set; }
        public string BloodGroup { get; set; }
        public string Idenitification { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> NoOfChild { get; set; }
        public string picturename { get; set; }
        public HttpPostedFileBase file { get; set; }
        public bool? ExcludefromTimesheet { get; set; }
        public Nullable<int> JOB_ID { get; set; }
        public int CreatedBy { get; set; }
        // bank detail
        public int BankId { get; set; }
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
        public Nullable<int> PassportCountryId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PassportNo { get; set; }
        public string PlaceOfIssue { get; set; }
        public string PassportRefNo { get; set; }
        public string VisaType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string VisaNo { get; set; }
        public Nullable<int> VisaCountryId { get; set; }
        public string VisaPlaceOfIssue { get; set; }
        public string Sponsor { get; set; }
        public string VisaIssueDate { get; set; }
        public string VisaExpiryDate { get; set; }
        public string EmimratesIDNO { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> CardExpiryDate { get; set; }

        // salary detail
        public Nullable<decimal> BasicSalary { get; set; }
        public Nullable<decimal> SalaryRange { get; set; }
        public string salarydate { get; set; }
        // public RequestEmployeeManagementmodel employeecreatemodel { get; set; }
    }
    public class RequestEmployeeGetRecordByID
    {
       
        public int id { get; set; }
    }

    public class RequestAddEmployeeShift
    {
      
        public int id { get; set; }
        public int shiftid { get; set; }

    }
    public class RequestEmployeeUpdate
    {
       
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string Solutation { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FirstArName { get; set; }
        public string SecondArName { get; set; }
        public string ThirdArName { get; set; }
        public string LastArName { get; set; }
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
        public string DOB { get; set; }
        public string Nationality { get; set; }
        public string HireDate { get; set; }
        public string OrgHireDate { get; set; }
        public string SupervisorNumber { get; set; }
        public string sect { get; set; }
        public string BloodGroup { get; set; }
        public string Idenitification { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> NoOfChild { get; set; }
        public string picturename { get; set; }
        public HttpPostedFileBase file { get; set; }
        public bool? ExcludefromTimesheet { get; set; }
        public Nullable<int> JOB_ID { get; set; }
        public int CreatedBy { get; set; }
        // bank detail
        public int BankId { get; set; }
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
        public Nullable<int> PassportCountryId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PassportNo { get; set; }
        public string PlaceOfIssue { get; set; }
        public string PassportRefNo { get; set; }
        public string VisaType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string VisaNo { get; set; }
        public Nullable<int> VisaCountryId { get; set; }
        public string VisaPlaceOfIssue { get; set; }
        public string Sponsor { get; set; }
        public string VisaIssueDate { get; set; }
        public string VisaExpiryDate { get; set; }
        public string EmimratesIDNO { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> CardExpiryDate { get; set; }

        // salary detail
        public Nullable<decimal> BasicSalary { get; set; }
        public Nullable<decimal> SalaryRange { get; set; }
        public string salarydate { get; set; }
        public string ipaddress { get; set; }
        public string systemusername { get; set; }
        // public RequestEmployeeManagementmodel employeecreatemodel { get; set; }
    }

    public class RequestAddworkfromhome
    {
        
        public int id { get; set; }
        public bool? wfh { get; set; }
        public string daysname { get; set; }
    }
    public class RequestProfileImage
    {
       
        public int id { get; set; }
        public string filename { get; set; }
        public byte[] ProfilePic { get; set; }
    }
    public class RequestProfileBulkImage
    {
      
        public string EmpNo { get; set; }
        public string filename { get; set; }
        public byte[] ProfilePic { get; set; }
    }

    public class RequestOracleemployee
    {
        public List<clsOracleemployee> employeeList { get; set; }
    }

    public class RequestProfile
    {
        public string userid { get; set; }
    }

    public class RequestAddExperience
    {
       
        public string userid { get; set; }
        public string PrevComp { get; set; }
        public string JobTitle { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string description { get; set; }
    }
    public class RequestAddEducation
    {
      
        public string userid { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string YearCompletion { get; set; }
        public string FieldsOfStudy { get; set; }
        public string Notes { get; set; }
    }
    public class RequestAddSkill
    {
       
        public string userid { get; set; }
        public string Skill { get; set; }
        public string Experience { get; set; }
        public string StartYear { get; set; }
        public string ddllevel { get; set; }

    }
    public class clsOracleemployee
    {

        public string PersonTypeId { get; set; }

        //   [XmlElement("EmployeeNumber")]
        public string EmployeeNumber { get; set; }
        public string OldEmployeeNumber { get; set; }
        public string PayrollId { get; set; }
        public string PersonId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FirstNameAr { get; set; }
        public string SecondNameAr { get; set; }
        public string ThirdNameAr { get; set; }
        public string LastNameAr { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string NationalIdentifier { get; set; }
        public string ManagerFlag { get; set; }
        public string SupervisorNumber { get; set; }
        public string OrganizationId { get; set; }
        public string JobId { get; set; }
        public string PositionId { get; set; }
        public string GradeId { get; set; }
        public string NationalityCode { get; set; }
        public string Sex { get; set; }
        public string MaritalStatusCode { get; set; }
        public string ReligionCode { get; set; }
        public string DateOfBirth { get; set; }
        public string OriginalDateOfHire { get; set; }
        public string HireDate { get; set; }
        public string EmailAddress { get; set; }
        public string MobileSms { get; set; }
        public string MobileM { get; set; }
        public string OfficeNumber { get; set; }
        public string CurrentFlag { get; set; }
        public string PrimaryFlag { get; set; }
        public string AssignmentType { get; set; }
        public string AssignmentCategory { get; set; }
        public string EmploymentCategory { get; set; }
        public string AssignmentId { get; set; }
        public string AssignmentNumber { get; set; }
        public string PayBasisId { get; set; }
        public string AssignmentStatusTypeId { get; set; }
        public string PeopleGroupId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNos { get; set; }
        public string ActualTerminationDate { get; set; }

    }


    #endregion


    #region ..... Shift...........
    public class RequestAddShift
    {
        public string function = "AddShift";
        public int userid { get; set; }
        public int shiftid { get; set; }
        public string shift { get; set; }
        public string shiftAr { get; set; }
        public int location { get; set; }
        public int company { get; set; }
        public string effectivefrom { get; set; }
        public TimeSpan from { get; set; }
        public TimeSpan to { get; set; }
        public string shiftmargin { get; set; }
        public string shiftallowance { get; set; }
        public int? marginstarthr { get; set; }
        public int? marginstartmin { get; set; }
        public int? marginendhr { get; set; }
        public int? marginendmin { get; set; }
        public string[] applicablefor { get; set; }
        public decimal? rate { get; set; }
        public TimeSpan? earlyhrs { get; set; }
        public TimeSpan? earlyfridayhrs { get; set; }
        public TimeSpan tofriday { get; set; }
        public string EffectiveTo { get; set; }
        public string Weekend1 { get; set; }
        public string Weekend2 { get; set; }
    }
    #endregion

    #region Organisation Structure.....
    public class RequestAddBusiness
    {
        public int id { get; set; }
        public  string businessname { get; set; }
        public string businessnameAr { get; set; }
        public int location { get; set; }
        public string phoneno { get; set; }
        public string address { get; set; }
        public int userid { get; set; }

    }

    public class RequestAddBranch
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public int location { get; set; }
        public int business { get; set; }
        public int userid { get; set; }

    }

    public class RequestAddlocation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public int userid { get; set; }

    }

    public class RequestAddDesignation
    {
       
        public int id { get; set; }
        public string desingation { get; set; }
        public string desingationAr { get; set; }
        public int userid { get; set; }

    }

    public class RequestSaveOracleDesignation
    {
       
        public List<clsOracleDesignationImportServiceFields> List { get; set; }


    }
    public class RequestSaveOracleDepartment
    {
       // public string function = "SaveOracleDepartment";
        public List<clsOracleDepartmentImportServiceFields> List { get; set; }
    }

    public class RequestAddDepartment
    {
       
        public int id { get; set; }
        public string department { get; set; }
        public string departmentAr { get; set; }
        public int lead { get; set; }
        public int userid { get; set; }

    }
    public class clsOracleDesignationImportServiceFields
    {
        public string code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }
    public class clsOracleDepartmentImportServiceFields
    {
        public string code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }

    public class RequestHolidayManagement
    {
      //  public string function = "SaveHolidayManagement";
        public int ID { get; set; }

        public string name { get; set; }
        public string nameAr { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public Nullable<int> company { get; set; }
        public Nullable<int> location { get; set; }
        public string description { get; set; }
        public Nullable<int> daysreminder { get; set; }
        public Nullable<bool> notify { get; set; }
        public Nullable<bool> reprocess { get; set; }

        public Nullable<int> CreatedBy { get; set; }
    }

    public class RequestSaveOracleHolidayManagement
    {
        //public string function = "SaveOracleHolidayManagement";
        public List<clsOracleHolidayImportServiceFields> List { get; set; }
    }

    public class clsOracleHolidayImportServiceFields
    {
        public string code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    #endregion


    #region Configuration.................

    public class RequestAddAccountFiled
    {
        public List<AccountFiled> List { get; set; }
    }
    public class RequestAddIncludedSupervisor
    {
       
        public List<IncludedsuperisoremailFiled> List { get; set; }
    }
    public class IncludedsuperisoremailFiled
    {
        public string EmployeeNumber { get; set; }
        public string Name { get; set; }
        public bool Included { get; set; }


    }
    public class AccountFiled
    {
        public int ID { get; set; }
        public string FieldName { get; set; }
        public bool Mandatroy { get; set; }
        public bool Visibility { get; set; }

    }

    public class RequestAddEducationConf
    {
        public int id { get; set; }
        public string education { get; set; }
        public string educationAr { get; set; }
    }
  
    public class RequestAddEmployeeType
    {
        public int id { get; set; }
        public string type { get; set; }
        public string typeAr { get; set; }
    }

    public class RequestOracleService
    {
        public List<clsRequestOracleService> List { get; set; }
    }
    public class clsRequestOracleService
    {
        public string code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }

    public class RequestOracleJobService
    {
        public List<clsRequestOracleJob> List { get; set; }
    }
    public class clsRequestOracleJob
    {
        public int code { get; set; }
        public string desc_A { get; set; }
        public string desc_E { get; set; }
    }

    public class RequestAddReligion
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
    }

    public class RequestAddNationality
    {
        public int id { get; set; }
        public string code { get; set; }
        public string nationality { get; set; }
        public string nationalityAr { get; set; }
    }

    public class RequestAddJob
    {
        public int id { get; set; }
        public int code { get; set; }
        public string jobEn { get; set; }
        public string jobAr { get; set; }
    }

    public class RequestAddEmployeeRank
    {
        public int id { get; set; }
        public string type { get; set; }
        public string typeAr { get; set; }
    }
    public class RequestAddEmailTemplate
    {
       
        public int id { get; set; }
        public string type { get; set; }
        public string subject { get; set; }
        public string emailbody { get; set; }
        public bool enable { get; set; }
    }

    public class RequestAddBank
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
    }
    public class RequestAddsponsor
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
    }
    #endregion

    #region .... Holiday.....

    public class RequestAddHoliday
    {
       public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string affectiveafterdateofjoin { get; set; }
        public bool accurate { get; set; }
        public string accurateday { get; set; }
        public string accuratemonth { get; set; }
        public bool reset { get; set; }
        public string resetday { get; set; }
        public string resetmonth { get; set; }
        public string carryforward { get; set; }
        public string carryforwardMaxLimit { get; set; }
        public string encashement { get; set; }
        public string encashementMaxLimit { get; set; }
        public int religion { get; set; }
        public int department { get; set; }
        public int nationality { get; set; }
        public int position { get; set; }
        public int affectiveafteryear { get; set; }
        public int restrictionyear { get; set; }
        public int restricationdays { get; set; }
        public string description { get; set; }
        public string userid { get; set; }
    }

    public class RequestHRLeavesRequestService
    {
      
        public List<clsHRLeavesRequestService> List { get; set; }
    }
    public class clsHRLeavesRequestService
    {
        public string employee_Number { get; set; }
        public string last_Update_Date { get; set; }
        public string leave_Category { get; set; }
        public string leave_Days { get; set; }
        public string leave_End_Date { get; set; }
        public string leave_Source { get; set; }
        public string leave_Start_Date { get; set; }
        public string leave_Status { get; set; }
        public string leave_Type { get; set; }
        public string leave_Code { get; set; }
        public string leave_Absence_Hours { get; set; }
    }
    #endregion


    public class RequestCheckHoliday
    {
        public DateTime? date { get; set; }
    }


}