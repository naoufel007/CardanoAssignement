namespace Cardano.Domain.Dto;

public class Root
{
    public Meta Meta { get; set; }
    public Links Links { get; set; }
    public List<Datum> Data { get; set; }
}
