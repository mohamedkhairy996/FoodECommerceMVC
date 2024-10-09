using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;

namespace ApplicationLayerServices
{
    public class ProductServices 
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Product> GetProducts()
        {
            return _unitOfWork.Products.GetAll();
        }
        
    }
}
