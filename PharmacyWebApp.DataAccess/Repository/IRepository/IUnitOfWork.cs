namespace PharmacyWebApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IBrandRepository Brand { get; }
        IShopCartRepository ShopCart { get; }
        ICategoryRepository Category { get; }
        IReviewRepository Review { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderForHeaderRepository OrderForHeader { get; }
        IOrderForDetailRepository OrderForDetail { get; }

        int Save();
    }
}