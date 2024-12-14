using Microsoft.EntityFrameworkCore;

namespace Gestiones.context;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<models.Gestiones> Gestiones { get; set; } = null!;
}