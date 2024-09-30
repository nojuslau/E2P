using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E2P.Models;

[Table("ExcelFile")]
public class ExcelFile : Entity
{
    [Required]
    public int FileSize { get; set; }
    [Required]
    public string FilePath { get; set; } = string.Empty;

    // Dependencies
    [Required, ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public PDFFile? ConvertedPDFFile { get; set; }

    [NotMapped]  // This ensures that this property is not mapped to the database
    public bool IsPDFGenerated => ConvertedPDFFile != null;
}
