namespace CampaignCodeAndReceiptParser.CampaignCode;

public class CodeValidator
{
    private readonly string _secretKey;

    public CodeValidator(string secretKey)
    {
        _secretKey = secretKey;
    }

    public bool ValidateCode(string code)
    {
        if (code.Length != 8) 
            return false;
        
        var randomPart = code.Substring(0, 5);
        var checksum = code.Substring(5, 3);
        var expectedChecksum = CodeHelper.Hash(randomPart,_secretKey).Substring(0, 3);
        return checksum == expectedChecksum;
    }
}