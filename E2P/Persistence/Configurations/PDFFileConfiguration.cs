using E2P.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E2P.Persistence.Configurations;
public class PDFFileConfiguration : IEntityTypeConfiguration<PDFFile>
{
    public void Configure(EntityTypeBuilder<PDFFile> builder)
    {
        // Define table name
        builder.ToTable("PDF_File");

        // Define primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.Property(p => p.FileSize)
            .IsRequired();

        builder.Property(p => p.FilePath)
            .IsRequired()
            .HasMaxLength(500); // Assuming file paths are capped at 500 characters

        // PDFFile -> ExcelFile (one-to-one)
        builder
            .HasOne(p => p.ExcelFile)
            .WithOne(e => e.ConvertedPDFFile)
            .HasForeignKey<PDFFile>(p => p.ExcelFileId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete PDF when related Excel file is deleted

        // PDFFile -> Company (many-to-one)
        builder
            .HasOne(p => p.Company)
            .WithMany(c => c.PDFFiles)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete PDF files when the company is deleted

        // Configure inherited fields if necessary
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150); // Set a reasonable maximum length for file names

        builder.Property(p => p.DateCreated)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Default to the current UTC time

        builder.Property(p => p.DateModified)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Default to the current UTC time
    }
}
