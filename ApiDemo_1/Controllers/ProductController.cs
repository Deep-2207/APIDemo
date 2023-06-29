using ApiDemo_1.Model;
using ApiDemo_1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductRepository _repository;
        public ProductController()
        {
            _repository = new ProductRepository();
        }
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }
        [HttpGet("{ProductID}")]
        public Product GetByID(int ProductID)
        {
            return _repository.GetByID(ProductID);
        }

        [HttpPost]
        public void Add([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(product);
            }
        }


        [HttpPut("{ProductID}")]
        public void Updtae(int ProductID, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductID = ProductID;
                _repository.Update(product);
            }
        }
        [HttpDelete("{ProductID}")]
        public void Delete(int ProductID)
        {
            _repository.Delete(ProductID);
        }

    }
}
