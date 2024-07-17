using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ViewDiscountsDto
    {
        public Guid Id { get; set; }
        public ICollection<Guid>? PlanTypeIds { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Percentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<PlanTypeDetailDto> PlanTypeDetails { get; set; } // List of plan type names
        public string DiscountStatus { get; set; }
        public DateTime Date { get; set; }
    }
}
