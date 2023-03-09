namespace DSCWebApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Post { get; }
        ICategoryRepository Category { get; }
        IApplicationUserRepository ApplicationUser { get; }

        int Save();
    }
}