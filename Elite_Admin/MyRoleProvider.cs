using System;
 
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using Elite_Admin.Controllers;
using Elite_Admin.Models;
using static Elite_Admin.Controllers.AccountController;

namespace Elite_Admin
{
    public class MyRoleProvider : RoleProvider
    {
        
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string login_username)
        {


            //var result = TaskManagementApIsClass.GetUserRolesAuth(login_username);
            //if (!string.IsNullOrEmpty(Convert.ToString(result)))
            //{


            //    var rest = result.role.ToArray();

            //    return rest;
            //}

            //return new[] { "" };
            //UserRolesAuth result = new UserRolesAuth();
            //result = webservices.GetUserRolesAuth(login_username);
            //if (!string.IsNullOrEmpty(Convert.ToString(result)))
            //{
            //    var rest = result.role.ToArray();

            //    return rest;
            //}

            return new[] { "" };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}