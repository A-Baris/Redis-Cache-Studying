using Jewellery.BLL.AbstractRepositories;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.BLL.ConcreteRepository
{
    public class CategoryService : Repository<Category>, ICategoryService
    {
        public CategoryService(ProjectContext context) : base(context)
        {
        }
    }
}
