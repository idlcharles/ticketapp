using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketSystemAPI.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketDbContext _context;

        // ✅ Correção: Injeção de dependência do TicketDbContext
        public TicketController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            // ✅ Correção: Usar o banco de dados em vez da lista estática
            return Ok(_context.Tickets.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            // ✅ Correção: Usar o banco de dados em vez da lista estática
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");
            return Ok(ticket);
        }

        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] Ticket newTicket)
        {
            // ✅ Correção: Validar o modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ Correção: Usar o banco de dados em vez da lista estática
            newTicket.Id = _context.Tickets.Count() + 1;
            newTicket.Status = "Aberto"; // Status inicial
            newTicket.DataCriacao = DateTime.Now;
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTicket(int id, [FromBody] Ticket updatedTicket)
        {
            // ✅ Correção: Usar o banco de dados em vez da lista estática
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");

            // ✅ Correção: Validar o modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ticket.Titulo = updatedTicket.Titulo;
            ticket.Descricao = updatedTicket.Descricao;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateTicketStatus(int id, [FromBody] string novoStatus)
        {
            // ✅ Correção: Usar o banco de dados em vez da lista estática
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");

            if (novoStatus != "Aberto" && novoStatus != "Em Atendimento" && novoStatus != "Atendido")
                return BadRequest("Status inválido. Use: 'Aberto', 'Em Atendimento' ou 'Atendido'.");

            ticket.Status = novoStatus;
            _context.SaveChanges();
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTicket(int id)
        {
            // ✅ Correção: Usar o banco de dados em vez da lista estática
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return NoContent();
        }
    }

    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")] // ✅ Correção: Validação
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")] // ✅ Correção: Validação
        public string Descricao { get; set; }

        public string Status { get; set; } // "Aberto", "Em Atendimento", "Atendido"
        public DateTime DataCriacao { get; set; }
    }

    // ✅ Correção: Adicionar DbContext para persistência
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
}