using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.Repository
{
    public interface IRepository
    {
        public Task<ResponseStatus> AddProducts(ProductListDto JsonData);

        public Task<IEnumerable<Products>> GetProducts();
    }
}
