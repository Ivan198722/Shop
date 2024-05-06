using Microsoft.EntityFrameworkCore;

namespace Shop
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

    }
}
