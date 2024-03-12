using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace FSPBook.Data.Entities;
public class Profile
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }

    [NotMapped]
    public string FullName => FirstName + " " + LastName;

    public ICollection<Post> Posts { get; } = new List<Post>();

}
