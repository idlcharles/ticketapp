﻿using Microsoft.EntityFrameworkCore;
using TicketSystemAPI.Models;

namespace TicketSystemAPI.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
}