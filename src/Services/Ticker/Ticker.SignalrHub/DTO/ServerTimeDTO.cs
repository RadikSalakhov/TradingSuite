namespace Ticker.SignalrHub.DTO
{
    public class ServerTimeDTO
    {
        public DateTime ServerTime { get; }

        public ServerTimeDTO(DateTime serverTime)
        {
            ServerTime = serverTime;
        }
    }
}