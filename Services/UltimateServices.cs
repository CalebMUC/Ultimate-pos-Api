using System.Collections;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
using Ultimate_POS_Api.Repository;

namespace Ultimate_POS_Api.Services
{
    public class UltimateServices : IServices
    {
        private readonly IRepository _repository;
        public UltimateServices(IRepository repository) {
            _repository = repository;
        }

        public async Task<ResponseStatus> AddProducts(ProductListDto productList) {
            return await _repository.AddProducts(productList);
        }


        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _repository.GetProducts();

        }
    }
}
