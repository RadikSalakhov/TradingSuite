namespace Services.Common.WorkerHandlers
{
    public sealed class WorkerHandlerAction
    {
        public long IntervalMS { get; set; }

        public Func<Task> Action { get; set; }

        public DateTime LastDT { get; set; }
    }
}