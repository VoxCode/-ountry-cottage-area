using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class PlanningStage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int AreaId { get; set; }
    }
}