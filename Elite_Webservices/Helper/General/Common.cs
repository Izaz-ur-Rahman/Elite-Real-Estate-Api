using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Webservices.Helpers.General
{
    public class Common
    {
        public enum Response
        {
            Success = 0,
            Error = 1,
            Already = 2,
            NotFound = 3,
            Invalid=4
        }
        public enum Roles
        {
            
            Admin = 1,
            Manager = 2,
            Employee = 3,
            SuperAdmin = 4
        }

        public enum Days
        {

            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 0
        }
    }
}