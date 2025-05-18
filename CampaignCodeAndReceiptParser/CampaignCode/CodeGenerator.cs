using System.Text;

namespace CampaignCodeAndReceiptParser.CampaignCode;

public class CodeGenerator
{
    private readonly string _secretKey;
    private readonly Random _random = new();

    public CodeGenerator(string secretKey)
    {
        _secretKey = secretKey;
    }
    
    public List<string> GenerateCodes(int count)
    {
        var codeList = new HashSet<string>();
        
        while (codeList.Count < count)
        {
            var randomPart = GenerateRandomString(5);
            var hash = CodeHelper.Hash(randomPart, _secretKey);
            var hashedPart = hash.Substring(0, 3);
            var code = randomPart + hashedPart;

            codeList.Add(code);
        }

        return codeList.ToList();
    }
    private string GenerateRandomString(int length)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            var index = _random.Next(CodeHelper.Characters.Length);
            builder.Append(CodeHelper.Characters[index]);
        }
        return builder.ToString();
    }
   
}