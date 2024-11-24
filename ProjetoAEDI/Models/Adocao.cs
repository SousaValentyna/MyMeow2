using ProjetoAEDI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAEDI.Models
{
    public class Adocao
    {
        public int Id { get; set; }

        [Required]
        public int GatinhoId { get; set; }
        public Gatinho Gatinho { get; set; }

        [Required]
        public int AdotanteId { get; set; }
        public Adotante Adotante { get; set; }

        [Required]
        public DateTime DataAdocao { get; set; } = DateTime.Now;
    }

}

