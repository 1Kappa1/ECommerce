using capanna.alessandro._5H.prenota.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class dataBase : IdentityDbContext<Utente>
{
    private readonly DbContextOptions? _options;
    
    public dataBase()
    {
    }

    public dataBase(DbContextOptions<dataBase> t){}

    protected override void
            OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=database.db");
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddIdentity<Utente, IdentityRole<int>>()
            .AddEntityFrameworkStores<dataBase>()
           .AddDefaultTokenProviders(); 
    }
    
    public DbSet<Utente> Utenti {get;set;}
    public DbSet<Fishing> Oggetti{get;set;}    
}

