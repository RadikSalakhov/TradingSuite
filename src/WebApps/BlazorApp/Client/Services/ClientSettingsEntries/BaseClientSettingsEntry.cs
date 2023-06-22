using System.Drawing;

namespace BlazorApp.Client.Services.ClientSettingsEntries
{
    public abstract class BaseClientSettingsEntry<T>
    {
        public event Func<Task>? Updated;

        public T? Value { get; private set; }

        public async Task Set(T value)
        {
            Value = value;

            if (Updated != null)
                await Updated.Invoke();
        }
    }
}