using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOPFilmHub.Library
{
    public class ProductionCountry
    {
        [Key]
        public int Id { get; set; }
        public string? Iso31661 { get; set; }
        public string? Name { get; set; }
    }
}
