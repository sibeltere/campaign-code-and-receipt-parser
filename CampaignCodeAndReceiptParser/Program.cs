using System.Security;
using CampaignCodeAndReceiptParser.CampaignCode;
using CampaignCodeAndReceiptParser.ReceiptParser;

class Program
{
    private const string secretKey = "secret-key";
    static void Main(string[] args)
    {
        Console.WriteLine("Please make your choice:");
        Console.WriteLine("1- Campaign Code Generation");
        Console.WriteLine("2- Campaign Code Validation");
        Console.WriteLine("3- Parse Receipt Text from OCR JSON");
        Console.WriteLine("Your choice: ");
      
        var input = Console.ReadLine();

        if (!int.TryParse(input, out int choice))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        switch (choice)
        {
            case 1:
                GenerateCampaignCode();
                break;
            case 2:
                ValidateCampaignCode();
                break;
            case 3:
                Console.WriteLine("Receipt Text from OCR JSON:");
                break;
            default:
                Console.WriteLine("Please enter a valid choice");
                break;
        }
    }

    private static void GenerateCampaignCode()
    {
        var generator = new CodeGenerator(secretKey);
        var code = generator.GenerateCode();
        Console.WriteLine($"Code: {code}");
    }

    private static void ValidateCampaignCode()
    {
        Console.Write("Please enter the code you want to validate: ");
        var code = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(code))
        {
            Console.WriteLine("Code cannot be empty.");
            return;
        }

        var validator = new CodeValidator(secretKey);
        var isValid = validator.ValidateCode(code);

        Console.WriteLine(isValid ? "Code is valid." : "Code is not valid.");
    }

   
}