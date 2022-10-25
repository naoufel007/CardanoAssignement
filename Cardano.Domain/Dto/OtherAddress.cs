namespace Cardano.Domain.Dto;

public class OtherAddress
{
    public string FieldType { get; set; }
    public string Language { get; set; }
    public string Type { get; set; }
    public List<string> AddressLines { get; set; }
    public string AddressNumber { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}
