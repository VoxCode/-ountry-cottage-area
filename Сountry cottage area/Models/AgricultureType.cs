using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class AgricultureType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AgriCategory { get; set; }
        public int Index { get; set; }
        public string Content { get; set; }
        public string AgricultureAppClass { get; set; }
        public List<IncompatibleAgriculture> IncompatibleAgricultures { get; set; }
    }
}