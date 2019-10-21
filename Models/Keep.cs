namespace keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImgUrl { get; set; }

    public bool Private { get; set; }

    public int Views { get; set; }

    public int Kept { get; set; }
  }
}