using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using YourLibrary.Models;

namespace YourLibrary.Security
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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

        public override string[] GetRolesForUser(string username)
        {
            YourLibraryDBEntities db = new YourLibraryDBEntities();
            var user = db.Users.FirstOrDefault(x => x.Email == username);
            if (user.Role != true)
            {
                return new string[] { "A" };
                //string adminRole = admin.Role;
            }
            else if(user.Role == true)
            {
                return new string[] { "U" };
                //string lecturerRole = lecturer.Role;
            }
            else
            {
                return null;
            }
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