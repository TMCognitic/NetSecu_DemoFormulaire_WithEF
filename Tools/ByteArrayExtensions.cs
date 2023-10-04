using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class ByteArrayExtensions
    {
        public static string ToBase64String(this byte[] byteArray) 
        {
            ArgumentNullException.ThrowIfNull(byteArray, nameof(byteArray));
            return Convert.ToBase64String(byteArray);
        }
    }
}
