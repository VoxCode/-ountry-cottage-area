using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class AgricultureType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgriculturesCategoryId { get; set; }
        public int Index { get; set; }
        public string Content { get; set; }  
        public AgriculturesCategory AgriculturesCategory { get; set; }

        public virtual ICollection<IncompatibleAgriculture> FirstCultures { get; set; }
        public virtual ICollection<IncompatibleAgriculture> SecondCultures { get; set; }
    }

    
}   