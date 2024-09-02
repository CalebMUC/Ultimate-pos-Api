using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.Services
{
    public interface IServices
    {
        public Task<ResponseStatus> AddProducts(ProductListDto productList);
        public Task<IEnumerable<Products>> GetProducts();
    }
}
