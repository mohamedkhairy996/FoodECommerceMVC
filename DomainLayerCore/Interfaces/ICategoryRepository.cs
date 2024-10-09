using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DomainLayerCore.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        bool IsExist(string Name);
    }
}
