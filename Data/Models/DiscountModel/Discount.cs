using Data.Models.PlanModels;
using Data.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models.DiscountModel
{
    public class Discount : UserTracking
    {
        public Guid Id { get; set; }
        public string State {  get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime? StartDate { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime? EndDate { get; set; }
        public decimal Percentage { get; set; }
        public virtual ICollection<PlanType> PlanTypes { get; set; }

    }
}
