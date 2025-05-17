using Newtonsoft.Json;

namespace CampaignCodeAndReceiptParser.ReceiptParser.Models;

public class Response
{
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("boundingPoly")]
    public BoundingPoly BoundingPoly { get; set; }
}