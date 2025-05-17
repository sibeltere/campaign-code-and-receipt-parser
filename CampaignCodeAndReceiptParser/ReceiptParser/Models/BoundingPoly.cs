using Newtonsoft.Json;

namespace CampaignCodeAndReceiptParser.ReceiptParser.Models;

public class BoundingPoly
{
    [JsonProperty("vertices")]
    public List<Vertice> Vertices { get; set; }
}