using System.Collections.Generic;

namespace ContactAPI.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Contacts
        {
            public const string View = "Permissions.Contacts.View";
            public const string Create = "Permissions.Contacts.Create";
            public const string Edit = "Permissions.Contacts.Edit";
            public const string Delete = "Permissions.Contacts.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class ContactSkills
        {
            public const string View = "Permissions.ContactSkills.View";
            public const string Create = "Permissions.ContactSkills.Create";
            public const string Edit = "Permissions.ContactSkills.Edit";
            public const string Delete = "Permissions.ContactSkills.Delete";
        }
    }
}