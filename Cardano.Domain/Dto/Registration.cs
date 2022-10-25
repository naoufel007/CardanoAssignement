namespace Cardano.Domain.Dto;

public class Registration
{
    public DateTime? InitialRegistrationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public string Status { get; set; }
    public DateTime? NextRenewalDate { get; set; }
    public string ManagingLou { get; set; }
    public string CorroborationLevel { get; set; }
    public ValidatedAt ValidatedAt { get; set; }
    public string ValidatedAs { get; set; }
    public List<string> OtherValidationAuthorities { get; set; }
}
