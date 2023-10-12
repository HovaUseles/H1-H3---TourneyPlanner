using FirebaseAdmin.Messaging;

namespace TourneyPlanner.API.Services
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(Message message);
    }
}
