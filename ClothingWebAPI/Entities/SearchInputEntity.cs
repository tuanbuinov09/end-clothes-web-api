using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class SearchInputEntity
    {
    
            public SearchInputEntity(int top, string keyword)
            {
                this.top = top;
                this.keyword = keyword;
            }
        public SearchInputEntity()
        {
          
        }
        public int top { get; set; }
            public string keyword { get; set; }
    }
}
