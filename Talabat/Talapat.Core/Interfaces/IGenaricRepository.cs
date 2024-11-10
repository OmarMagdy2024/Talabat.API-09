﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Interfaces
{
	public interface IGenaricRepository<T> where T : BaseModel
	{
		public Task<IReadOnlyList<T>> GetAllAsync();
		public Task<T> GetByIdAsync(int id);
		public Task<int> CreateAsync(T t);
		public Task<int> UpdateAsync(T t);
		public Task<int> DeleteAsync(T t);
		public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification);
        public Task<T> GetByIdWithSpecAsync(ISpecification<T> specification);
        public int GetCount();
    }
}
