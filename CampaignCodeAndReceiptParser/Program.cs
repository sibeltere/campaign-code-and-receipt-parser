class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please make your choice:");
        Console.WriteLine("1- Campaign Code Generation & Verification");
        Console.WriteLine("2- Parse Receipt Text from OCR JSON");
        Console.WriteLine("Your choice: ");
        var choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Campaign Code Generation & Verification");
                break;
            case 2:
                Console.WriteLine("Receipt Text from OCR JSON");
                break;
            default:
                Console.WriteLine("Please enter a valid choice");
                break;
            
        }
    }
}