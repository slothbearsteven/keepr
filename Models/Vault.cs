using System.Collections.Generic;

namespace keepr.Models
{
  public class Vault
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Keep> Keeps { get; set; }

  }
}