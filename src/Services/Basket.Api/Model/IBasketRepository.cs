using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Api.Model
{
    public interface IBasketRepository
    {
        Task<Basket> getBasketAsync(string customerId);
        IEnumerable<string> getUsers();
        Task<Basket> updateBasketAsync(Basket basket);
        Task<bool> deleteBasketAsync(string id);
    }
}
