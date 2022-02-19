using Microsoft.EntityFrameworkCore;
using SampleCqrs.Domain.Common;

namespace SmapleCqrs.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        //
        private DbContext _context { get; set; }
        public Repository(SampleCqrsDbContext context) => _context = context;
        //
        public async Task<long> Count(CancellationToken cancellationToken) 
        {
            return  await _context.Set<T>().CountAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> Get(CancellationToken cancellationToken, int skip = 0, int take = 0)
        {
            return await _context.Set<T>().Skip(skip).Take(take).AsNoTracking().ToListAsync<T>(cancellationToken);
        }
        public async Task<T> Find(string Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=>x.Id.Equals(Id));
        }
        public async Task<bool> Exist(string Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id.Equals(Id), cancellationToken);
        }
        public async Task Add(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }
        public async Task AddRange(List<T> entities, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        //
    }
}
