using Northwind.Business.Abstract;
using Northwind.Business.Utilities;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class ProductManager :IProductService
    {
        private IProductDal _productDal;
  

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
           // ValidationTool.Validate(new ProductValidator(), product);//**********

            //ProductValidator pv= new ProductValidator();
            //pv.Validate(product);
            _productDal.Add(product);
        }
        public void Update(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Update(product);
        }

        public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
            }
            catch 
            {
                throw new Exception("Silme gerçekleşemedi");
            }
                
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();// Aldığım hata  Nortwind.Business a entitiy yi referance etmedik.
            //Business code : iş kodları
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
           return _productDal.GetAll(p=>p.CategoryId == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

       
    }
}
