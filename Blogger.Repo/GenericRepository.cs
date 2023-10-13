using Blogger.Data;
using Blogger.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repo
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseEntity
    {
        private readonly DbSet<Entity> _entities;
        private readonly BloggerDbContext _context;

        public GenericRepository(BloggerDbContext context)
        {
            _context = context;
            _entities = context.Set<Entity>();
        }
        public IEnumerable<Entity> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public Entity? Get(int id)
        {
            return _entities.FirstOrDefault(s => s.Id == id);
        }
        public int Insert(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            return SaveChanges();
        }

        public int Update(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Update(entity);
            return SaveChanges();
        }

        public int Delete(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            return SaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}