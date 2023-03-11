using Microsoft.EntityFrameworkCore;

namespace Game_X_O.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }

        public DbSet<Game> Games => Set<Game>();

        public DbSet<PlayerMove> PlayerMoves => Set<PlayerMove>();
    }
}
