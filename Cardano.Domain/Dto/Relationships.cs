using System.Text.Json.Serialization;

namespace Cardano.Domain.Dto;

public class Relationships
{
    [JsonPropertyName("managing-lou")]
    public ManagingLou ManagingLou { get; set; }

    [JsonPropertyName("lei-issuer")]
    public LeiIssuer LeiIssuer { get; set; }

    [JsonPropertyName("field-modifications")]
    public FieldModifications FieldModifications { get; set; }

    [JsonPropertyName("direct-parent")]
    public DirectParent DirectParent { get; set; }

    [JsonPropertyName("ultimate-parent")]
    public UltimateParent UltimateParent { get; set; }

    [JsonPropertyName("direct-children")]
    public DirectChildren DirectChildren { get; set; }

    [JsonPropertyName("managed-funds")]
    public ManagedFunds ManagedFunds { get; set; }

    [JsonPropertyName("sub-funds")]
    public SubFunds SubFunds { get; set; }
}
