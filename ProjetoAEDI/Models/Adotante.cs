using ProjetoAEDI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAEDI.Models
{
    public class Adotante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do adotante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O contato é obrigatório.")]
        [StringLength(50, ErrorMessage = "O contato não pode exceder 50 caracteres.")]
        public string Contato { get; set; }

        public string Preferencias { get; set; }
        public string HistoricoVisitas { get; set; }

        public ICollection<Adocao> Adocoes { get; set; }
    }

}

