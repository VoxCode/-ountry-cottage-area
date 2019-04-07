using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class IncompatibleAgriculture
    {
        public int Id { get; set; }
        public int AgricultureTypeId { get; set; }
        public string AgricultureName { get; set; }
        public AgricultureType AgricultureType { get; set; }
    }
}