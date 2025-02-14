using eCommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.Data;

public class CraftContext : DbContext
{
    /// <summary>
    /// Constructor for CraftContext
    /// </summary>
    /// <param name="options"></param>
    public CraftContext(DbContextOptions<CraftContext> options) : base(options)
    {

    }

    public DbSet<Craft> Crafts { get; set; }
}
