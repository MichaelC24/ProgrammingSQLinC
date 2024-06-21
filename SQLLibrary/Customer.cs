using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLibrary
{
    public class Customer
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty ;
        public string State { get; set; } = String.Empty ;
        public decimal /*?*/ Sales { get; set; } = 0; // ? after type lets the property take null values
        public bool Active { get; set; } = true; 



    }
}
