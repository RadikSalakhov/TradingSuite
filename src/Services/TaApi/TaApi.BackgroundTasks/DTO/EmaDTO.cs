using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaApi.BackgroundTasks.Entities;

namespace TaApi.BackgroundTasks.DTO
{
    public class EmaDTO : BaseDTO<EmaEntity>
    {
        //[JsonProperty("timestamp")]
        public long timestamp { get; set; }

        //[JsonProperty("value")]
        public decimal value { get; set; }

        public override EmaEntity GetEntity()
        {
            var entity = new EmaEntity();

            entity.ReferenceDT = ConvertDateTimeFromTimestampS(timestamp);
            entity.Value = value;

            return entity;
        }
    }
}