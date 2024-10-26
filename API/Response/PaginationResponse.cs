using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Response
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}