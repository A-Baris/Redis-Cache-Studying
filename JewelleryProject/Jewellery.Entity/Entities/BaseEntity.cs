using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Entity.Entities
{
    public class BaseEntity
    {
     
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedByIp { get; set; }
        public string? CreatedByComputer { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedByIp { get; set; }
        public string? UpdatedByComputer { get; set; }
    }
}
