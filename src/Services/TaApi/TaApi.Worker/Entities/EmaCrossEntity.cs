using TaApi.Worker.Data;

namespace TaApi.Worker.Entities
{
    public class EmaCrossEntity : BaseEntity
    {
        public Asset Asset { get; set; }

        public TAInterval TAInterval { get; set; }

        public decimal ValueShort { get; set; }

        public decimal ValueLong { get; set; }

        public decimal PrevValueShort { get; set; }

        public decimal PrevValueLong { get; set; }
    }
}