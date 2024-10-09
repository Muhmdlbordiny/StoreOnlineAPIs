using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications.Products
{
    public class Productspecparms
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int Pagesize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }
}
