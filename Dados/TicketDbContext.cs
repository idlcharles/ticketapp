using Microsoft.EntityFrameworkCore;
using ticketapp.Models;

namespace ticketapp.Dados
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

    }
}