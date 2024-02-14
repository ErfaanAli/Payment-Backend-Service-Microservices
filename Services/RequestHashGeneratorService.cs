using System.Security.Cryptography;
using System.Text;

namespace Payment_Backend_Service.Services
{
    public class RequestHashGeneratorService : IRequestHashGeneratorService
    {
        public string GenerateRequestHash(int employeeId, string month, string requestType)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Concatenate employeeId, month, and requestType
                    string inputString = $"{employeeId}-{month}-{requestType}";

                    // Compute hash
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                    // Convert hash to hexadecimal string
                    StringBuilder hashStringBuilder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hashStringBuilder.Append(b.ToString("x2"));
                    }

                    return hashStringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                //Since It's an existing system feature ,
                //I assume there is a common implementation for exception handling
                // Handle exceptions appropriately (log, display error message, etc.)
                throw ex;
            }
        }
    }

}
