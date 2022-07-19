using System.ComponentModel.DataAnnotations;

namespace Qulix.Entities;
public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string OrganizationalForm { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}