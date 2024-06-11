using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Common
{
    public class HashPassword
    {

        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // Assuming the hashed password is stored as Base64 encoded string
            byte[] originalHashBytes = Convert.FromBase64String(hashedPassword);

            using (var sha256 = SHA256.Create())
            {
                byte[] computedHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (originalHashBytes.SequenceEqual(computedHashBytes))
                {
                    return true; // Passwords match
                }
                else
                {
                    return false; // Passwords do not match
                }
            }
        }


    }
}
