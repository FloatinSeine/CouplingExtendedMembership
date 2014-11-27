
namespace Coupling.Web.ApplicationServices.Memberships
{
    internal class PasswordStrengthRegularExpressions
    {

        /// <summary>
        /// Length must be greater than or equal to 6
        /// Contain one or more uppercase characters
        /// Contain one or more lowercase characters
        /// Contain one or more numeric values
        /// Contain one or more special characters
        /// </summary>
        public const string StrengthWeek = @"(?=^.{6,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";


        /// <summary>
        /// ^                         Start anchor
        /// (?=.*[A-Z].*[A-Z])        Ensure string has two uppercase letters.
        /// (?=.*[!@#$&*])            Ensure string has one special case letter.
        /// (?=.*[0-9].*[0-9])        Ensure string has two digits.
        /// (?=.*[a-z].*[a-z].*[a-z]) Ensure string has three lowercase letters.
        /// .{8}                      Ensure string is of length 8.
        /// $                         End anchor.
        /// </summary>
        public const string StrengthStrong = @"^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$";
    }
}
