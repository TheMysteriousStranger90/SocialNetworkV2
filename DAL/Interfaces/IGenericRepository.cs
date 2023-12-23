namespace DAL.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IList<T>> ListAllAsync();
    Task<IEnumerable<T>> GetAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task DeleteByIdAsync(int id);
    Task<bool> SaveChangesAsync();
}