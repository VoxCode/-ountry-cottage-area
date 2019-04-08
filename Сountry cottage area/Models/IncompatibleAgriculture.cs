using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Сountry_cottage_area.Models
{
    public class IncompatibleAgriculture
    {
        public int Id { get; set; }

        public int FirstCulureId { get; set; }
        public int? SecondCulureId { get; set; }

        [InverseProperty("FirstCultures")]
        [ForeignKey("FirstCulureId")]
        public virtual AgricultureType FirstCulure { get; set; }

        [InverseProperty("SecondCultures")]
        [ForeignKey("SecondCulureId")]
        public virtual AgricultureType SecondCulure { get; set; }
    }
}