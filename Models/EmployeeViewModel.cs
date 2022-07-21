using Qulix.Entities;

namespace Qulix.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Middlename { get; set; }
    // public DateTimeOffset SignedDate { get; set; }
    public EPosition Position { get; set; }
    public int CompanyId { get; set; }
}