namespace CampaignCodeAndReceiptParser.ReceiptParser.Models;

public record Output
{
    public int LineNumber { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Description { get; set; }
    
}