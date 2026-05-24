using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : AuditBase
{
    protected readonly UniParcheDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(UniParcheDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // ═══ Lectura ═══

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> GetByNameAsync(string name)
    {
        // Busca por propiedad Name si existe
        var property = typeof(T).GetProperty("Name");
        if (property == null)
            return null;

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, "Name");
        var constant = Expression.Constant(name);
        var comparison = Expression.Equal(propertyAccess, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

        return await _dbSet.FirstOrDefaultAsync(lambda);
    }

    public virtual async Task<IEnumerable<T>> GetByExpressionAsync(
        Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    // ═══ Crear ═══

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var entitiesList = entities.ToList();
        await _dbSet.AddRangeAsync(entitiesList);
        await _context.SaveChangesAsync();
        return entitiesList;
    }

    // ═══ Actualizar ═══

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    // ═══ Eliminar ═══

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
        return true;
    }

    // ═══ Utilidades ═══

    public virtual async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }
}
