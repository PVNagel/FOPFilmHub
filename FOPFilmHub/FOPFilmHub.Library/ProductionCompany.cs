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
    public class ProductionCompany
    {
        [Key]
        public int ProductionCompanyId { get; set; }
        public int TmdbProductionCompanyId { get; set; }
        public string? LogoPath { get; set; }
        public string? Name { get; set; }
        public string? OriginCountry { get; set; }
        public List<Film>? Films { get; set; }

    }
}
