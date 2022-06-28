using System.ComponentModel.DataAnnotations;

namespace LP2MVCWithAuth.Data.Models
{
    public class ListaTarefas
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Titulo { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Required]
        public DateTime DataEntrega { get; set; }
    }
}
