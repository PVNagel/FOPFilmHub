using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOPFilmHub.Library
{
    public class ProductionCompany
    {
        [Key]
        public int Id { get; set; }
        public int TmdbCompanyId { get; set; }
        public string? Name { get; set; }
        public string? LogoPath { get; set; }
        public string? OriginCountry { get; set; }

    }
}
