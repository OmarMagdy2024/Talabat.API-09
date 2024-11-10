using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Critria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; }=false;

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> critria)
        {
            Critria = critria;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderBydescending)
        {
            OrderByDescending = orderBydescending;
        }

        public void applypagination(int skip,int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
