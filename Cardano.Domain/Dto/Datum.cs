namespace Cardano.Domain.Dto;

public class Datum
{
    public string Type { get; set; }
    public string Id { get; set; }
    public Attributes Attributes { get; set; }
    public Relationships Relationships { get; set; }
    public Links Links { get; set; }
}