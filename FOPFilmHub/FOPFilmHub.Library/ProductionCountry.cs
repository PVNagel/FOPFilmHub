using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FOPFilmHub.Library
{
    public class ProductionCountry
    {
        [Key]
        public int ProductionCountryId { get; set; }
        public string? Iso31661 { get; set; }
        public string? Name { get; set; }
        public List<Film>? Films { get; set; }

    }
}
