using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private IBasketRepository _repository;
        private ILogger _logger;

        public BasketController(IBasketRepository repository, ILoggerFactory factory)
        {
            _repository = repository;
            _logger = factory.CreateLogger<BasketController>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.getBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Model.Basket value)
        {
            var basket = await _repository.updateBasketAsync(value);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.deleteBasketAsync(id);
        }
    }
}
