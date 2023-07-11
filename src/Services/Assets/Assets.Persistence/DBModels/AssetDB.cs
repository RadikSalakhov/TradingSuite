using Assets.Domain.Entites;
using Assets.Domain.Keys;
using Assets.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assets.Persistence.DBModels
{
    [PrimaryKey("AssetType", "BaseAsset")]
    public class AssetDB : BaseDB<AssetKey, AssetEntity, AssetDB>
    {
        [StringLength(10)]
        public string AssetType { get; set; } = string.Empty;

        [StringLength(10)]
        public string BaseAsset { get; set; } = string.Empty;

        [Column(TypeName = "decimal(38,19)")]
        public decimal LotStepSize { get; set; }

        public override AssetKey GetKey()
        {
            return new AssetKey(AssetType, BaseAsset);
        }

        public override void FromEntity(AssetEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            AssetType = entity.AssetType;
            BaseAsset = entity.BaseAsset;
            LotStepSize = entity.LotStepSize;
        }

        public override AssetEntity ToEntity()
        {
            var entity = AssetEntity.Create(AssetType, BaseAsset);
            entity.LotStepSize = LotStepSize;

            return entity;
        }
    }
}