namespace Cardano.Domain.Entity;

public class Transaction
{
    public string Uti { get; set; }
    public string Isin { get; set; }
    public decimal National { get; set; }
    public string NationalCurrency { get; set; }
    public string Type { get; set; }
    public DateTime? DateTime { get; set; }
    public decimal Rate { get; set; }
    public string Lei { get; set; }
    public string LegalName { get; set; }
    public string Bic { get; set; }
    public decimal Cost { get; set; }
}
