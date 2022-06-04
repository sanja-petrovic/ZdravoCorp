using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository
{
    internal interface IRepositoryBase<T,TypeOfId>
    {
        public List<T> GetAll();
        public T GetById(TypeOfId id);
        public void Add(T item);
        public void Remove(T item);
        public void Update(T item);

        public void RemoveAll();
    }
}
