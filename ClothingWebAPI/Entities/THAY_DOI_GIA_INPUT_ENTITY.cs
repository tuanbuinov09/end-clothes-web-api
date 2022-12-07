using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public class THAY_DOI_GIA_INPUT_ENTITY
    {
        public THAY_DOI_GIA_INPUT_ENTITY()
        {
        }
        public string MA_NV { get; set; }
        public List<THAY_DOI_GIA_ENTITY> listThayDoiGia { get; set; }
    }
}
