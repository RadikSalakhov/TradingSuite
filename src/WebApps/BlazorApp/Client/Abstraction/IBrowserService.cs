using System.Drawing;

namespace BlazorApp.Client.Abstraction
{
    public interface IBrowserService
    {
        Task InitializeResizeListener(object refObj);

        Task<Size> GetWindowSize();
    }
}