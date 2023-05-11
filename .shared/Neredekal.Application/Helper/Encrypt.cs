using System.Text;
using System.Security.Cryptography;

namespace Neredekal.Application.Helper
{
    public static class Encrypt
    {
        private static string secretKey = ReadConfig.CustomSectionValue("Settings", "SecretKey");

        public static string MD5(string value)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(value);
            btr = md5.ComputeHash(btr);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        public static string SHA256(string value)
        {
            byte[] apiKey = Encoding.ASCII.GetBytes(secretKey);
            byte[] byteData = Encoding.ASCII.GetBytes(value);
            HMACSHA256 hmac = new HMACSHA256(apiKey);
            byte[] hashHMAC = hmac.ComputeHash(byteData);

            StringBuilder sb = new StringBuilder();
            foreach (byte ba in hashHMAC)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}