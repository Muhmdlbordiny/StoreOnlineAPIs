using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Helper
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pagesize, int pageindex, int count, IEnumerable<TEntity> data)
        {
            Pagesize = pagesize;
            Pageindex = pageindex;
            Count = count;
            Data = data;
        }

        public int Pagesize {  get; set; }
        public int Pageindex {  get; set; }
        public int Count {  get; set; }
        public IEnumerable<TEntity> Data {  get; set; }
    }
}
