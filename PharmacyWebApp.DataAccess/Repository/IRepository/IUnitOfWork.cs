using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IBrandRepository Brand { get; }
        IShopCartRepository ShopCart { get; }
        ICategoryRepository Category { get; }
        IOrderRepository Order { get; }
        IReviewRepository Review { get; }
        int Save();

    }
}
