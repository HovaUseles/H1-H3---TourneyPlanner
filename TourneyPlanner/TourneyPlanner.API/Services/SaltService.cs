using System.Diagnostics;
using System.Security.Cryptography;

namespace TourneyPlanner.API.Services
{
    public class SaltService : ISaltService
    {
        public string GenerateSalt()
        {
            try
            {
                return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
