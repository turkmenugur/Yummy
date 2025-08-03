using Microsoft.EntityFrameworkCore;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Context;

//DB bağlantısı
public class ApiContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //sql serverda olsaydı bu şekilde olması yeterliydi sadece Server adı farklı olacaktı
        //optionsBuilder.UseSqlServer("Server=localhost,1433;initial catalog=YummyDb;integrated security=true;");
        
        //Podmande olduğu için bu şekilde
        // Güvenlik için normalde bu bilgiyi appsettings.json dosyasında tutmalısın.
        //optionsBuilder.UseSqlServer("Server=localhost,1433;Database=YummyDb;User Id=sa;Password=arsenLupen33;TrustServerCertificate=True;");
        //optionsBuilder.UseSqlServer("Server=UTRKMN\\SQLEXPRESS;Database=YummyDb;TrustServerCertificate=True;");
        //optionsBuilder.UseSqlServer("Server=UTRKMN\\SQLEXPRESS;initial catalog=YummyDb;integrated security=true;");
        optionsBuilder.UseSqlServer("Server=UTRKMN\\SQLEXPRESS;initial catalog=YummyDb;integrated security=true;TrustServerCertificate=True;");
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<YummyEvent> YummyEvents { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<About> Abouts { get; set; }
}