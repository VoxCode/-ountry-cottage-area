using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class Agriculture
    {
        public int Id { get; set; }
        public string Variable { get; set; }
        public int AgricultureIndex { get; set; }
        public int AgricultureNumber { get; set; }
        public DateTime Date { get; set; }       
        public int AreaId { get; set; }             
    }
}