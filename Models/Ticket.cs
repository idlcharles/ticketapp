namespace TicketSystemAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; } // "Aberto", "Em Atendimento", "Atendido"
        public DateTime DataCriacao { get; set; }
        public class Ticket
        {
            public string Titulo { get; set; } = "";
            public string Descricao { get; set; } = "";
            public string Status { get; set; } = "";

        }
    }
}