using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E2P.Models;
public class PDFFile : Entity
{
    [Required]
    public int FileSize { get; set; }
    [Required]
    public string FilePath { get; set; } = string.Empty;

    // Dependencies //
    [Required, ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    [Required, ForeignKey(nameof(ExcelFile))]
    public int ExcelFileId { get; set; }
    public ExcelFile ExcelFile { get; set; }
}
