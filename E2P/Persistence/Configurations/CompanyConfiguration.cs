using E2P.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E2P.Persistence.Configurations;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Define the table name
        builder.ToTable("Company");

        // Define primary key
        builder.HasKey(c => c.Id);

        // Configure properties
        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20); // Assuming 20 characters for international numbers

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        // Relationships

        // Company -> ExcelFile (one-to-many)
        builder
            .HasMany(c => c.ExcelFiles)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete to delete related files when company is deleted

        // Company -> PDFFile (one-to-many)
        builder
            .HasMany(c => c.PDFFiles)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for PDF files as well

        // Configure additional settings for inherited fields (if needed)
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150); // Assuming a maximum name length of 150 characters

        builder.Property(c => c.DateCreated)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Use database default for creation date

        builder.Property(c => c.DateModified)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()"); // Use database default for modification date
    }
}
