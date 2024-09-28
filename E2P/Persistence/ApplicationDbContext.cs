using E2P.AppSettingsModels;
using E2P.Models;
using E2P.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace E2P.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ExcelFile> ExcelFiles => Set<ExcelFile>();
    public DbSet<PDFFile> PDFFiles => Set<PDFFile>();
    private readonly IWritableOptions<ApplicationSettings> _options;

    public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IWritableOptions<ApplicationSettings> writableOptions)
    : base(options)
    {
        _options = writableOptions;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new ExcelFileConfiguration());
        builder.ApplyConfiguration(new PDFFileConfiguration());
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_options.Value.ConnectionStrings.DefaultConnection);
        }
    }
}
