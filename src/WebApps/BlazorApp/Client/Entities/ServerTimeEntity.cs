namespace BlazorApp.Client.Entities
{
    public class ServerTimeEntity
    {
        public DateTime ServerTime { get; } = DateTime.MinValue;

        public ServerTimeEntity()
        {
        }

        public ServerTimeEntity(DateTime serverTime)
        {
            ServerTime = serverTime;
        }
    }
}