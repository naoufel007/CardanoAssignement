using System.Text.Json.Serialization;

namespace Cardano.Domain.Dto;

public class Links
{
    public string First { get; set; }
    public string Last { get; set; }
    public string Self { get; set; }
    public string Related { get; set; }

    [JsonPropertyName("reporting-exception")]
    public string ReportingException { get; set; }

    [JsonPropertyName("relationship-record")]
    public string RelationshipRecord { get; set; }

    [JsonPropertyName("lei-record")]
    public string LeiRecord { get; set; }

    [JsonPropertyName("relationship-records")]
    public string RelationshipRecords { get; set; }
}
