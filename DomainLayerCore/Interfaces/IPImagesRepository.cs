using DomainLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Interfaces
{
    public interface IPImagesRepository : IGenericRepository<PImages>
    {
        PImages GetPImagesByImageUrl(string homeUrl);

        ICollection<PImages> GetProductImages(int? id);
    }
}
