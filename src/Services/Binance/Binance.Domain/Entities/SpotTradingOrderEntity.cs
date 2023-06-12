using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class SpotTradingOrderEntity : BaseEntity
    {
        public CryptoSymbol CryptoSymbol { get; set; }

        public TradingOrderSide TradingOrderSide { get; set; }

        public TradingOrderType TradingOrderType { get; set; }

        public TradingOrderMode TradingOrderMode { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public CryptoAsset CommissionCryptoAsset { get; set; }

        public decimal Commission { get; set; }

        public decimal GetValuationUSDT()
        {
            return Price * Quantity;
        }

        public override string ToString()
        {
            return $"{CryptoSymbol}|{TradingOrderSide}|{TradingOrderType}|{TradingOrderMode}|{Price}|{Quantity}|{CommissionCryptoAsset}|{Commission}";
        }

        public static SpotTradingOrderEntity CreateOrder(CryptoSymbol cryptoSymbol, TradingOrderSide tradingOrderSide, TradingOrderType tradingOrderType, TradingOrderMode tradingOrderMode, decimal price, decimal quantity)
        {
            var order = BaseEntity.CreateNew<SpotTradingOrderEntity>();
            order.CryptoSymbol = cryptoSymbol;
            order.TradingOrderSide = tradingOrderSide;
            order.TradingOrderType = tradingOrderType;
            order.TradingOrderMode = tradingOrderMode;
            order.Price = price;
            order.Quantity = quantity;
            order.CommissionCryptoAsset = CryptoAsset.BNB;
            order.Commission = 0.00025m;

            return order;
        }
    }
}