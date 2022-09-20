using PharmacyWebApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Product { get; private set; }

        public IBrandRepository Brand { get; private set; }

        public IShopCartRepository ShopCart { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IReviewRepository Review { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new ProductRepository(_context);
            Brand = new BrandRepository(_context);
            ShopCart = new ShopCartRepository(_context);
            Category = new CategoryRepository(_context);
            Order = new OrderRepository(_context);
            Review = new ReviewRepository(_context);
        }

        

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
