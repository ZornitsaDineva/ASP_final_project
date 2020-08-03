using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DocumentsContext : DbContext
    {
        public DocumentsContext (DbContextOptions<DocumentsContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoUniversity.Models.Document> Document { get; set; }
    }
}
