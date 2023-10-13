using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data.Entities;

namespace Blogger.Repo
{
    public interface IGenericRepository<Entity> where Entity : BaseEntity
    {
        IEnumerable<Entity> GetAll();
        Entity? Get(int id);
        int Insert(Entity entity);
        int Delete(Entity entity);
        int Update(Entity entity);
        int SaveChanges();
    }
}
