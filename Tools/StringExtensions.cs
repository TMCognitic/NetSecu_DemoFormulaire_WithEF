using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public static class StringExtensions
    {
        public static byte[] Hash(this string value)
        {
            byte[] toHash = Encoding.Default.GetBytes(value);
            return SHA512.HashData(toHash);
        }

    }
}