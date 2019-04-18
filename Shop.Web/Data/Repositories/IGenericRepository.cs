namespace Shop.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);

    }
}
