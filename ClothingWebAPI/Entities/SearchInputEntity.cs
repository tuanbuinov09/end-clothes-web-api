using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class SearchInputEntity
    {
        public SearchInputEntity()
        {
          
        }
        public int top { get; set; }
        public string keyword { get; set; }
        public int priceFrom { get; set; }

        public int priceTo { get; set; }
    }
}
