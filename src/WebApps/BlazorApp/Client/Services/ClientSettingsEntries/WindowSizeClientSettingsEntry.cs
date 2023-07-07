using System.Drawing;

namespace BlazorApp.Client.Services.ClientSettingsEntries
{
    public class WindowSizeClientSettingsEntry : BaseClientSettingsEntry<Size>
    {
        private const decimal REF_ASSET_ITEM_WIDTH = 350m;//350m
        private const decimal REF_ASSET_ITEM_HEIGHT = 175m;//175m

        public int ClientAreaHeight => Value.Height - 100;

        public int GetVisibleColumnsAmount()
        {
            var columns = (int)Math.Floor(Value.Width / REF_ASSET_ITEM_WIDTH);
            return columns > 0 ? columns : 1;
        }

        public int GetVisibleRowsAmount()
        {
            var rows = (int)Math.Floor(ClientAreaHeight / REF_ASSET_ITEM_HEIGHT);
            return rows > 0 ? rows : 1;
        }

        public int GetRowHeight()
        {
            const int ROW_GAP = 5;
            const int PADDING = 5;
            var rowsAmount = GetVisibleRowsAmount();

            return (ClientAreaHeight - (rowsAmount - 1) * ROW_GAP - 2 * PADDING) / rowsAmount;
        }
    }
}