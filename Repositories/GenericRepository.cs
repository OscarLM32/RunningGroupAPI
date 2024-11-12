using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Data;

namespace RunningGroupAPI.Repositories;

public class GenericRepository<TEntity> where TEntity : class
{
	private AppDbContext _context;
	private DbSet<TEntity> _dbSet;

	public GenericRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = context.Set<TEntity>();
	}

	public virtual async Task<IEnumerable<TEntity>> GetAsync(
		Expression<Func<TEntity, bool>> filter = null,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
	{
		IQueryable<TEntity> query = _dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (orderBy != null)
		{
			return await orderBy(query).ToListAsync();
		}
		else
		{
			return await query.ToListAsync();
		}
	}

	public virtual async Task<TEntity> GetByIdAsync(object id)
	{
		return await _dbSet.FindAsync(id);
	}

	public virtual void Add(TEntity entity)
	{
		_dbSet.Add(entity);
	}

	public virtual void Delete(object id)
	{
		TEntity entityToDelete = _dbSet.Find(id);
		Delete(entityToDelete);
	}

	private void Delete(TEntity entityToDelete)
	{
		if (_context.Entry(entityToDelete).State == EntityState.Detached)
		{
			_dbSet.Attach(entityToDelete);
		}
		_dbSet.Remove(entityToDelete);
	}

	public virtual void Update(TEntity entityToUpdate)
	{
		_dbSet.Attach(entityToUpdate);
		_context.Entry(entityToUpdate).State = EntityState.Modified;
	}
}