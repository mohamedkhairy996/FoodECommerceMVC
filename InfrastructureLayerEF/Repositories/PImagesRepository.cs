using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayerEF.Repositories
{
    public class PImagesRepository :GenericRepository<PImages> , IPImagesRepository
    {
        private readonly ApplicationDBContext _context;

        public PImagesRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public PImages GetPImagesByImageUrl(string url)
        {
           return _context.pImages.FirstOrDefault(i => i.ImageUrl == url);
        }
        public ICollection<PImages> GetProductImages(int? id)
        {
            if (id == 0) { return new List<PImages>(); }
            else { return _context.pImages.Where(x => x.ProductId == id).ToList(); }
        }
    }
}
