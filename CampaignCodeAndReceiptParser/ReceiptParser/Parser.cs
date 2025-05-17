using CampaignCodeAndReceiptParser.ReceiptParser.Models;
using Newtonsoft.Json;

namespace CampaignCodeAndReceiptParser.ReceiptParser;

public class Parser
{
    public static List<string> ParseTextLines(string jsonContent)
    {
        var responseList = JsonConvert.DeserializeObject<List<Response>>(jsonContent);
        if (responseList == null || responseList.Count == 0) return [];


        var frame = responseList[0].BoundingPoly?.Vertices;
        if (frame == null) return [];

        var frameMinX = frame.Min(v => v.X);
        var frameMaxY = frame.Max(v => v.Y);

        var filtered = responseList.Skip(1)
            .Where(r => !string.IsNullOrWhiteSpace(r.Description)
                        && r.BoundingPoly?.Vertices != null
                        && r.BoundingPoly.Vertices.Min(v => v.X) >= frameMinX
                        && r.BoundingPoly.Vertices.Max(v => v.Y) <= frameMaxY)
            .Select(r=> new Output()
            {
                Description = r.Description,
                LineNumber = 0,
                X = r.BoundingPoly.Vertices.Min(v => v.X),
                Y = r.BoundingPoly.Vertices.Max(v => v.Y)
            })
            .OrderBy(r => r.Y)
            .ToList();

        var lines = new List<string>();
        var currentLine = new List<Output>();
        Output? prevItem = null;
        var lineNumber = 0;

        foreach (var item in filtered)
        {
            if (prevItem == null || item.Y > prevItem.Y + 15)
            {
                if (currentLine.Count > 0)
                    lines.Add($"{lineNumber} {string.Join(" ", currentLine.OrderBy(cl=>cl.X).Select(cl=>cl.Description))}");

                currentLine.Clear();
                lineNumber++;
            }

            item.LineNumber = lineNumber;
            currentLine.Add(new Output{Description = item.Description.Trim(),LineNumber = item.LineNumber,X=item.X, Y=item.Y});
            prevItem = item;
        }

        if (currentLine.Count > 0)
            lines.Add($"{lineNumber} {string.Join(" ", currentLine.OrderBy(cl=>cl.X).Select(cl=>cl.Description))}");

        return lines;
    }
}