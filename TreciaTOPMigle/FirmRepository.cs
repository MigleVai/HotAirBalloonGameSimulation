using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciaTOPMigle
{
    public class FirmRepository<T>  where T : class
    {
        public List<T> GetAll()
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxwithtype = ctx.Set<T>();
                return ctxwithtype.ToList();
            }
        }
        public T Add(T item)
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxWithType = ctx.Set<T>();
                ctxWithType.Add(item);
                ctx.SaveChanges();
                return item;
            }
        }
        public void Delete(int id)
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxWithType = ctx.Set<T>();
                var item = ctxWithType.Find(id);
                ctxWithType.Remove(item);
                ctx.SaveChanges();
            }
        }
        public void AllDelete()
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxWithType = ctx.Set<T>();
                var items = ctxWithType.Select(x => x);
                ctxWithType.RemoveRange(items);
                ctx.SaveChanges();
            }
        }
        public void Update(T item, int id)
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxWithType = ctx.Set<T>();
                var entity = ctxWithType.Find(id);
                if (entity == null)
                {
                    return;
                }
                ctx.Entry(entity).CurrentValues.SetValues(item);
                ctx.SaveChanges();
            }
        }
        public T GetItem(int id)
        {
            using (var ctx = new FirmDBContext())
            {
                var ctxWithType = ctx.Set<T>();
                return ctxWithType.Find(id);
            }
        }
    }
}
