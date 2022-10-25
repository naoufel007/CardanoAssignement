namespace Cardano.Domain.Dto;

public class Entity
{
    public LegalName LegalName { get; set; }
    public List<OtherName> OtherNames { get; set; }
    public List<string> TransliteratedOtherNames { get; set; }
    public LegalAddress LegalAddress { get; set; }
    public HeadquartersAddress HeadquartersAddress { get; set; }
    public RegisteredAt RegisteredAt { get; set; }
    public string RegisteredAs { get; set; }
    public string Jurisdiction { get; set; }
    public string Category { get; set; }
    public LegalForm LegalForm { get; set; }
    public AssociatedEntity AssociatedEntity { get; set; }
    public string Status { get; set; }
    public Expiration Expiration { get; set; }
    public SuccessorEntity SuccessorEntity { get; set; }
    public List<string> SuccessorEntities { get; set; }
    public DateTime? CreationDate { get; set; }
    public string SubCategory { get; set; }
    public List<OtherAddress> OtherAddresses { get; set; }
    public List<string> EventGroups { get; set; }
}
