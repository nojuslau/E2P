using System;

namespace E2P.Models.SearchFilters;
public class PDFFileSearchFilters
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public int CompanyId { get; set; }
    public int ExcelFileId { get; set; }
}
