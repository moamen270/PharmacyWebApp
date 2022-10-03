using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository.IRepository
{
    public interface IOrderForHeaderRepository : IRepository<OrderForHeader>
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);
    }
}