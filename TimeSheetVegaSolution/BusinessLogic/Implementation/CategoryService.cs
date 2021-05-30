using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _categoryRepository.GetAll().Select(category => new CategoryDto(category)).ToList();
        }

        public CategoryDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDto Save(Category obj)
        {
            throw new NotImplementedException();
        }
        public CategoryDto Edit(Category obj)
        {
            throw new NotImplementedException();
        }

        public CategoryDto Delete(Category obj)
        {
            throw new NotImplementedException();
        }    
    }
}
