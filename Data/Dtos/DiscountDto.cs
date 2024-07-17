using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class DiscountDto : UpdateDiscountDto
    {
     
    }
    public class UpdateDiscountDto
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Percentage { get; set; }
        public ICollection<Guid> PlanTypeIds { get; set; }
    }
}
