﻿using CampaignCodeAndReceiptParser.CampaignCode;
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
                GenerateCampaignCodes();
                break;
            case 2:
                ValidateCampaignCode();
                break;
            case 3:
                ParseAndPrintReceipt();
                break;
            default:
                Console.WriteLine("Please enter a valid choice");
                break;
        }
    }

    private static void GenerateCampaignCodes()
    {
        Console.Write("Please enter the number of codes you want to generate: ");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out int count) && count <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        var generator = new CodeGenerator(secretKey);
        var codes = generator.GenerateCodes(count);

        if (codes.Count == 0)
        {
            Console.WriteLine("Failed to generate codes");
            return;
        }

        Console.WriteLine("Generated Codes:");
        Console.WriteLine(string.Join("\n", codes));
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

    private static void ParseAndPrintReceipt()
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, "ReceiptParser/response.json");

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("response.json file could not be found.");
            return;
        }

        var json = File.ReadAllText(fullPath);
        var lines = Parser.ParseTextLines(json);

        if (lines.Count == 0)
        {
            Console.WriteLine("Receipt is empty.");
            return;
        }

        Console.WriteLine("\nReceipt Line:");

        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }
}