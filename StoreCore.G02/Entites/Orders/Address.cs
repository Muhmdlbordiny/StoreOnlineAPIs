using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites.Orders
{
    public class Address
    {
        public Address(string fName, string lName, string street, string country, string city)
        {
            FName = fName;
            LName = lName;
            Street = street;
            Country = country;
            City = city;
        }
        public Address()
        {
            
        }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
