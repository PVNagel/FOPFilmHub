using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOPFilmHub.Library
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public int TmdbGenreId { get; set; }
        public string? Name { get; set; }
        public List<Film>? Films { get; set; }
    }
}
