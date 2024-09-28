using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E2P.Models;
public class Company : Entity
{
    public string Address { get; set; } = string.Empty;
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    //public int ExcelFileCount { get; set; }
    //public int PDFFileCount { get; set; }

    // Dependencies //
    public ICollection<ExcelFile> ExcelFiles { get; set; } = new List<ExcelFile>();
    public ICollection<PDFFile> PDFFiles { get; set; } = new List<PDFFile>();
}
