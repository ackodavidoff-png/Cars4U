namespace WebApplication1.GCommon
{
    public class EntityConstraints
    {
        //car
        public const int CarBrandNameMaxLength = 50;
        public const int CarBrandNameMinLength = 1;
        public const int CarModelNameMaxLength = 50;
        public const int CarModelNameMinLength = 1;
        public const int CarProductionYearMinValue = 1900;
        public const int CarProductionYearMaxValue = 2100;
        public const int CarDescriptionMinLength = 1;
        public const int CarDescriptionMaxLength = 5000;
        public const double CarPriceMinValue = 0.01;
        public const double CarPriceMaxValue = 1000000000;
        public const int CarMileageMinValue = 1;
        public const int CarMileageMaxValue = 1000000;
        public const int CarHorsepowerMinValue = 1;
        public const int CarHorsepowerMaxValue = 2500;
        //Application user
        public const int UserFirstNameMaxLength = 50;
        public const int UserFirstNameMinLength = 1;
        public const int UserLastNameMaxLength = 50;
        public const int UserLastNameMinLength = 1;
        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 50;
        public const string PhoneNumberRegexPattern = @"0\d{9}";
    }
}
