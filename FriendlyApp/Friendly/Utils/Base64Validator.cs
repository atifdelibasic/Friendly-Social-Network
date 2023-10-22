using System.Text.RegularExpressions;

namespace Friendly.WebAPI.Utils
{
    public class Base64Validator
    {
        public bool IsBase64String(string s)
        {
            if (s.Length % 4 == 0 && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
            {
                return true;
            }
            return false;
        }
    }
}
