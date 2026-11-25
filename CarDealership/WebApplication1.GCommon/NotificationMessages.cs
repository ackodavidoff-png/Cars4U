using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.GCommon
{
    public class NotificationMessages
    {
        public const string UserNotExists = "The user you are trying to modify no longer exist!";
        public const string RoleAssignNotEists = "The role you are trying to assign no longer exist!";
        public const string UserAlreadyinRole = "User is already in role!";
        public const string UnexpectedError = "Unexpected error occurred! Please try again later!";
        public const string RoleAssigned = "User role assigned successfully!";
        public const string RoleRemoveNotExists = "The role you are trying to remove no longer exist!";
        public const string UserNotInRole = "User was not in the role that you tried to remove!";
        public const string RoleRemoved = "User role removed successfully!";
        public const string CarCreatedSuccessfully = "Car created successfully!";
    }
}
