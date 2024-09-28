using System;
using System.ComponentModel.DataAnnotations;

namespace E2P.Models;
public class Entity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateTime DateCreated { get; set; } = DateTime.Now;
    [Required]
    public DateTime DateModified { get; set; } = DateTime.Now;
}
