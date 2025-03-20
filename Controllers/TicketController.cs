using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketSystemAPI.Data;
using TicketSystemAPI.Models;

namespace TicketSystemAPI.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketDbContext _context;

        public TicketController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            return Ok(_context.Tickets.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");
            return Ok(ticket);
        }

        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] Ticket newTicket)
        {
            newTicket.Status = "Aberto";
            newTicket.DataCriacao = DateTime.Now;
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateTicketStatus(int id, [FromBody] string novoStatus)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");

            if (novoStatus != "Aberto" && novoStatus != "Em Atendimento" && novoStatus != "Atendido")
                return BadRequest("Status inválido.");

            ticket.Status = novoStatus;
            _context.SaveChanges();
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
                return NotFound("Ticket não encontrado.");

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return NoContent();
        }
    }
}