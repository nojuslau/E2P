using E2P.Models;
using Microsoft.EntityFrameworkCore;

namespace E2P.Persistence;
public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<ExcelFile> ExcelFiles { get; }
    DbSet<PDFFile> PDFFiles { get; }
}
