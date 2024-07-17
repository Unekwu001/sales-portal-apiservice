using Data.Models.UserModels;

namespace Data.Models.CustomerRequestsModels
{
    public class RequestForInstallation : UserTracking
    {
        public Guid Id { get; set; }
        public DateTime InstallationDate { get; set; }
        public string OrderId { get; set; }
    }
}
