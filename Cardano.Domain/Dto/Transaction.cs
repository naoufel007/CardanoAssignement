using Cardano.Domain.Enum;

namespace Cardano.Domain.Dto;

public class TransactionDto
{
    public string Uti { get; set; }
    public string Isin { get; set; }
    public decimal? National { get; set; }
    public string NationalCurrency { get; set; }
    public TransactionType? Type { get; set; }
    public DateTime? DateTime { get; set; }
    public decimal? Rate { get; set; }
    public string Lei { get; set; }
}
