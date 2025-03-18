using System.ComponentModel.DataAnnotations;

namespace ticketapp.Models
{
    public class Ticket
    {
        [Key] // Define como chave primária
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string Status { get; set; } = "Aberto"; // Padrão: Aberto

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
