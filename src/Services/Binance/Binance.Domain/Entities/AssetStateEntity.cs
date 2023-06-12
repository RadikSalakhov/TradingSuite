using Binance.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Domain.Entities
{
    public class AssetStateEntity : BaseEntity
    {
        #region Public Properties

        public CryptoAsset CryptoAsset { get; set; }

        public decimal Quantity { get; set; }

        public decimal SpentUSDT { get; set; }

        public decimal LastBuyPrice { get; set; }

        public decimal LastSellPrice { get; set; }

        public bool IsHidden { get; set; }

        public bool AutoBuyEnabled { get; set; }

        public bool AutoBuyPlusEnabled { get; set; }

        public bool AutoSellEnabled { get; set; }

        #endregion

        #region Public Methods

        public void ToggleAutoBuy()
        {
            if (!AutoBuyEnabled)
            {
                AutoBuyEnabled = true;
                AutoBuyPlusEnabled = false;
            }
            else
            {
                if (!AutoBuyPlusEnabled)
                {
                    AutoBuyEnabled = true;
                    AutoBuyPlusEnabled = true;
                }
                else
                {
                    AutoBuyEnabled = false;
                    AutoBuyPlusEnabled = false;
                }
            }
        }

        public decimal GetPurchasePrice()
        {
            return Quantity != 0m
                ? SpentUSDT / Quantity
                : 0m;
        }

        public decimal GetLastSellPriceDiffCoeff(decimal price)
        {
            return LastSellPrice != 0m
                ? price / LastSellPrice - 1m
                : 0m;
        }

        public decimal GetPnlCoeff(decimal assetPrice)
        {
            var purchasePrice = GetPurchasePrice();
            if (purchasePrice == 0m)
                return 0;

            var coeff = assetPrice / purchasePrice;

            return coeff - 1m;
        }

        public decimal GetPnlCoeffNormalized(decimal assetPrice, decimal? multiplier = null)
        {
            var pnlCoeff = GetPnlCoeff(assetPrice);

            return SpentUSDT != 0m
                ? (multiplier ?? 1m) * pnlCoeff / SpentUSDT
                : 0m;
        }

        public decimal GetPnlUSDT(decimal assetPrice)
        {
            return Quantity * assetPrice - SpentUSDT;
        }

        public void CopyFrom(AssetStateEntity settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            Id = settings.Id;
            CreateDT = settings.CreateDT;
            UpdateDT = settings.UpdateDT;

            CryptoAsset = settings.CryptoAsset;
            Quantity = settings.Quantity;
            SpentUSDT = settings.SpentUSDT;
            LastBuyPrice = settings.LastBuyPrice;
            LastSellPrice = settings.LastSellPrice;
            IsHidden = settings.IsHidden;
            AutoBuyEnabled = settings.AutoBuyEnabled;
            AutoBuyPlusEnabled = settings.AutoBuyPlusEnabled;
            AutoSellEnabled = settings.AutoSellEnabled;
        }

        public AssetStateEntity GetCopy()
        {
            var settings = new AssetStateEntity();

            settings.CopyFrom(this);

            return settings;
        }

        #endregion

        #region Public Static Methods

        public static AssetStateEntity GetDefault(CryptoAsset cryptoAsset)
        {
            var entity = CreateNew<AssetStateEntity>();

            entity.CryptoAsset = cryptoAsset;
            entity.Quantity = 0m;
            entity.SpentUSDT = 0m;
            entity.LastBuyPrice = 0m;
            entity.LastSellPrice = 0m;
            entity.IsHidden = false;
            entity.AutoBuyEnabled = false;
            entity.AutoBuyPlusEnabled = false;
            entity.AutoSellEnabled = false;

            return entity;
        }

        #endregion
    }
}