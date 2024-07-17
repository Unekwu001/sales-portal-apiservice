using Data.Enums;
using Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ViewAllCustomerOrdersDto
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime DateOrdered { get; set; }
        public string FormattedDateOrdered => DateOrdered.ToCustomizedDateString();
        public string Amount { get; set; }
        public OrderStatusEnum Status { get; set; }
        public string CustomerType { get; set; } // To differentiate between SME and Residential
        public string WhoReferredYou { get; set; } 

    }
}
