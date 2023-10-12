using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Diagnostics;

namespace TourneyPlanner.API.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
            string path = Directory.GetCurrentDirectory();
            FirebaseApp.Create(new AppOptions()
            {
                //Credential = GoogleCredential.FromFile(@"C:\Users\bocaj\Documents\GitHub\H1-H3---TourneyPlanner\TourneyPlanner\TourneyPlanner.API\Firebase\initial-project-a2af8-firebase-adminsdk-wabct-549c507278.json"),
                Credential = GoogleCredential.FromFile(@$"{path}\Firebase\initial-project-a2af8-firebase-adminsdk-wabct-549c507278.json"),
            });

        }
        public async Task SendNotificationAsync(Message message)
        {
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }
}
