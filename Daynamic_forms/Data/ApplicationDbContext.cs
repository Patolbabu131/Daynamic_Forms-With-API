using Daynamic_forms.Model;
using Microsoft.EntityFrameworkCore;

namespace Daynamic_forms.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Forms> forms { get; set; }
        public DbSet<Question> question { get; set; }
    }
}
