using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        public string UserId { get; set; }
        public List<AllMap> AllMaps { get; set; }
        public List<Agriculture> Agricultures { get; set; }
        public List<PlanningStage> PlanningStages { get; set; }
    }
}