namespace WebApplication1.GCommon
{
    public class ErrorMessages
    {
        //Registration
        public const string FirstNameRequired = "First name is required.";
        public const string LastNameRequired = "Last name is required.";
        public const string UsernameRequired = "Username is required.";
        public const string PhoneNumberRequired = "Phone number is required.";
        public const string PhoneNumberIsNotInCorrectFormat = "Phone number should be in this format: 0xxxxxxxxx where x is a digit from 0 to 9.";
        public const string TownIsRequired = "Please select town from the list.";
        //controller errors
        public const string BadRequestErrorMessage = "There was an error with your request";
        //role seeder error messages
        public const string RoleCreateFail = "Role {0} can't be created.";
        //Admin user seeder error messages
        public const string AdminUserSeedFail = "Admin user {0} can't be seeded!";
        public const string AdminUserRoleAddFail = "Admin user {0} can't be added to role {1}!";
    }
}
