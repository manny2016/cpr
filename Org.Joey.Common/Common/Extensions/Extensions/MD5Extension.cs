

namespace Org.Joey.Common
{
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;
    using System;

    public static class MD5Extension
    {
        public static string GetMd5HashFromFile(this string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}
