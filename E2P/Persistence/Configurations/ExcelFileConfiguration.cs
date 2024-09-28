using E2P.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E2P.Persistence.Configurations;
public class ExcelFileConfiguration : IEntityTypeConfiguration<ExcelFile>
{
    public void Configure(EntityTypeBuilder<ExcelFile> builder)
    {
        // Define table name
        builder.ToTable("Excel_File");

        // Define primary key
        builder.HasKey(e => e.Id);

        // Configure properties
        builder.Property(e => e.FileSize)
            .IsRequired();

        builder.Property(e => e.FilePath)
            .IsRequired()
            .HasMaxLength(500); // Assuming file paths are capped at 500 characters

        // ExcelFile -> PDFFile (one-to-one)
        builder
            .HasOne(e => e.ConvertedPDFFile)
            .WithOne(p => p.ExcelFile)
            .HasForeignKey<PDFFile>(p => p.ExcelFileId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete PDF when Excel is deleted

        // ExcelFile -> Company (many-to-one)
        builder
            .HasOne(e => e.Company)
            .WithMany(c => c.ExcelFiles)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete Excel files when the company is deleted

        // Configure inherited fields if necessary
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150); // Set a reasonable maximum length for file names

        builder.Property(e => e.DateCreated)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Default to the current UTC time

        builder.Property(e => e.DateModified)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Default to the current UTC time
    }
}
