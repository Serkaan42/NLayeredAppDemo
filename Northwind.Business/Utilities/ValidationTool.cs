using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Utilities
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator ,object entity)
        {
            var  result1 = validator.Validate(IValidationContext(entity));
            
            if (result1.Errors.Count > 0)
            {
                throw new ValidationException(result1.Errors);
            }
        }

        private static IValidationContext IValidationContext(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
