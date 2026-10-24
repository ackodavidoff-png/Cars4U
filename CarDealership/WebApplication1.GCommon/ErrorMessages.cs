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
    }
}
