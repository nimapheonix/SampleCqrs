namespace SampleCqrs.Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        Task<long> Count(CancellationToken cancellationToken);
        Task<T> Find(string Id, CancellationToken cancellationToken);
        Task<bool> Exist(string Id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> Get(CancellationToken cancellationToken, int skip = 0, int take = 0);
        Task Add(T entity,CancellationToken cancellationToken);
        Task AddRange(List<T> entities, CancellationToken cancellationToken);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Delete(T entity, CancellationToken cancellationToken);
    }
}
