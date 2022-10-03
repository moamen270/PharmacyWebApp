using PharmacyWebApp.DataAccess.Repository.IRepository;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Product { get; private set; }

        public IBrandRepository Brand { get; private set; }

        public IShopCartRepository ShopCart { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IReviewRepository Review { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderForHeaderRepository OrderForHeader { get; private set; }
        public IOrderForDetailRepository OrderForDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new ProductRepository(_context);
            Brand = new BrandRepository(_context);
            ShopCart = new ShopCartRepository(_context);
            Category = new CategoryRepository(_context);
            Review = new ReviewRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            OrderForHeader = new OrderForHeaderRepository(_context);
            OrderForDetail = new OrderForDetailRepository(_context);
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