using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductDataModels
{
    public class NewProductStaging
    {
        public int Id { get; set; }
        public long UTCTimestamp { get; set; }
        public string JSONData { get; set; }
        public bool IsProcessed { get; set; }
    }
}
