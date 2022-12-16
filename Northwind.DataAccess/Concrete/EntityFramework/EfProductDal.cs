using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using Northwind.DataAccess.Abstract;
using System.Linq.Expressions;

namespace Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal :EfEntityRepositoryBase<Product,NorthwindContext> ,IProductDal
    { //bir class ın bir base in olmaması kötü kokudur.
        
    }
}
