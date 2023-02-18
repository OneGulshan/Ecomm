using Ecomm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) // ye Context Class hamne isme isliye pass ki hai kunki ham DbContext ko connection provider ki info de saken using its DbContextOptions class. ab ham Context ki jagah apni constr pass karenge using by prog.cs file with Context class jhan sqlsever ka db hamne use kia hai. ye base class DbContext me ja kar init ho jaegi ye ek prakar ki generic class hoti hai. TContext Class hoti hai jo hamare constr ko init karegi connect karegi phisical db ke saath using AddDbContext in program.cs
        {

        }
        public DbSet<Category> Categories { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder) // in override we have diff types of methods we use here OnModelCreating method for seeding our database
        {
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id=1,Title="Samsung",DisplayOrder=1
            },
            new Category
            {
                Id = 2,
                Title = "Xiomi",
                DisplayOrder = 2
            });
        }
    }
}
//Context file ham isliye banate hain kunki ye ek prakar ki file/Class hai jo ki DbSet ka collection hota hai. DbSet is can say that Tables and Context file can say that Database as an example. by this Class file we communicate with phisical database.