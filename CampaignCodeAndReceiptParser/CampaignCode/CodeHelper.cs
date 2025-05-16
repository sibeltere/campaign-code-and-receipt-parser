using System.Security.Cryptography;
using System.Text;

namespace CampaignCodeAndReceiptParser.CampaignCode;

public static class CodeHelper
{
    public const string Characters = "ACDEFGHKLMNPRTXYZ234579";
    public static string Hash(string input, string secretKey)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));

        return string.Concat(hash.Select(b => Characters[b % Characters.Length]));
    }
}