using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Admin.Models
{
    public class ModelClass
    {
    }
    public class BlogPostModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string BlogDetails { get; set; }
        public string CoverImage { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public string Tags { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string Profileimage { get; set; }
        public Nullable<int> Blog_readTime { get; set; }
        public Nullable<int> Blog_openCount { get; set; }

    }
    public class tokenDetails
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginDetails
    {
        public string CompanyID { get; set; }
        public string userid { get; set; }
        public string EmployeeID { get; set; }
        public byte[] Pic { get; set; }
        public string Name { get; set; }
        public string jobtitle { get; set; }
    }
    public class UserRolesAuth
    {
        public List<string> role { get; set; }
    }

    #region  Ad user
    public class RequestADLogin
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string AppName { get; set; }
    }
    public class RequestDomain
    {
        public string function = "CheckDomainExist";
        public string Domain { get; set; }
    }
    public class RequestAutoLogin
    {
        public string function = "Autologin";
        public string UserName { get; set; }
        public string Domain { get; set; }
        public string AppName { get; set; }
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
    #endregion
    public class LoginNewResponse
    {
        public bool? IsSuccess { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        //   public int id { get; set; }
        public string UserName { get; set; }
        //  public object customertype { get; set; }
        public int? UserId { get; set; }
        public int? Empid { get; set; }
        public int? EmpNo { get; set; }
        public string AppRoleName { get; set; }
        //  public int CompanyID { get; set; }
        // public string RoleType { get; set; }
    }
    public class EmployeProfileModel
    {
        public int id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string profilepicname { get; set; }
        public string ProfilePic { get; set; }
        public DateTime? DateOfHire { get; set; }
        public string MaritialStatus { get; set; }
        public string Mobile { get; set; }
        public string PersonalEmail { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public string EmployeeType { get; set; }
        public string Grade { get; set; }
    }

    public class PopupDataModal
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Headlines { get; set; }
        public string Sentence { get; set; }
        public string Image { get; set; }
        public string Form_Name { get; set; }
        public string Form_Email { get; set; }
        public string TriggerType { get; set; }
        public string DisplayRule { get; set; }
        public string PagelinkToShow { get; set; }
        public string DisplayFrequency { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Createdby { get; set; }
        public string Receivableemail { get; set; }
        public Nullable<bool> showlinkbutton { get; set; }
        public string ButtonLink { get; set; }
        public string ButtonText { get; set; }
        public int? EmailTemplateId { get; set; }
        
    }
    public class PopModalCls
    {
        
        public string Name { get; set; }

        public string Email { get; set; }
        public int? popId { get; set; }

    }
    public   class Cms_BlogPopUserApply
    {
        public int Id { get; set; }
        public Nullable<int> PopwidgetId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string PopName { get; set; }
    }
    public class UserModal
    {
        public int? id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Profileimage { get; set; }
        public string EmpNo { get; set; }
        public int? EmpId { get; set; }


    }
    public class UserProfile
    {
        public int? Empid { get; set; }
        public string EmpNo { get; set; }

        public string Bio { get; set; }
        public string Email { get; set; }

        public string Position { get; set; }

        public DateTime? Birthday { get; set; }

        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }

        public string LinkedIn { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Address { get; set; }
    }
    public class EmailTempletModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Createdby { get; set; }
        
    }
    public   class Cms_EmailTemplete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Details { get; set; }
    }
    public   class Cms_WebsiteAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string Country { get; set; }
        public string NoofEmployees { get; set; }
        public string PageName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Notes { get; set; }
        public string ApplyFor { get; set; }
    }

    public partial class Cms_WebsiteContactus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Notes { get; set; }
    }

    public class ContactU
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string LastName { get; set; }
        public string PageSource { get; set; }
        public string PropertyName { get; set; }
        public string Source { get; set; }
        public string AgentName { get; set; }
        public string Status { get; set; }
    }

    public partial class Quotation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public string BudgetMin { get; set; }
        public string BudgetMax { get; set; }
        public string Message { get; set; }
        public string PageSource { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
    }

    public   class Cms_Campaign
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public string Choose_recipient { get; set; }
        public Nullable<int> PoplistId { get; set; }
        public Nullable<int> EmailTemplateId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        
        public string EmailSubject { get; set; }

        public int?[] recipientCheckbox { get; set; }

    }
    
    public class GetCampaignDetals
    {
        public string Name { get; set; }
        public string CampaignName { get; set; }
        public string Email { get; set; }
        public string EmailTempleteid { get; set; }
        public string Choose_recipient { get; set; }
        public int? Id { get; set; }
        public int? CampignId { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
            public int? tblid { get; set; }
        
        public string EmailStatus { get; set; }

    }

    public class GetCms_Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public string Choose_recipient { get; set; }
        public Nullable<int> PoplistId { get; set; }
        public string PopName { get; set; }
        public Nullable<int> EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Createdby { get; set; }
        public string EmailtempletsDesign { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string EmailSubject { get; set; }

        
    }
    public   class Cms_Manualuser
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Notes { get; set; }
    }


    public class MonthlyBlogCount
    {
        public string Month { get; set; }
        public int BlogCount { get; set; }
    }
    public class UserDetailsModal
    {
        public string Name { get; set; }
        public string Picname { get; set; }
        public string Designation { get; set; }
        public int? BlogCount { get; set; }
        // public string Role { get; set; }
    }
    public class ApplyUserModal
    {
        public int? ContactusCount { get; set; }
        public int? PopUserCount { get; set; }
        public int? Demo { get; set; }
        public int? Trial { get; set; }
        public int? Manual { get; set; }


    }

    public class CountModal
    {
        public int? PropertiesCount { get; set; }
        public int? OffPlanCount { get; set; }
        public int? RentalCount { get; set; }
        public int? SecondryCount { get; set; }
        public int? ContactCount { get; set; }
        public int? QueriesCount { get; set; }

        public int? UserCount { get; set; }
        public int? BlogsCount { get; set; }
        public int? PublishBlogCount { get; set; }


    }
    public   class Cms_NewsLetter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<int> blogId { get; set; }
        public string Blogtitle { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
}