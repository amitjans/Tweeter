using System.Security.Cryptography;
using System.Text;

namespace Tweeter.Configuration
{
    public class Sha256
    {

        public static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
