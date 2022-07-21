using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qulix.Entities;
public class Employee
{
    [Key]
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Middlename { get; set; }
    // public DateTimeOffset SignedDate { get; set; }
    public EPosition Position { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public Company Company { get; set; }
    public int CompanyId { get; set; }
}