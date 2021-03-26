namespace PMFightAcademy.Dal
{
    /// <summary>
    /// Class with constant settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Regular expression for parsing a date.
        /// </summary>
        public const string DateRegularExpr = @"^(0[1-9]|1[0-2]).([0-2][0-9]|3[0-1]).[0-9]{4}$";

        /// <summary>
        /// Date format string.
        /// </summary>
        public const string DateFormat = @"MM.dd.yyyy";

        /// <summary>
        /// Regular expression for parsing time.
        /// </summary>
        public const string TimeRegularExpr = @"^([0-1][0-9]|2[0-3]):([0-5][0-9])$";

        /// <summary>
        /// Time format string.
        /// </summary>
        public const string TimeFormat = @"HH:mm";

        /// <summary>
        /// Regular expression with constraints for a phone number.
        /// </summary>
        public const string PhoneRegularExpr = @"^(\+38|38)?0(39|50|63|66|67|68|91|92|93|94|95|96|97|98|99)\d{7}$";

        /// <summary>
        /// Regular expression with constraints for a password.
        /// </summary>
        public const string PasswordRegularExpr = @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$";
    }
}
