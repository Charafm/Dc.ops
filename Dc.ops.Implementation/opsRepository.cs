using Dc.Ops.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


public class OpsRep<T> where T : class
{
    private readonly DcOpsDbContext dcOpsDbContext;
    private readonly ILogger logger;
    private readonly DbSet<T> dbSet;

    public OpsRep(DcOpsDbContext dcOpsDbContext, ILogger<OpsRep<T>> logger)
    {
        this.dcOpsDbContext = dcOpsDbContext;
        this.logger = logger;
        this.dbSet = this.dcOpsDbContext.Set<T>();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
    {
        try
        {
            
            IQueryable<T> query = dcOpsDbContext.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "An error occurred while retrieving data");
            throw; 
        }
    }

    public IQueryable<T> GetWithNoTracking(Expression<Func<T, bool>> predicate = null)
    {
        try
        {
            
            IQueryable<T> query = dcOpsDbContext.Set<T>().AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        catch (Exception ex)
        {
           
            logger.LogError(ex, "An error occurred while retrieving data with no tracking");
            throw; 
        }
    }

    public int Count(Func<T, bool> predicate = null)
    {
        try
        {

            IQueryable<T> query = dcOpsDbContext?.Set<T>() ?? new List<T>().AsQueryable();
            return query.Count();
        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "An error occurred while counting entities");
            throw; 
        }
    }

    public bool Any(Func<T, bool> predicate = null)
    {
        try
        {

            IQueryable<T> query = dcOpsDbContext?.Set<T>() ?? new List<T>().AsQueryable();
            return query.Any();
        }
        catch (Exception ex)
        {
           
            logger.LogError(ex, "An error occurred while checking for existence of entities");
            throw;

        }
    }

    public T FindById(object id)
    {
        try
        {
            return dbSet.Find(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving entity by ID");
            throw; 
        }
    }

    public void Add(T entity)
    {
        try
        {
          
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Cannot add a null entity.");
            }
           
            dbSet.Add(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while adding an entity");
            throw; 
        }
    }

    public void Delete(object id)
    {
        try
        {
           
           
            T entity = dbSet.Find(id);

           
            if (entity == null)
            {
                throw new InvalidOperationException("Entity with the specified ID does not exist.");
            }

            
            dbSet.Remove(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting an entity");
            throw; 
        }
    }

    public void Delete(T entity)
    {
        try
        {
            
            if (!(dcOpsDbContext.Entry(entity).State == EntityState.Detached))
            {
                
                dbSet.Remove(entity);
            }
            else
            {
               
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting an entity");
            throw; 
        }
    }

    public void Update(T entity)
    {
        try
        {
            
            if (dcOpsDbContext.Entry(entity).State == EntityState.Detached)
            {
                
                dbSet.Attach(entity);
            }

           
            dcOpsDbContext.Entry(entity).State = EntityState.Modified;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating an entity");
            throw; 
        }
    }
}