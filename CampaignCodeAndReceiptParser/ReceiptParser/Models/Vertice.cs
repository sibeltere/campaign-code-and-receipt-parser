using Newtonsoft.Json;

namespace CampaignCodeAndReceiptParser.ReceiptParser.Models;

public class Vertice
{
    [JsonProperty("x")]
    public int X { get; set; }
    
    [JsonProperty("y")]
    public int Y { get; set; }
}