using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Dto.orders
{
    public class AddressDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
