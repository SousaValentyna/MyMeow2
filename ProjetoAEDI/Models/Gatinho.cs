using System.ComponentModel.DataAnnotations;

namespace ProjetoAEDI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Gatinho
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do gatinho é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade do gatinho é obrigatória.")]
        [Range(0, 30, ErrorMessage = "A idade deve estar entre 0 e 30 anos.")]
        public int Idade { get; set; }

        [StringLength(50, ErrorMessage = "A cor não pode exceder 50 caracteres.")]
        public string Cor { get; set; }

        [StringLength(50, ErrorMessage = "O temperamento não pode exceder 50 caracteres.")]
        public string Temperamento { get; set; }

        public string RequisitosEspeciais { get; set; }

        public Adocao Adocao { get; set; }
    }

}

