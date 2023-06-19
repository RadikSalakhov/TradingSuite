namespace BlazorApp.Client.Services
{
    public class ClientCacheService : IClientCacheService
    {
        private DateTime _serverTime = DateTime.MinValue;

        public event Func<Task>? ServerTimeUpdated;

        public DateTime GetServerTime()
        {
            return _serverTime;
        }

        public void UpdateServerTime(DateTime dateTime)
        {
            _serverTime = dateTime;

            ServerTimeUpdated?.Invoke();
        }
    }
}