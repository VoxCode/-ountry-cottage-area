using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Сountry_cottage_area.Models
{
    public class AllMap
    {
        public int Id { get; set; }
        [Required]
        public string Cordinates { get; set; }
        [Required]
        public int SaveNumber { get; set; }
        public int AreaId { get; set; }     
    }
}