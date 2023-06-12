namespace Binance.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime UpdateDT { get; set; }

        public virtual string GetDescription()
        {
            return ToString() ?? string.Empty;
        }

        public static TEntity CreateNew<TEntity>()
            where TEntity : BaseEntity, new()
        {
            var nowDT = DateTime.UtcNow;

            return new TEntity
            {
                Id = Guid.NewGuid(),
                CreateDT = nowDT,
                UpdateDT = nowDT
            };
        }
    }
}