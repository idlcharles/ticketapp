using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ticketapp.Dados
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext (DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

    }

}
