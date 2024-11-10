using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Repository.Specification
{
    internal class SpecificationEvaluator<T> where T : BaseModel
    {
        public  static int GetCount(IQueryable<T> Sequnce)
        {
            return  Sequnce.Count();
        }
        public static IQueryable<T> GetQuery(IQueryable<T> Sequnce,ISpecification<T> specification)
        {
            var query = Sequnce;
            if (specification.Critria != null)
            {
                query=Sequnce.Where(specification.Critria);
            }
            if(specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if(specification.OrderByDescending != null)
            {
                query=query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IsPagination==true)
            {
                query=query.Skip(specification.Skip).Take(specification.Take);
            }
            query = specification.Includes.Aggregate(query, (Input, outbut) => Input.Include(outbut));
            return query;
        }
    }
}
