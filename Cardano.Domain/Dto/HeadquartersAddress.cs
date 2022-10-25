namespace Cardano.Domain.Dto;

public class HeadquartersAddress
{
    public string Language { get; set; }
    public List<string> AddressLines { get; set; }
    public string AddressNumber { get; set; }
    public string AddressNumberWithinBuilding { get; set; }
    public string MailRouting { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}
