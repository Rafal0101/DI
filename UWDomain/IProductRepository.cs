using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWDomain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Guid id);
    }
}
