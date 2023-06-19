namespace BlazorApp.Client.Services
{
    public interface IClientCacheService
    {
        event Func<Task>? ServerTimeUpdated;

        DateTime GetServerTime();

        void UpdateServerTime(DateTime dateTime);
    }
}